using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using GithubStatistics.Annotations;
using GithubStatistics.Common.Enums;
using GithubStatistics.Interfaces;
using GithubStatistics.Models;
using GithubStatistics.Services;
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GithubStatistics.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserPage : Page, INotifyPropertyChanged
    {
        private SearchResultRoot searchResultRoot;
        private ObservableCollection <String> suggestions; 

        private UserDetailsResult _userDetailsResult;

        public UserDetailsResult UserDetailsResult
        {
            get => _userDetailsResult;
            set
            {
                _userDetailsResult = value;
                OnPropertyChanged(nameof(UserDetailsResult));
            }
        }

        private readonly IGithubService _githubService = new GithubService();
        public UserPage()
        {
            this.InitializeComponent();
            suggestions = new ObservableCollection < string > ();  
        }

        private async void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            // Only get results when it was a user typing, 
            // otherwise assume the value got filled in by TextMemberPath 
            // or the handler for SuggestionChosen.
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var name = sender.Text;
                if (name == string.Empty) return;
                try
                {
                    var response = await Task.Run(() => SendSearchRequest(name));

                    if (response.max != null && response.remaining != null)
                    {
                        SearchLimit.Text = $"{response.remaining}/{response.max}";
                        SearchLimitBoard.Background = new SolidColorBrush(
                            LimitColor.GetColorByIndex(CalculateColorIndex(response.remaining, response.max)));
                    }
                    //var response = await SendSearchRequest(name);

                    searchResultRoot = response.root;
                    suggestions = new ObservableCollection<string>(response.root.items.Select(x => x.login));
                    sender.ItemsSource = suggestions;
                    //Set the ItemsSource to be your filtered dataset
                    //sender.ItemsSource = dataset;
                }
                catch (HttpRequestException ex)
                {
                    SearchLimit.Text = $"Wait!";
                    SearchLimitBoard.Background = new SolidColorBrush(
                        LimitColor.GetColorByIndex(CalculateColorIndex("0", "10")));
                }
                catch(Exception ex)
                {
                    throw ex;
                }
                
            }
        }


        private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            JsonTextBody.Text = "Choosen";
        }


        private async void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                //var choosedUser = searchResultRoot.items.First(x => x.login == args.ChosenSuggestion.ToString());
                var name = args.ChosenSuggestion.ToString();
                var response = await Task.Run(() => SendGetUserDetailsRequest(name));

                if (response.max != null && response.remaining != null)
                {
                    NormalLimit.Text = $"{response.remaining}/{response.max}";
                    NormalLimitBoard.Background = new SolidColorBrush(LimitColor.GetColorByIndex(CalculateColorIndex(response.remaining, response.max)));
                }
                UserDetailsResult = response.result;
            }
            else
            {
                JsonTextHeader.Text = sender.Text; 
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var response = SendGetUserDetailsRequest("wojtek-rak");
            JsonTextBody.Text = response.Result.ToString();
            UserDetailsResult = response.Result.result;


        }

        private async Task<(SearchResultRoot root, string max, string remaining)> SendSearchRequest(string name)
        {
            var response = _githubService.SearchUser(name);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var maxLimit = response.Headers.GetValues("X-RateLimit-Limit").FirstOrDefault();
            var remaining = response.Headers.GetValues("X-RateLimit-Remaining").FirstOrDefault();

            return (JsonConvert.DeserializeObject<SearchResultRoot>(responseBody), maxLimit, remaining);
        }

        private async Task<(UserDetailsResult result, string max, string remaining)> SendGetUserDetailsRequest(string name)
        {
            var response = _githubService.GetUserDetails(name);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var maxLimit = response.Headers.GetValues("X-RateLimit-Limit").FirstOrDefault();
            var remaining = response.Headers.GetValues("X-RateLimit-Remaining").FirstOrDefault();
            var result = JsonConvert.DeserializeObject<UserDetailsResult>(responseBody);
            return (result, maxLimit, remaining);
        }

        private int CalculateColorIndex(string remaining, string maxLimit)
        {
            var maxLimitValue = Int32.Parse(maxLimit);
            var remainingValue = Int32.Parse(remaining);
            return (int)(((float)remainingValue / maxLimitValue)*5);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
