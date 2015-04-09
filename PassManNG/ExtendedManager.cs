//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;
//using System.Threading;
//using System.Globalization;
//using System.Security.Cryptography;
//using System.IO;
//using System.Collections;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.Runtime.Serialization;
//using System.Reflection;

//// For Open File Dialog
//using Microsoft.Win32;

//namespace PassManNG
//{
//    /// <summary>
//    /// Interaction logic for MainWindow.xaml
//    /// </summary>
//    public partial class MainWindow : Window
//    {
//        private void ManageMenu(object sender, RoutedEventArgs e)
//        {
//            // GLOBAL ACTIONS
//            // Manage unresolved situations
//            //ManageLastWorkspace(CurrentWorkspace);

//            // AÇÕES ESPECÍFICAS:
//            // FILE:
//            // New Profile
//            // if (sender == NewProfile || sender == LoginScreenNewProfile)
//            //if (sender == LoginScreenNewProfile)
//            //{
//            //    ManageGrids("NewProfile");
//            //}
//            // Forgot password
//            //if (sender == forgotPasswordButton)
//            //{
//            //    ManageGrids("ForgotLogin");
//            //}
//            // Cancel forgot password
//            //if (sender == CancelForgotLogin)
//            //{
//            //    ManageGrids("FirstScreen");
//            //}
//            // Manage Profile
//            //if (sender == ManageProfile)
//            //{
//            //    ManageGrids("ManageProfile");
//            //}
//            // Delete Profile
//            //if (sender == DeleteProfileMenu)
//            //{
//            //    ManageGrids("DeleteProfile");
//            //}
//            // Login
//            //if (sender == LoginMenu)
//            //{
//            //    ManageGrids("Login");
//            //}
//            // Exit
//            //if (sender == Exit)
//            //{
//            //    this.Close();
//            //}
//            // Set
//            //if (sender == SetItem || sender == AddBigButton)
//            //{
//            //    ManageGrids("Set");
//            //    //ManageBottomLabel("Set");
//            //}
//            // Get
//            //if (sender == GetItem || sender == SearchBigButton)
//            //{
//            //    TempList.Clear();
//            //    //MyGetListBox.Items.Refresh();
//            //    // Fills temporary list used for display
//            //    Lista.ForEach((item) => { if (item.Owner == CurrentProfile.Username) { TempList.Add((Items)item.ShallowCopy()); } });
//            //    MyGetListBox.ItemsSource = TempList;
//            //    MyGetListBox.Items.Refresh();
//            //    ManageGrids("Get");
//            //    //ManageBottomLabel("Get");
//            //}
//            // Keep Above
//            //if (sender == KeepAboveItem)
//            //{
//            //    // Toggles Windows top most property
//            //    MyMainWindow.Topmost = !MyMainWindow.Topmost;
//            //}
//            // Import old keyring
//            if (sender == ImportKeyringItem)
//            {
//                OpenFileDialog openfile = new OpenFileDialog();
//                openfile.FileName = "keyring";
//                openfile.DefaultExt = ".ker";
//                openfile.Filter = "keyring legacy files (.ker)|*.ker";

//                Nullable<bool> result = openfile.ShowDialog();

//                if (result == true)
//                {
//                    // Counter for number of added passwords
//                    int counter = 0;

//                    String filename = openfile.FileName;

//                    // Open file
//                    FileStream fs = new FileStream(filename, FileMode.Open);
//                    BinaryFormatter binForm = new BinaryFormatter();

//                    Hashtable ht = (Hashtable)binForm.Deserialize(fs);

//                    String displayString = (String)MyMainWindow.TryFindResource("OpenKeyringpt1");
//                    displayString = displayString + " " + Convert.ToString(ht.Count) + " " + (String)MyMainWindow.TryFindResource("OpenKeyringpt2");
//                    MessageBoxResult result2 = MessageBox.Show(displayString, (String)MyMainWindow.TryFindResource("OpenKeyringTitle"), MessageBoxButton.YesNo);

