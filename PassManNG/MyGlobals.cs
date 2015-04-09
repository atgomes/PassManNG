using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace PassManNG
{
    public static class MyGlobals
    {
        public static Items CurrentProfile = null;
        public static Items TemporaryItem = Items.Empty();

        public static List<Items> Lista = new List<Items>();
        public static List<Items> TempList = new List<Items>();

        public static class GlobalMethods
        {
            public static string GenerateRandPassword()
            {
                bool hasDigit = !Properties.Settings.Default.Sett_Pass_Digit;
                bool hasUpperCase = !Properties.Settings.Default.Sett_Pass_Casess;
                bool hasLowCase = false;
                bool hasSymbol = !Properties.Settings.Default.Sett_Pass_Symbol;

                Random randNumber = new Random();
                int sizePass = randNumber.Next(9, 24);
                // Determine maximum quantity of symbols for a good reasonable password (20% of the pass size)
                int maxSymbols = (int)Math.Round(0.2 * sizePass);
                byte[] protoPass = new byte[sizePass];
                randNumber.NextBytes(protoPass);
                for (int i = 0; i < protoPass.Length; i++)
                {
                    // keeps trying till a good value is found
                    while (protoPass[i] < 33 || protoPass[i] > 125 || protoPass[i] == 34 || protoPass[i] == 39 || protoPass[i] == 44 || protoPass[i] == 94 || protoPass[i] == 96) //fora dos limites ascii pretendidos
                    {
                        protoPass[i] = (byte)randNumber.Next(33, 125);
                    }

                    if (!Properties.Settings.Default.Sett_Pass_Symbol)
                    {
                        // keeps trying till a good value is found
                        while (protoPass[i] < 48 || protoPass[i] > 122 || (protoPass[i] > 57 && protoPass[i] < 65) || (protoPass[i] > 90 && protoPass[i] < 97)) //fora dos limites ascii pretendidos
                        {
                            protoPass[i] = (byte)randNumber.Next(48, 122);
                        }
                    }
                    if (!Properties.Settings.Default.Sett_Pass_Digit)
                    {
                        // keeps trying till a good value is found
                        while (protoPass[i] > 47 && protoPass[i] < 58) //fora dos limites ascii pretendidos
                        {
                            protoPass[i] = (byte)randNumber.Next(33, 125);
                        }
                    }
                    if (!Properties.Settings.Default.Sett_Pass_Casess)
                    {
                        // keeps trying till a good value is found
                        while (protoPass[i] < 91 && protoPass[i] > 64) //fora dos limites ascii pretendidos
                        {
                            protoPass[i] = (byte)randNumber.Next(97, 122);
                        }
                    }
                    // its a symbol
                    if ((protoPass[i] > 32 && protoPass[i] < 48) || (protoPass[i] > 57 && protoPass[i] < 65)
|| (protoPass[i] > 90 && protoPass[i] < 97))
                    {
                        maxSymbols -= 1;
                        if (maxSymbols < 0)
                        {
                            protoPass[i] = (byte)randNumber.Next(97, 122);
                        }
                        else
                        {
                            hasSymbol = true;
                        }
                    }
                    // its lowcase
                    if ((protoPass[i] > 96 && protoPass[i] < 123) && !hasLowCase)
                    {
                        hasLowCase = true;
                    }
                    // its uppercase
                    if ((protoPass[i] > 64 && protoPass[i] < 91) && !hasUpperCase)
                    {
                        hasUpperCase = true;
                    }
                    // its a digit
                    if ((protoPass[i] > 47 && protoPass[i] < 58) && !hasDigit)
                    {
                        hasDigit = true;
                    }
                    // In the last 4 positions it checks if all security need were met
                    if (i >= (protoPass.Length - 4))
                    {
                        if (!hasLowCase)
                        {
                            protoPass[i] = (byte)randNumber.Next(97, 122);
                        }
                        else
                        {
                            if (!hasUpperCase)
                            {
                                protoPass[i] = (byte)randNumber.Next(65, 90);
                            }
                            else
                            {
                                if (!hasDigit)
                                {
                                    protoPass[i] = (byte)randNumber.Next(48, 57);
                                }
                                else
                                {
                                    if (!hasSymbol)
                                    {
                                        switch (randNumber.Next(0, 2))
                                        {
                                            case 0:
                                                protoPass[i] = 64;
                                                break;
                                            case 1:
                                                protoPass[i] = 35;
                                                break;
                                            default:
                                                protoPass[i] = 33;
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                char[] semiPass = new char[sizePass];
                for (int i = 0; i < protoPass.Length; i++)
                {
                    semiPass[i] = (char)protoPass[i];
                }
                string finalPass = new string(semiPass);
                return (finalPass);
            }

            public static void LogOut()
            {
                CurrentProfile = null;
                TemporaryItem = Items.Empty();
                //Lista = null;
                //TempList = null;
            }

            public static void ImportKeyring()
            {
                OpenFileDialog openfile = new OpenFileDialog();
                openfile.FileName = "keyring";
                openfile.DefaultExt = ".ker";
                openfile.Filter = "keyring legacy files (.ker)|*.ker";

                Nullable<bool> result = openfile.ShowDialog();

                if (result == true)
                {
                    // Counter for number of added passwords
                    int counter = 0;

                    String filename = openfile.FileName;

                    // Open file
                    FileStream fs = new FileStream(filename, FileMode.Open);
                    BinaryFormatter binForm = new BinaryFormatter();

                    Hashtable ht = (Hashtable)binForm.Deserialize(fs);

                    String displayString = (String)App.Current.TryFindResource("OpenKeyringpt1");
                    displayString = displayString + " " + Convert.ToString(ht.Count) + " " + (String)App.Current.TryFindResource("OpenKeyringpt2");
                    MessageBoxResult result2 = MessageBox.Show(displayString, (String)App.Current.TryFindResource("OpenKeyringTitle"), MessageBoxButton.YesNo);

                    if (result2 == MessageBoxResult.Yes)
                    {
                        // Import passwords under current user ownership with null user
                        foreach (String key in ht.Keys)
                        {
                            Items novoItem = new Items(key, MyGlobals.CurrentProfile.Username, (String)ht[key], null, (String)App.Current.TryFindResource("ImportedNote"));

                            // Se não existir
                            if (!MyGlobals.Lista.Exists(d => d.DirectKey == key))
                            {
                                // Add to Local List
                                MyGlobals.Lista.Add(novoItem);

                                // Add to DB
                                App.PassDB.UpItem(novoItem);

                                counter++;
                            }
                        }
                    }
                    fs.Close();
                    MessageBox.Show(Convert.ToString(counter) + " " + (String)App.Current.TryFindResource("OpenKeyringSuccess"), (String)App.Current.TryFindResource("OpenKeyringSuccessTitle"), MessageBoxButton.YesNo);
                }
            }

            public static bool VerifyPasswordQuality(String suggestedPassword)
            {
                bool hasDigit = false, hasUpperCase = false, hasLowCase = false, hasSymbol = false;
                
                for(int i = 0; i < suggestedPassword.Length; i++)
                {
                    if (Char.IsDigit(suggestedPassword, i))
                    {
                        hasDigit = true;
                    }
                    if (Char.IsLower(suggestedPassword, i))
                    {
                        hasLowCase = true;
                    }
                    if (!Char.IsLetterOrDigit(suggestedPassword, i))
                    {
                        hasSymbol = true;
                    }
                    if (Char.IsUpper(suggestedPassword, i))
                    {
                        hasUpperCase = true;
                    }
                }
                return (hasDigit && hasUpperCase && hasLowCase && hasSymbol);
            }


        }
    }
}
