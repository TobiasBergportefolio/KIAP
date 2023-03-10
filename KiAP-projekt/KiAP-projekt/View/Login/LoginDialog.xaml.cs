using KiAP_projekt.ViewModel.Login;
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
using System.Windows.Shapes;

namespace KiAP_projekt.View.Login
{
    public partial class LoginDialog : Window
    {
        private LoginDialogViewModel loginDialogViewModel;
        public LoginDialog()
        {
            InitializeComponent();
            loginDialogViewModel = new LoginDialogViewModel();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            // The buttonLogin verifies the user with the VerifyUser bool method which returns true or false.
            // If the userinput username and password equals a user with name and password in our SQL database it will return true.
            if (loginDialogViewModel.VerifyUser(tbUserName.Text, tbPassword.Text))
            {
                // If the if-statement is true it will use the GetUserID method to get the ID from the user which has logged in. And it will set the DialogResult to be true,
                // which will activate the new MainWindow().ShowDialog(); in the app xaml code-behind.
                loginDialogViewModel.GetUserID(tbUserName.Text, tbPassword.Text);
                this.DialogResult = true;
            }
            else
            {
                //If the user cannot be verifyed it will show the message
                MessageBox.Show("Brugernavn eller password er forkert");
            }
        }
    }
}
