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
using System.Data.SQLite;
using System.Net;

namespace PassManNG
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //NavigationService nav;
        // A few variables.
        static readonly string PasswordHash = "P@@Sw0rd";
        static readonly string SaltKey = "S@LT&KEY";
        static readonly string VIKey = "@1B2c3D4e5F6g7H8";

        public static void SelectCulture(string culture)
        {
            // List all our resources      
            List<ResourceDictionary> dictionaryList = new List<ResourceDictionary>();
            foreach (ResourceDictionary dictionary in Application.Current.Resources.MergedDictionaries)
            {
                dictionaryList.Add(dictionary);
            }
            // We want our specific culture      
            string requestedCulture = string.Format("/Strings/{0}/Resources.xaml", culture);
            ResourceDictionary resourceDictionary = dictionaryList.FirstOrDefault(d => d.Source.OriginalString == requestedCulture);
            if (resourceDictionary == null)
            {
                // If not found, we select our default language        
                //        
                requestedCulture = "Strings/Resources.xaml";
                resourceDictionary = dictionaryList.FirstOrDefault(d => d.Source.OriginalString == requestedCulture);
            }

            // If we have the requested resource, remove it from the list and place at the end.\      
            // Then this language will be our string table to use.      
            if (resourceDictionary != null)
            {
                Application.Current.Resources.MergedDictionaries.Remove(resourceDictionary);
                Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
            }
            // Inform the threads of the new culture      
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
        }
        
        public static string EncryptString(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);

            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;
            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);

        }

        public static string DecryptString(string encryptedText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }

        // Main Window Associated Functions
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Selects Culture
            SelectCulture(Properties.Settings.Default.Language);
            // SelectCulture("pt-PT");

            // Verify if DB exists
            CreateDB();

            App.NewWindow = this;
        }
        
        private void SuperFrame_Loaded(object sender, RoutedEventArgs e)
        {
            //nav = NavigationService.GetNavigationService(this);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            
            App.Current.Shutdown();
        }

        private void NewProfile_Click(object sender, RoutedEventArgs e)
        {
            MyGlobals.GlobalMethods.LogOut();
            SuperFrame.Navigate(new System.Uri("NewProfile.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ManageProfile_Click(object sender, RoutedEventArgs e)
        {
            if (MyGlobals.CurrentProfile != null)
            {
                MyGlobals.TemporaryItem = MyGlobals.CurrentProfile;
            }

            SuperFrame.Navigate(new System.Uri("NewProfile.xaml", UriKind.RelativeOrAbsolute));
        }

        private void CheckForUpdates_Click(object sender, RoutedEventArgs e)
        {
            //WebRequest request = WebRequest.Create("http://www.andreteixeiragomes.com/PassManNG/CurrentVersion.txt");

            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            WebClient client = new WebClient();
            try
            {
                Stream stream = client.OpenRead("http://www.andreteixeiragomes.zz.vc/PassManNG/CurrentVersion.txt");
                StreamReader reader = new StreamReader(stream);
                String content = reader.ReadToEnd();

                String CurrentVersion = new String(content.Where(Char.IsDigit).ToArray());
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                String version = assembly.GetName().Version.ToString();

                String ThisVersion = new String(version.Where(Char.IsDigit).ToArray());

                int CurrentVersionNumber = Convert.ToInt32(CurrentVersion);
                int ThisVersionNumber = Convert.ToInt32(ThisVersion);

                if (ThisVersionNumber >= CurrentVersionNumber)
                {
                    // Programa está actualizado
                    MessageBox.Show("This software is updated.");
                }
                else
                {
                    // Programa está desactualizado
                    MessageBox.Show("This software is outdated.");
                }
            }
            catch (WebException)
            {
                MessageBox.Show("It was not possible to connect. Please try again later or visit http://www.andreteixeiragomes.zz.vc/PassManNG/.");
            }
            catch (Exception ex)
            {
                String output = String.Format("An error ocurred. If this problem persists contact developer. Error: {0}", ex.ToString());
                MessageBox.Show(output);
            }
        }

        public static void ShowPopUp(object currentPage)
        {
            MainWindow page = (MainWindow)currentPage;
            page.Dispatcher.BeginInvoke((ThreadStart)delegate() { App.NewWindow.FloatLabelGrid.Visibility = Visibility.Visible; }, System.Windows.Threading.DispatcherPriority.Normal);
            Thread.Sleep(TimeSpan.FromSeconds(3));
            page.Dispatcher.BeginInvoke((ThreadStart)delegate() { App.NewWindow.FloatLabelGrid.Visibility = Visibility.Hidden; }, System.Windows.Threading.DispatcherPriority.Normal);
        }

        public static void EnablesMenuFeatures()
        {
            MainWindow window = (MainWindow)App.Current.MainWindow;

            window.ManageProfile.IsEnabled = true;
            window.DeleteProfileMenu.IsEnabled = true;
            window.OptionsMenuItem.Visibility = Visibility.Visible;
        }

        private void KeepAboveItem_Checked(object sender, RoutedEventArgs e)
        {
            MyMainWindow.Topmost = true;
        }

        private void ImportKeyringItem_Click(object sender, RoutedEventArgs e)
        {
            MyGlobals.GlobalMethods.ImportKeyring();
        }

        private void KeepAboveItem_Unchecked(object sender, RoutedEventArgs e)
        {
            MyMainWindow.Topmost = false;
        }

        private void DeleteProfileMenu_Click(object sender, RoutedEventArgs e)
        {
            if (MyGlobals.CurrentProfile != null)
            {
                App.PassDB.RemoveProfile(MyGlobals.CurrentProfile.Username);
                App.PassDB.RemoveAllItens(MyGlobals.CurrentProfile.Username);

                MyGlobals.CurrentProfile = null;
                MyGlobals.TemporaryItem = Items.Empty();

                SuperFrame.Navigate(new System.Uri("LoginPage.xaml", UriKind.RelativeOrAbsolute));
            }
        }

        private void PreferencesItem_Click(object sender, RoutedEventArgs e)
        {
            SuperFrame.Navigate(new System.Uri("PreferencesPage.xaml", UriKind.RelativeOrAbsolute));
        }

    }
}
