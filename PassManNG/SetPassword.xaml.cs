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
    /// Interaction logic for SetPassword.xaml
    /// </summary>
    public partial class SetPassword : Page
    {
        public SetPassword()
        {
            InitializeComponent();
        }

        NavigationService nav;
        String TemporaryRandomPassword = String.Empty;

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            nav.Navigate(new System.Uri("InitialPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            // Only adds if the label is unique and required fields are filled
            if (MyGlobals.Lista.Exists(x => x.DirectKey == LabelTextBox.Text))
            {
                Thread thread = new Thread(ShowRectangleLabelExists);
                thread.Start();

                LabelTextBox.Focus();
            }
            else
            {
                if (LoginTextBox.Text == String.Empty || LoginTextBox.Text == (String)LoginTextBox.Tag)
                {
                    Thread thread = new Thread(ShowRequiredFieldWarning);
                    thread.Start(ErrorFieldEmpty2);

                    LoginTextBox.Focus();
                }
                else
                {
                    if(LabelTextBox.Text == String.Empty || LabelTextBox.Text == (String)LabelTextBox.Tag)
                    {
                        Thread thread = new Thread(ShowRequiredFieldWarning);
                        thread.Start(ErrorFieldEmpty);

                        LabelTextBox.Focus();
                    }
                    else
                    {
                        if (PasswordTextBox.Text == String.Empty || PasswordTextBox.Text == (String)PasswordTextBox.Tag)
                        {
                            Thread thread = new Thread(ShowRequiredFieldWarning);
                            thread.Start(ErrorFieldEmpty3);

                            PasswordTextBox.Focus();
                        }
                        else
                        {
                            // Add entry to local list and database
                            AddEntry();

                            // Checks if temporary Item exists, if so clears it
                            if (MyGlobals.TemporaryItem.DirectKey != String.Empty)
                            {
                                MyGlobals.TemporaryItem = Items.Empty();
                            }

                            nav.Navigate(new System.Uri("InitialPage.xaml", UriKind.RelativeOrAbsolute));
                        }
                    }
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            nav = NavigationService.GetNavigationService(this);

            if (MyGlobals.TemporaryItem.DirectKey != String.Empty)
            {
                // Pre-fill TextBoxes
                LabelTextBox.Text = MyGlobals.TemporaryItem.DirectKey;
                PasswordTextBox.Text = MyGlobals.TemporaryItem.Password;
                LoginTextBox.Text = MyGlobals.TemporaryItem.Username;
                NotesTextBox.Text = MyGlobals.TemporaryItem.Notes;
            }
            else
            {
                // Clears TextBoxes
                //LabelTextBox.Text = String.Empty;
                //PasswordTextBox.Text = String.Empty;
                //LoginTextBox.Text = String.Empty;
                //NotesTextBox.Text = String.Empty;
            }
        }

        public void AddEntry()
        {
            // Check if there's a logged in user
            if (MyGlobals.CurrentProfile == null)
            {

            }
            else
            {
                string dKey = LabelTextBox.Text;
                string owner = MyGlobals.CurrentProfile.Username;
                string ePass = MainWindow.EncryptString(PasswordTextBox.Text);
                string user = LoginTextBox.Text;
                string notes = NotesTextBox.Text;
                if (NotesTextBox.Text == NotesTextBox.Tag as String)
                {
                    notes = String.Empty;
                }
                Items novoItem = new Items(dKey, owner, ePass, user, notes);

                // Add to Local List
                MyGlobals.Lista.Add(novoItem);

                // Add to DB
                    App.PassDB.UpItem(novoItem);            
            }
        }

        private void GeneratePassword_Click(object sender, RoutedEventArgs e)
        {
            TemporaryRandomPassword = MyGlobals.GlobalMethods.GenerateRandPassword();
            PasswordTextBox.Focus();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;

            // If the textbox is a password textbox
            if (textbox == PasswordTextBox)
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
            if (textbox == PasswordTextBox)
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

        private void ShowRectangleLabelExists()
        {
            this.Dispatcher.BeginInvoke((ThreadStart)delegate() { ErrorLabelExists.Visibility = Visibility.Visible; }, System.Windows.Threading.DispatcherPriority.Normal);
            Thread.Sleep(TimeSpan.FromSeconds(3));
            this.Dispatcher.BeginInvoke((ThreadStart)delegate() { ErrorLabelExists.Visibility = Visibility.Hidden; }, System.Windows.Threading.DispatcherPriority.Normal);
        }

        private void ShowRequiredFieldWarning(object obj)
        {
            TextBlock temp = obj as TextBlock;
            this.Dispatcher.BeginInvoke((ThreadStart)delegate() { temp.Visibility = Visibility.Visible; }, System.Windows.Threading.DispatcherPriority.Normal);
            Thread.Sleep(TimeSpan.FromSeconds(5));
            this.Dispatcher.BeginInvoke((ThreadStart)delegate() { temp.Visibility = Visibility.Hidden; }, System.Windows.Threading.DispatcherPriority.Normal);
        }
    }
}
