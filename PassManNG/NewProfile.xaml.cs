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
    /// Interaction logic for NewProfile.xaml
    /// </summary>
    public partial class NewProfile : Page
    {
        public NewProfile()
        {
            InitializeComponent();
        }

        NavigationService nav;
        String TemporaryRandomPassword = String.Empty;

        public event EventHandler ProfileCreated;
        protected virtual void OnProfileCreated(EventArgs e)
        {
            EventHandler handler = ProfileCreated;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public bool EditingExistingProfile = false;

        private void CreateProfileButton_Click(object sender, RoutedEventArgs e)
        {
            // Verify password
            bool result = MyGlobals.GlobalMethods.VerifyPasswordQuality(NewProfilePasswordTextBox.Text);

            if (!result)
            {
                // 5 second warning
                Thread thread = new Thread(ShowInvalidPasswordWarning);
                thread.Start();

                NewProfilePasswordTextBox.Focus();
            }
            else
            {

                if (EditingExistingProfile)
                {
                    // Updates passwords ownerships
                    App.PassDB.UpdatePasswordsByField("profile", NewProfileUserTextBox.Text, MyGlobals.CurrentProfile.Username);
                    // Removes older version of profile
                    App.PassDB.RemoveProfile(MyGlobals.TemporaryItem.Username);

                    // Restores globals
                    MyGlobals.TemporaryItem = Items.Empty();
                    MyGlobals.CurrentProfile = null;

                    EditingExistingProfile = false;
                }

                AddProfile();

                // Pop up
                Thread thread = new Thread(MainWindow.ShowPopUp);
                thread.Start(App.NewWindow);

                nav.Navigate(new System.Uri("LoginPage.xaml", UriKind.RelativeOrAbsolute));
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (EditingExistingProfile)
            {
                nav.Navigate(new System.Uri("InitialPage.xaml", UriKind.RelativeOrAbsolute));
            }
            else
            {
                nav.Navigate(new System.Uri("LoginPage.xaml", UriKind.RelativeOrAbsolute));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GeneratePassword_Click(object sender, RoutedEventArgs e)
        {
            TemporaryRandomPassword = MyGlobals.GlobalMethods.GenerateRandPassword();
            NewProfilePasswordTextBox.Focus();
        }

        public void AddProfile()
        {
            string dKey = "Profile";
            string owner = NewProfileUserTextBox.Text;
            string ePass = MainWindow.EncryptString(NewProfilePasswordTextBox.Text);
            string user = NewProfileUserTextBox.Text;
            string notes = "Access Profile";

            Items novoProfile = new Items(dKey, owner, ePass, user, notes);

            // Add to DB
            App.PassDB.UpProfile(novoProfile);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            nav = NavigationService.GetNavigationService(this);

            // Check if there's temp info to show
            if (MyGlobals.TemporaryItem != null)
            {
                if (MyGlobals.TemporaryItem.Equals(MyGlobals.CurrentProfile))
                {
                    EditingExistingProfile = true;
                    NewProfileUserTextBox.Text = MyGlobals.CurrentProfile.Username;
                    NewProfileUserTextBox.IsEnabled = false;
                    NewProfilePasswordTextBox.Text = MyGlobals.CurrentProfile.Password;
                }
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;

            // If the textbox is a password textbox
            if (textbox == NewProfilePasswordTextBox)
            {
                GeneratePassword.Visibility = Visibility.Visible;
                textbox.Text = TemporaryRandomPassword;
            }
            else
            {
                GeneratePassword.Visibility = Visibility.Hidden;
            }

            if (textbox.Text == (String)textbox.Tag)
            {
                textbox.Text = String.Empty;
            }
            else
            {
                
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;

            // If the textbox is a password textbox
            if (textbox == NewProfilePasswordTextBox)
            {
                //GeneratePassword.Visibility = Visibility.Hidden;
                if (textbox.Text == String.Empty)
                {
                    textbox.Text = textbox.Tag as String;
                }
            }
            else
            {
                if (textbox.Text == String.Empty)
                {
                    textbox.Text = textbox.Tag as String;
                }
            }
        }

        private void ShowInvalidPasswordWarning()
        {
            this.Dispatcher.BeginInvoke((ThreadStart)delegate() { ErroInvalidPassword.Visibility = Visibility.Visible; }, System.Windows.Threading.DispatcherPriority.Normal);
            Thread.Sleep(TimeSpan.FromSeconds(5));
            this.Dispatcher.BeginInvoke((ThreadStart)delegate() { ErroInvalidPassword.Visibility = Visibility.Hidden; }, System.Windows.Threading.DispatcherPriority.Normal);
        }

    }
}
