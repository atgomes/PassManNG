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

//namespace PassManNG
//{
//    public partial class MainWindow : Window
//    {
//        public int Login(string username, string password)
//        {
//            Items profileRequested = App.PassDB.GetProfile(username);
//            if (profileRequested == null)
//            {
//                // Failed Login!
//                //ManageImageTooltips(false);
//                MessageBox.Show((String)MyMainWindow.TryFindResource("NoProfileWarning"));
//                return (-1);
//            }
//            else
//            {
//                if (profileRequested.Password != password)
//                {
//                    // Failed Login!
//                    //ManageImageTooltips(false);
//                    MessageBox.Show((String)MyMainWindow.TryFindResource("WrongPasswordWarning"));
//                    return (-1);
//                }
//                else
//                {
//                    // Successfull Login
//                    MyGlobals.CurrentProfile = profileRequested;
//                    //ManageImageTooltips(true, username);

//                    // Unlock Password related options
//                    PasswordMenuItem.Visibility = Visibility.Visible;
//                    OptionsMenuItem.Visibility = Visibility.Visible;
//                    return (0);
//                }
//            }
//        }
//    }
//}
