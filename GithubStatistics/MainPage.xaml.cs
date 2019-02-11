using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using GithubStatistics.Common;
using GithubStatistics.Services;
using GithubStatistics.Views;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GithubStatistics
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            InitializeAccesToken();
        }

        private void nvTopLevelNav_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (NavigationViewItemBase item in nvTopLevelNav.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "UserPage")
                {
                    nvTopLevelNav.SelectedItem = item;
                    break;
                }
            }
            contentFrame.Navigate(typeof(UserPage));
        }

        private void nvTopLevelNav_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            //throw new NotImplementedException();
        }

        private void nvTopLevelNav_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var itemContent = args.InvokedItem;
            if (itemContent == null) return;
            switch (itemContent) 
            {
                case "User":
                    contentFrame.Navigate(typeof(UserPage));
                    break;

                case "Settings":
                    contentFrame.Navigate(typeof(SettingsPage));
                    break;
            }
        }

        private void InitializeAccesToken()
        {
            var value = ApplicationData.Current.LocalSettings.Values[Com.AccesToken];
            if (value != null)
            {
                GithubService.client.DefaultRequestHeaders.Authorization  = new AuthenticationHeaderValue("Bearer", value.ToString());
            }
        }
    }
}