//                    if (result2 == MessageBoxResult.Yes)
//                    {
//                        // Import passwords under current user ownership with null user
//                        foreach (String key in ht.Keys)
//                        {
//                            Items novoItem = new Items(key, MyGlobals.CurrentProfile.Username, (String)ht[key], null, (String)MyMainWindow.TryFindResource("ImportedNote"));

//                            // Se não existir
//                            if (!MyGlobals.Lista.Exists(d => d.DirectKey == key))
//                            {
//                                // Add to Local List
//                                MyGlobals.Lista.Add(novoItem);

//                                // Add to DB
//                                App.PassDB.UpItem(novoItem);

//                                counter++;
//                            }
//                        }
//                    }
//                    fs.Close();
//                    MessageBox.Show(Convert.ToString(counter) + " " + (String)MyMainWindow.TryFindResource("OpenKeyringSuccess"), (String)MyMainWindow.TryFindResource("OpenKeyringSuccessTitle"), MessageBoxButton.YesNo);
//                }
//            }
//            // Master Password
//            //if (sender == MasterPasswordItem)
//            //{
//            //    ManageGrids("MPassword");
//            //    ManageBottomLabel("Set");
//            //}
//            // Clear all
//            if (sender == AllItem)
//            {
//                string str1 = (String)MyMainWindow.TryFindResource("AllWarning");
//                string str2 = (String)MyMainWindow.TryFindResource("AllWarningTitle");
//                MessageBoxResult result = MessageBox.Show(str1, str2, MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

//                if (result == MessageBoxResult.Yes)
//                {
//                    // Clear local list
//                    MyGlobals.Lista.Clear();

//                    // Clear DB
//                    App.PassDB.RemoveAllItens();
//                }

//            }
//            //if (sender == ShowList)
//            //{
//            //    ManageGrids("ShowList");
//            //    ManageBottomLabel("ShowList");
//            //    //MyDataGrid.DataContext = Lista;
//            //    MyListBox.ItemsSource = Lista;
//            //    // Unselect items at the listbox
//            //    MyListBox.SelectedIndex = -1;
//            //    MyListBox.Items.Refresh();
//            //}
//        }

//        // Exiting from the current grid through the menu might leave items, textboxes or others in a pending situation awaiting for resolution. ManageLastWorkspace will then be called whenever a menu item is used in order to manage pending situations. Leaving a menu through option buttons is always a "clear exit" because option buttons perform the required actions to that Grid data. "CANCEL" buttons might also call this function because their actions is usually the same as leaving the grid through menu items: -discard textbox data; -put temporary items back into the lists; -clear temporary items; etc
//        //public void ManageLastWorkspace(string LastWorkspace)
//        //{
//        //    switch (LastWorkspace)
//        //    {
//        //        case "Set":
//        //            if (TemporaryItem.DirectKey != String.Empty)
//        //            {
//        //                // Add Temporary item back to the List
//        //                Lista.Add(TemporaryItem);
//        //                // Clear Temporary Item
//        //                TemporaryItem = Items.Empty();
//        //            }
//        //            break;
//        //    }
//        //}

//        public void ManageGrids(string WorkArea)
//        {
//            //// Esconde todas as grelhas
//            //GridGet.Visibility = Visibility.Hidden;
//            //GridMasterPassword.Visibility = Visibility.Hidden;
//            //GridSet.Visibility = Visibility.Hidden;
//            //GridSetPassword.Visibility = Visibility.Hidden;
//            //GridPasswordRetrieved.Visibility = Visibility.Hidden;
//            //GridShowList.Visibility = Visibility.Hidden;
//            //GridNewProfile.Visibility = Visibility.Hidden;
//            //GridManageProfile.Visibility = Visibility.Hidden;
//            //GridLoginProfile.Visibility = Visibility.Hidden;
//            //GridDeleteProfile.Visibility = Visibility.Hidden;

