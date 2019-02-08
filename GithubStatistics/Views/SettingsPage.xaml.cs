using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GithubStatistics.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        private readonly ApplicationDataContainer _applicationDataContainer;
        public SettingsPage()
        {
            this.InitializeComponent();
            _applicationDataContainer = ApplicationData.Current.LocalSettings;
            InitializeToken();
        }

        private void InitializeToken()
        {
            var value = _applicationDataContainer.Values[Com.AccesToken];
            if (value != null)
            {
                Token.Text = value.ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _applicationDataContainer.Values[Com.AccesToken] = Token.Text;
        }
    }
}
