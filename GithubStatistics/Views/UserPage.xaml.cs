using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
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
    public sealed partial class UserPage : Page
    {
        private SearchResultRoot searchResultRoot;
        private UserDetailsResult userDetailsResult;
        private ObservableCollection < String > suggestions;  
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
                var response = await SendSearchRequest(name);
                searchResultRoot = response;
                suggestions = new ObservableCollection<string>(response.items.Select(x => x.login));
                sender.ItemsSource = suggestions;  
                //Set the ItemsSource to be your filtered dataset
                //sender.ItemsSource = dataset;
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
                var response = await SendGetUserDetailsRequest(args.ChosenSuggestion.ToString());
                userDetailsResult = response;
            }
            else
            {
                JsonTextHeader.Text = sender.Text; 
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var response = SendSearchRequest("AAB");
        }

        private async Task<SearchResultRoot> SendSearchRequest(string name)
        {
            var response = Task.Run(() =>_githubService.SearchUser(name)).Result;
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            JsonTextHeader.Text = response.ToString();
            JsonTextBody.Text = responseBody;

            return JsonConvert.DeserializeObject<SearchResultRoot>(responseBody);
        }

        private async Task<UserDetailsResult> SendGetUserDetailsRequest(string name)
        {
            var response = Task.Run(() =>_githubService.GetUserDetails(name)).Result;
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            JsonTextHeader.Text = response.ToString();
            JsonTextBody.Text = responseBody;

            return JsonConvert.DeserializeObject<UserDetailsResult>(responseBody);
        }
    }
}
