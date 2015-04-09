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
using System.Threading;
using System.Globalization;
using System.Security.Cryptography;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Reflection;

namespace PassManNG
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Action buttons
        // Edit button related function

        // Delete button related functions (moved to GetPassword.xaml)
        //private void DeleteSelectedEntry(object sender, RoutedEventArgs e)
        //{
        //    // ATENTION - TO DO: "are you sure dialog"
        //    // ---

        //    // Get selected item on the list box
        //    TemporaryItem = (Items)MyGetListBox.SelectedItem;

        //    // Find and removes item on the original List
        //    Lista.Remove(Lista.Find(x => x.DirectKey == TemporaryItem.DirectKey));

        //    // Removes item from the DB
        //    App.PassDB.RemoveItem(TemporaryItem.DirectKey);

        //    // Clear current temp list
        //    TempList.Clear();

        //    // Fills temporary list used for display
        //    Lista.ForEach((item) => { if (item.Owner == CurrentProfile.Username) { TempList.Add((Items)item.ShallowCopy()); } });
        //    MyGetListBox.ItemsSource = TempList;
        //    MyGetListBox.Items.Refresh();
        //    ManageGrids("Get");
        //}

        // Copy to clipboard related functions
        //private void CopyToClipboard(object sender, RoutedEventArgs e)
        //{
        //    Clipboard.Clear();
        //    Clipboard.SetText(passwordtextbox.Text);
        //}

        // List box

        // Text boxes
        // When text boxes get focus selects all text
        //private void TextBoxGotFocus(object sender, RoutedEventArgs e)
        //{
        //    TextBox textbox = sender as TextBox;

        //    textbox.SelectAll();
        //}

    }
}