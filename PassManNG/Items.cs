using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace PassManNG
{
    public class Items : INotifyPropertyChanged
    {
        private string directKey;
        private string owner;
        private string encryptedPassword;
        //private string password;
        private string username;
        private string notes;

        public string DirectKey
        {
            get {return directKey;}
            set {
                directKey = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DirectKey"));
            }
        }
        public string Owner
        {
            get { return owner; }
            set
            {
                owner = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Owner"));
            }
        }
        public string EncryptedPassword
        {
            get { return encryptedPassword; }
            set { 
                encryptedPassword = value;
                OnPropertyChanged(new PropertyChangedEventArgs("EncryptedPassword"));
            }
        }
        public string Password
        {
            get { return MainWindow.DecryptString(encryptedPassword); }
            set { 
                EncryptedPassword = MainWindow.EncryptString(value);
                OnPropertyChanged(new PropertyChangedEventArgs("Password"));
            }
        }
        public string Username
        {
            get { return username; }
            set { 
                username = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Username"));
            }
        }
        public string Notes
        {
            get { return notes; }
            set { 
                notes = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Notes"));
            }
        }
        public Items(string directKey, string owner, string encryptedPassword, string username=null,string notes=null)
        {
            DirectKey = directKey;
            Owner = owner;
            EncryptedPassword = encryptedPassword;
            Username = username;
            Notes = notes;
        }

        // Cloning Method
        public Items ShallowCopy()
        {
                return (Items)this.MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            // Return false if obj is null
            if (obj == null)
            {
                return false;
            }

            // Return false if obj can't be cast as Items
            Items temp = obj as Items;
            if ((Items)temp == null)
            {
                return false;
            }

            return (directKey == temp.DirectKey) && (owner == temp.Owner) && (encryptedPassword == temp.EncryptedPassword) && (username == temp.Username);

        }

        // Creation of an empty object
        public static Items Empty()
        {
            return (new Items(String.Empty, String.Empty, String.Empty));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

    }
}
