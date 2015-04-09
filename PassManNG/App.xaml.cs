using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;
using System.Globalization;
using System.IO;
using System.Windows.Threading;

namespace PassManNG
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static PassDB passDB = new PassDB();
        public static PassDB PassDB
        {
            get { return passDB; }
        }

        private static MainWindow newwindow = new MainWindow();
        public static MainWindow NewWindow
        {
            get { return newwindow; }
            set { newwindow = value; }
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            

            e.Handled = true;
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            DateTime data1 = File.GetLastWriteTimeUtc("databaseFile.db3");
            DateTime data2 = File.GetLastWriteTimeUtc(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\PassManNG\\databaseFile.db3");

            if (DateTime.Compare(data1, data2) >= 0)
            {
                File.Copy("databaseFile.db3", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\PassManNG\\databaseFile.db3", true);
            }

        }
    }

}
