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
    /// Interaction logic for PasswordPropertiesPage.xaml
    /// </summary>
    public partial class PasswordPropertiesPage : Page
    {
        public PasswordPropertiesPage()
        {
            InitializeComponent();
        }

        NavigationService nav;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            nav = NavigationService.GetNavigationService(this);

            SymbolCheckbox.IsChecked = Properties.Settings.Default.Sett_Pass_Symbol;
            CasesCheckbox.IsChecked = Properties.Settings.Default.Sett_Pass_Casess;
            NumberCheckbox.IsChecked = Properties.Settings.Default.Sett_Pass_Digit;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Sett_Pass_Symbol = SymbolCheckbox.IsChecked.GetValueOrDefault();
            Properties.Settings.Default.Sett_Pass_Casess = CasesCheckbox.IsChecked.GetValueOrDefault();
            Properties.Settings.Default.Sett_Pass_Digit = NumberCheckbox.IsChecked.GetValueOrDefault();

            Properties.Settings.Default.Save();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}
