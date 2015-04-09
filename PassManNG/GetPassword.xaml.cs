using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
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
    /// Interaction logic for GetPassword.xaml
    /// </summary>
    public partial class GetPassword : Page
    {
        public GetPassword()
        {
            InitializeComponent();
        }

        NavigationService nav;

        /// <summary>
        /// Copies the password in the textbox to the clipboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyToClipboardButtonOnGetScreen_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetText(passwordtextbox.Text);

            Thread thread = new Thread(ShowRectangleCopied);
            thread.Start();
        }

        /// <summary>
        /// Changes the floating rectangle saying password has been copied Visibility to "Visible".
        /// </summary>
        private void ShowRectangleCopied()
        {
            this.Dispatcher.BeginInvoke((ThreadStart)delegate() { FloatLabelGrid.Visibility = Visibility.Visible; }, System.Windows.Threading.DispatcherPriority.Normal);
            Thread.Sleep(TimeSpan.FromSeconds(3));
            this.Dispatcher.BeginInvoke((ThreadStart)delegate() { FloatLabelGrid.Visibility = Visibility.Hidden; }, System.Windows.Threading.DispatcherPriority.Normal);
        }

        /// <summary>
        /// Saves current item in a temporary object then removes the original from the list. Navigates to the "SetPassword" screen and pre-fills the textboxes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // Get selected item on the list box
            MyGlobals.TemporaryItem = (Items)MyGetListBox.SelectedItem;

            // Find and removes item on the original List
            MyGlobals.Lista.Remove(MyGlobals.Lista.Find(x => x.DirectKey == MyGlobals.TemporaryItem.DirectKey));

            nav.Navigate(new System.Uri("SetPassword.xaml", UriKind.RelativeOrAbsolute));
        }

        /// <summary>
        /// Deletes the selected entry by deleting it from the list, database and temporary list. Then updates the temporary list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteSelectedEntry(object sender, RoutedEventArgs e)
        {
            // ATENTION - TO DO: "are you sure dialog"
            // ---

            // Get selected item on the list box
            MyGlobals.TemporaryItem = (Items)MyGetListBox.SelectedItem;

            // Find and removes item on the original List
            MyGlobals.Lista.Remove(MyGlobals.Lista.Find(x => x.DirectKey == MyGlobals.TemporaryItem.DirectKey));

            // Removes item from the DB
            App.PassDB.RemoveItem(MyGlobals.TemporaryItem.DirectKey);

            // Clear current temp list
            MyGlobals.TempList.Clear();

            // Fills temporary list used for display
            MyGlobals.Lista.ForEach((item) => { if (item.Owner == MyGlobals.CurrentProfile.Username) { MyGlobals.TempList.Add((Items)item.ShallowCopy()); } });
            MyGetListBox.ItemsSource = MyGlobals.TempList;
            MyGetListBox.Items.Refresh();
            //ManageGrids("Get");
        }

        /// <summary>
        /// When search textbox gains focus it clears the current text.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;

            textbox.Clear();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            nav = NavigationService.GetNavigationService(this);

            //
            MyGlobals.TempList.Clear();
            //MyGetListBox.Items.Refresh();
            // Fills temporary list used for display
            MyGlobals.Lista.ForEach((item) => { if (item.Owner == MyGlobals.CurrentProfile.Username) { MyGlobals.TempList.Add((Items)item.ShallowCopy()); } });
            MyGetListBox.ItemsSource = MyGlobals.TempList;
            MyGetListBox.Items.Refresh();
            //ManageGrids("Get");
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            MyGlobals.TempList.Clear();
            //Lista.ForEach((item) => { TempList.Add((Items)item.ShallowCopy()); });
            for (int i = 0; i < MyGlobals.Lista.Count; i++)
            {
                if (MyGlobals.Lista[i].DirectKey.StartsWith(searchTextBox.Text, System.StringComparison.CurrentCultureIgnoreCase))
                {
                    if (MyGlobals.Lista[i].Owner == MyGlobals.CurrentProfile.Username)
                    {
                        MyGlobals.TempList.Add((Items)MyGlobals.Lista[i].ShallowCopy());
                    }
                }
            }
            MyGetListBox.Items.Refresh();
        }

        private void EnableActionButtons(object sender, SelectionChangedEventArgs e)
        {
            if (MyGetListBox.SelectedIndex > -1)
            {
                EditButton.Visibility = Visibility.Visible;
                CopyToClipboardButtonOnGetScreen.Visibility = Visibility.Visible;
                DeleteButtonOnGetScreen.Visibility = Visibility.Visible;
            }
            else
            {
                EditButton.Visibility = Visibility.Hidden;
                CopyToClipboardButtonOnGetScreen.Visibility = Visibility.Hidden;
                DeleteButtonOnGetScreen.Visibility = Visibility.Hidden;
            }
        }

        private void TextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;

            textbox.SelectAll();
        }

        private void GoHome_Click(object sender, RoutedEventArgs e)
        {
            nav.Navigate(new System.Uri("InitialPage.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
