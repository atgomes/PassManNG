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
    /// Interaction logic for PreferencesPage.xaml
    /// </summary>
    public partial class PreferencesPage : Page
    {
        public PreferencesPage()
        {
            InitializeComponent();
        }

        NavigationService nav;

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Sett_Pass_Symbol = SymbolCheckbox.IsChecked.GetValueOrDefault();
            Properties.Settings.Default.Sett_Pass_Casess = CasesCheckbox.IsChecked.GetValueOrDefault();
            Properties.Settings.Default.Sett_Pass_Digit = NumberCheckbox.IsChecked.GetValueOrDefault();

            Properties.Settings.Default.Save();

            nav.Navigate(new System.Uri("InitialPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            nav.Navigate(new System.Uri("InitialPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void GoHome_Click(object sender, RoutedEventArgs e)
        {
            nav.Navigate(new System.Uri("InitialPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            nav = NavigationService.GetNavigationService(this);

            NumberCheckbox.IsChecked = Properties.Settings.Default.Sett_Pass_Digit;
            CasesCheckbox.IsChecked = Properties.Settings.Default.Sett_Pass_Casess;
            SymbolCheckbox.IsChecked = Properties.Settings.Default.Sett_Pass_Symbol;
        }
    }
}