//            //// Hide big buttons
//            //SearchBigButton.Visibility = Visibility.Hidden;
//            //AddBigButton.Visibility = Visibility.Hidden;
//            //HelpBigButton.Visibility = Visibility.Hidden;

//            //// Hide other initial buttons
//            //forgotPasswordButton.Visibility = Visibility.Hidden;
//            //LoginScreenNewProfile.Visibility = Visibility.Hidden;

//            switch (WorkArea)
//            {
//                case "Home":
//                    //// Show "big buttons"
//                    //SearchBigButton.Visibility = Visibility.Visible;
//                    //AddBigButton.Visibility = Visibility.Visible;
//                    //HelpBigButton.Visibility = Visibility.Visible;
//                    break;
//                case "Set":
//                    // Mostra grelha devida
//                    //GridSet.Visibility = Visibility.Visible;

//                    // Carry information from Edit button
//                    //if (TemporaryItem.DirectKey != String.Empty)
//                    //{
//                    //    // Pre-fill TextBoxes
//                    //    LabelTextBox.Text = TemporaryItem.DirectKey;
//                    //    PasswordTextBox.Password = TemporaryItem.Password;
//                    //    LoginTextBox.Text = TemporaryItem.Username;
//                    //    NotesTextBox.Text = TemporaryItem.Notes;
//                    //}                    
//                    //else
//                    //{
//                    //    // Clears TextBoxes
//                    //    LabelTextBox.Text = String.Empty;
//                    //    PasswordTextBox.Password = String.Empty;
//                    //    LoginTextBox.Text = String.Empty;
//                    //    NotesTextBox.Text = String.Empty;
//                    //}
//                    break;
//                case "SetPassword":
//                    // Mostra grelha devida
//                    //GridSetPassword.Visibility = Visibility.Visible;
//                    break;
//                case "Get":
//                    // Mostra grelha devida
//                    //GridGet.Visibility = Visibility.Visible;
//                    // Other actions relative to this grid
//                    // Enable action buttons
//                    //EditButton.IsEnabled = false;
//                    //CopyToClipboardButtonOnGetScreen.IsEnabled = false;
//                    //DeleteButtonOnGetScreen.IsEnabled = false;

//                    break;
//                case "MPassword":
//                    // Mostra grelha devida
//                    //GridMasterPassword.Visibility = Visibility.Visible;
//                    break;
//                case "PasswordRetrieved":
//                    // Mostra grelha devida
//                    //GridPasswordRetrieved.Visibility = Visibility.Visible;
//                    break;
//                case "ShowList":
//                    // Mostra grelha devida
//                    //GridShowList.Visibility = Visibility.Visible;
//                    break;
//                case "NewProfile":
//                    // Esconde objectos do ecra inicial
//                    //LoginScreenLogin.Visibility = Visibility.Hidden;
//                    //LoginScreenLoginButton.Visibility = Visibility.Hidden;
//                    //LoginScreenPassword.Visibility = Visibility.Hidden;
//                    //LoginScreenNewProfile.Visibility = Visibility.Hidden;

//                    //GridNewProfile.Visibility = Visibility.Visible;
//                    break;
//                case "ForgotLogin":
//                    // Esconde objectos do ecra inicial
//                    //LoginScreenLogin.Visibility = Visibility.Hidden;
//                    //LoginScreenLoginButton.Visibility = Visibility.Hidden;
//                    //LoginScreenPassword.Visibility = Visibility.Hidden;
//                    //LoginScreenNewProfile.Visibility = Visibility.Hidden;

//                    //GridForgotLogin.Visibility = Visibility.Visible;
//                    break;
//                case "ManageProfile":
//                    // Retrieve information from Profiles DB
//                    Items profile = App.PassDB.GetProfile("Profile");
//                    // Pre-fill TextBoxes
//                    if (profile != null)
//                    {
//                        ManageProfilePasswordTextBox.Text = profile.Password;
//                        ManageProfileUserTextBox.Text = profile.Username;
//                    }

