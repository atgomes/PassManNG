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
using System.IO;
using System.Reflection;

namespace PassManNG
{
    /// <summary>
    /// Interaction logic for Help.xaml
    /// </summary>
    public partial class Help : Page
    {
        public Help()
        {
            InitializeComponent();
        }
        NavigationService nav;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            nav = NavigationService.GetNavigationService(this);

            ReadFile();
        }

        private async void ReadFile()
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "PassManNG.HelpFile.txt";
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        String text = await reader.ReadToEndAsync();
                        DisplayHelp.Text = text;
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayHelp.Text = String.Empty;
            }
        }

        private void GoHome_Click(object sender, RoutedEventArgs e)
        {
            nav.Navigate(new System.Uri("InitialPage.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
