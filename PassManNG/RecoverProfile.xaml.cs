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
    /// Interaction logic for RecoverProfile.xaml
    /// </summary>
    public partial class RecoverProfile : Page
    {
        public RecoverProfile()
        {
            InitializeComponent();
        }

        NavigationService nav;

        private void recover_button_Click(object sender, RoutedEventArgs e)
        {
            String RecoverUsername = recover_user.Text;
            String RecoverPassword = recover_pass.Password;

            Items TestRecoverItem = App.PassDB.GetItem(RecoverUsername);
            if (RecoverPassword == MainWindow.DecryptString(TestRecoverItem.EncryptedPassword))
            {
                // Devolver password
                String RecoverOwner = TestRecoverItem.Owner;
                Items RecoveredUser = App.PassDB.GetProfile(RecoverOwner);
                String RecoveredPassword = MainWindow.DecryptString(RecoveredUser.EncryptedPassword);

                top_block.Text = (String)Application.Current.TryFindResource("RecoveredPasswordText");
                recover_user.Text = RecoverOwner;
                recovered_pass.Text = RecoveredPassword;
                recovered_pass.Visibility = Visibility.Visible;
            }
            else
            {
                // enviar mensagem de aviso
            }
        }

        private void cancel_button_Click(object sender, RoutedEventArgs e)
        {
            nav.Navigate(new System.Uri("LoginPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            nav = NavigationService.GetNavigationService(this);
        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            top_block.Text = (String)Application.Current.TryFindResource("RecoverDirections");
            recovered_pass.Visibility = Visibility.Hidden;
            nav.Navigate(new System.Uri("LoginPage.xaml", UriKind.RelativeOrAbsolute));
        }


    }
}