//                    GridManageProfile.Visibility = Visibility.Visible;
//                    break;
//                case "DeleteProfile":
//                    if (MyGlobals.CurrentProfile != null)
//                    {
//                        DeleteProfileTextBox.Text = MyGlobals.CurrentProfile.Username;
//                        DeletePasswordTextBox.Text = MyGlobals.CurrentProfile.Password;

//                        GridDeleteProfile.Visibility = Visibility.Visible;
//                    }
//                    else
//                    {
//                        MessageBox.Show((String)MyMainWindow.TryFindResource("NoLoginWarning"));
//                    }
//                    break;
//                case "Login":
//                    //GridLoginProfile.Visibility = Visibility.Visible;
//                    break;
//                case "FirstScreen":
//                    // Mostra objectos do ecra inicial
//                    //LoginScreenLogin.Visibility = Visibility.Visible;
//                    //LoginScreenLoginButton.Visibility = Visibility.Visible;
//                    //LoginScreenPassword.Visibility = Visibility.Visible;
//                    //LoginScreenNewProfile.Visibility = Visibility.Visible;

//                    // Hide text and button
//                    CancelForgotLogin.Visibility = Visibility.Hidden;
//                    NotImplementedText.Visibility = Visibility.Hidden;
//                    break;
//            }

//            // Update current workspace
//            CurrentWorkspace = WorkArea;
//        }

//        //public void ManageBottomLabel(string WorkArea)
//        //{
//        //    switch (WorkArea)
//        //    {
//        //        case "Get":
//        //            bottom_label.Content = (String)MyMainWindow.TryFindResource("GetLabel");
//        //            break;
//        //        case "SetPassword":
//        //            bottom_label.Content = (String)MyMainWindow.TryFindResource("SetPasswordLabel");
//        //            break;
//        //        case "Set":
//        //            bottom_label.Content = (String)MyMainWindow.TryFindResource("SetLabel");
//        //            break;
//        //        case "WaitingPassword":
//        //            bottom_label.Content = (String)MyMainWindow.TryFindResource("SetPasswordLabel");
//        //            break;
//        //        case "PasswordInserted":
//        //            bottom_label.Content = (String)MyMainWindow.TryFindResource("PasswordInsertedLabel");
//        //            break;
//        //        case "PasswordRetrieved":
//        //            bottom_label.Content = (String)MyMainWindow.TryFindResource("PasswordRetrievedLabel");
//        //            break;
//        //        case "EmptyLabel":
//        //            bottom_label.Content = String.Empty;
//        //            break;
//        //        case "ShowList":
//        //            bottom_label.Content = (String)MyMainWindow.TryFindResource("ListShowingLabel");
//        //            break;
//        //    }
//        //}

//        //public void ManageImageTooltips(bool state, string updatedTooltip = "", string language = "")
//        //{
//        //    // State Image
//        //    if (state)
//        //    {
//        //        CurrentStateImage.ToolTip = (String)MyMainWindow.TryFindResource("ImageOK") + " " + updatedTooltip;
//        //        CurrentStateImage.Source = new BitmapImage(new Uri(@"/positive.jpg", UriKind.Relative));

//        //    }
//        //    else
//        //    {
//        //        CurrentStateImage.ToolTip = (String)MyMainWindow.TryFindResource("ImageNOTOK");
//        //        CurrentStateImage.Source = new BitmapImage(new Uri(@"/negative.jpg", UriKind.Relative));
//        //    }

//        //    //Language Image
//        //    if (language == "en-US")
//        //    {
//        //        ChangeLanguage.ToolTip = (String)MyMainWindow.TryFindResource("LanguageEnglish");
//        //    }
//        //    if(language == "pt-PT")
//        //    {
//        //        ChangeLanguage.ToolTip = (String)MyMainWindow.TryFindResource("LanguagePortuguese");
//        //    }
//        //}
//    }
//}