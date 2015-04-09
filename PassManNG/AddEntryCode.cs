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
        //public void AddEntry()
        //{
        //    // Check if there's a logged in user
        //    if (MyGlobals.CurrentProfile == null)
        //    {

        //    }
        //    else
        //    {
        //        // Rotina de Validação TO DO!
        //        string dKey = LabelTextBox.Text;
        //        string owner = MyGlobals.CurrentProfile.Username;
        //        string ePass = EncryptString(PasswordTextBox.Password);
        //        string user = LoginTextBox.Text;
        //        string notes = NotesTextBox.Text;

        //        Items novoItem = new Items(dKey, owner, ePass, user, notes);

        //        // Add to Local List
        //        MyGlobals.Lista.Add(novoItem);

        //        // Add to DB
        //        App.PassDB.UpItem(novoItem);
        //    }
        //}

        //public void AddProfile()
        //{
        //    // Rotina de Validação TO DO!
        //    string dKey = "Profile";
        //    string owner = NewProfileUserTextBox.Text;
        //    string ePass = EncryptString(NewProfilePasswordTextBox.Text);
        //    string user = NewProfileUserTextBox.Text;
        //    string notes = "Access Profile";

        //    Items novoProfile = new Items(dKey, owner, ePass, user, notes);

        //    // Add to Local List
        //    //Lista.Add(novoItem);

        //    // Add to DB
        //    App.PassDB.UpProfile(novoProfile);
        //}

        //public void UpdateProfile()
        //{
        //    if (MyGlobals.CurrentProfile != null)
        //    {
        //        App.PassDB.RemoveProfile(MyGlobals.CurrentProfile.Username);
        //        // Rotina de Validação TO DO!
        //        string dKey = "Profile";
        //        string ePass = EncryptString(ManageProfilePasswordTextBox.Text);
        //        string user = ManageProfileUserTextBox.Text;
        //        string notes = "Access Profile";

        //        Items novoProfile = new Items(dKey, "", ePass, user, notes);

        //        // Add to Local List
        //        //Lista.Add(novoItem);

        //        // Add to DB
        //        App.PassDB.UpProfile(novoProfile);

        //        // Update passwords owned by the old profile
        //        App.PassDB.UpdatePasswordsByField("profile", novoProfile.Username, MyGlobals.CurrentProfile.Username);

        //        // Update current profile
        //        MyGlobals.CurrentProfile = novoProfile;

        //        // Update image tooltip
        //        //ManageImageTooltips(true, CurrentProfile.Username);
        //    }
        //    else
        //    {
        //        MessageBox.Show((String)MyMainWindow.TryFindResource("NoLoginWarning"));
        //    }
        //}
        //public void DeleteProfile()
        //{
        //    if (MyGlobals.CurrentProfile != null)
        //    {
        //        App.PassDB.RemoveProfile(MyGlobals.CurrentProfile.Username);
        //    }
        //    else
        //    {
        //        MessageBox.Show((String)MyMainWindow.TryFindResource("NoLoginWarning"));
        //    }
        //}
    }
}