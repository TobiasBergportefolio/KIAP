using KiAP_projekt.View;
using KiAP_projekt.View.Login;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace KiAP_projekt
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //The try statement execute the verify user bool method in the LoginDialog xaml code-behind. If its returns true, the button DialogResult is set true.
            try
            {
                // When the verify user bool method return true, the DialogResult is set true. This opens MainWindow through the method ShowDialog
                if (new LoginDialog().ShowDialog() == true)
                {
                    
                    new MainWindow().ShowDialog();
                }
                else
                {
                    // If the method ShowDialog does not get exectued. For exsample if cancel button or close window get activated.
                    MessageBox.Show("Login mislykkedes!");
                }
            }

            // The finally statement make sure that the application shuts down. The shutdown methods has been enable through the ShutdownMode="OnExplicitShutdown", which give the application a possiblity to shutdown the application in a normal window>
            finally
            {
                Shutdown();
            }
        }
    }
}
