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

namespace PassManNG
{
    public partial class MainWindow : Window
    {
        // Create new profile
        //private void CreateProfileButton_Click(object sender, RoutedEventArgs e)
        //{
        //    AddProfile();
        //    // Set login screen objects as visible
        //    LoginScreenLogin.Visibility = Visibility.Visible;
        //    LoginScreenLoginButton.Visibility = Visibility.Visible;
        //    LoginScreenPassword.Visibility = Visibility.Visible;
        //    LoginScreenNewProfile.Visibility = Visibility.Visible;


        //    //ManageGrids("Home");
        //}

        // Edit existing profile
        //private void ValidateProfileButton_Click(object sender, RoutedEventArgs e)
        //{
        //    UpdateProfile();
        //    //ManageGrids("Home");
        //}

        // Delete current profile
        //private void DeleteProfileButton_Click(object sender, RoutedEventArgs e)
        //{
        //    DeleteProfile();
        //    MyGlobals.CurrentProfile = null;
        //    //ManageGrids("Home");
        //}
    }
}
