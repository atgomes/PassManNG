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
//using System.Data.SQLite;

//namespace PassManNG
//{
//    public partial class MainWindow : Window
//    {
//        // Change Language
//        private void ChangeLanguage_Click(object sender, RoutedEventArgs e)
//        {
//            if (Thread.CurrentThread.CurrentCulture.IetfLanguageTag == "en-US")
//            {
//                SelectCulture("pt-PT");
//                Properties.Settings.Default.Language = "pt-PT";
//                Properties.Settings.Default.Save();
//                //ManageImageTooltips(false, "", Properties.Settings.Default.Language);
//            }
//            else
//            {
//                SelectCulture("en-US");
//                Properties.Settings.Default.Language = "en-US";
//                Properties.Settings.Default.Save();
//                //ManageImageTooltips(false, "", Properties.Settings.Default.Language);
//            }
//        }
//    }
//}
