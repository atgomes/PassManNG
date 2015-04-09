using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PassManNG
{
    /// <summary>
    /// Interaction logic for InitialPage.xaml
    /// </summary>
    public partial class InitialPage : Page
    {
        public InitialPage()
        {
            InitializeComponent();
        }

        NavigationService nav;

        private void SearchBigButton_Click(object sender, RoutedEventArgs e)
        {
            nav.Navigate(new System.Uri("GetPassword.xaml", UriKind.RelativeOrAbsolute));
        }

        private void AddBigButton_Click(object sender, RoutedEventArgs e)
        {
            nav.Navigate(new System.Uri("SetPassword.xaml", UriKind.RelativeOrAbsolute));
        }

        private void HelpBigButton_Click(object sender, RoutedEventArgs e)
        {
            nav.Navigate(new System.Uri("Help.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            nav = NavigationService.GetNavigationService(this);
        }
    }
}
