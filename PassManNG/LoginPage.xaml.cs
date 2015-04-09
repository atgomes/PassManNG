using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        NavigationService nav;

        // Login to existing profile
        private void LoginProfileButton_Click(object sender, RoutedEventArgs e)
        {
            if (LoginRelated.Login(LoginScreenLogin.Text, LoginScreenActualPasswordBox.Password) == 0)
            {
                // Populates local list
                MyGlobals.Lista = App.PassDB.GetAllItems();

                // Enables menu blocked features
                MainWindow.EnablesMenuFeatures();

                nav.Navigate(new System.Uri("InitialPage.xaml", UriKind.RelativeOrAbsolute));
            }
            else
            {
                LoginScreenPassword.Visibility = Visibility.Visible;
                LoginScreenActualPasswordBox.Password = String.Empty;
                LoginScreenActualPasswordBox.Visibility = Visibility.Hidden;
            }
        }

        private void NewProfileButton_Click(object sender, RoutedEventArgs e)
        {
            nav.Navigate(new System.Uri("NewProfile.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            nav = NavigationService.GetNavigationService(this);

            ManageImageTooltips(false, "", Properties.Settings.Default.Language);


        }

        private void ClearCurrentText(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;

            textbox.Text = String.Empty;

            // If the textbox is a password textbox
            if (sender == LoginScreenPassword)
            {
                LoginScreenPassword.Visibility = Visibility.Hidden;

                LoginScreenActualPasswordBox.Visibility = Visibility.Visible;
                LoginScreenActualPasswordBox.Focus();
            }
        }

        private void ReWriteTipText(object sender, RoutedEventArgs e)
        {
            // IF the textbox is an actual password textbox
            if (sender == LoginScreenActualPasswordBox)
            {
                if (LoginScreenActualPasswordBox.Password == String.Empty)
                {
                    LoginScreenActualPasswordBox.Visibility = Visibility.Hidden;
                    LoginScreenPassword.Visibility = Visibility.Visible;
                }
            }
            else
            {
                TextBox textbox = sender as TextBox;

                if (textbox.Text == String.Empty)
                {
                    textbox.Text = textbox.Tag as String;
                }
            }
        }

        private void ChangeLanguage_Click(object sender, RoutedEventArgs e)
        {
            if (Thread.CurrentThread.CurrentCulture.IetfLanguageTag == "en-US")
            {
                MainWindow.SelectCulture("pt-PT");
                Properties.Settings.Default.Language = "pt-PT";
                Properties.Settings.Default.Save();
                ManageImageTooltips(false, "", Properties.Settings.Default.Language);
            }
            else
            {
                MainWindow.SelectCulture("en-US");
                Properties.Settings.Default.Language = "en-US";
                Properties.Settings.Default.Save();
                ManageImageTooltips(false, "", Properties.Settings.Default.Language);
            }
        }

        public void ManageImageTooltips(bool state, string updatedTooltip = "", string language = "")
        {
            //State Image
            if (state)
            {
                CurrentStateImage.ToolTip = (String)Application.Current.TryFindResource("ImageOK") + " " + updatedTooltip;
                CurrentStateImage.Source = new BitmapImage(new Uri(@"/positive.jpg", UriKind.Relative));

            }
            else
            {
                CurrentStateImage.ToolTip = (String)Application.Current.TryFindResource("ImageNOTOK");
                CurrentStateImage.Source = new BitmapImage(new Uri(@"/negative.jpg", UriKind.Relative));
            }

            //Language Image
            if (language == "en-US")
            {
                ChangeLanguage.ToolTip = (String)Application.Current.TryFindResource("LanguageEnglish");
            }
            if (language == "pt-PT")
            {
                ChangeLanguage.ToolTip = (String)Application.Current.TryFindResource("LanguagePortuguese");
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void forgotPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            nav.Navigate(new System.Uri("RecoverProfile.xaml", UriKind.RelativeOrAbsolute));
        }
        
    }
}
