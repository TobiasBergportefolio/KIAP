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
using KiAP_projekt.ViewModel;



namespace KiAP_projekt.View
{
    /// <summary>
    /// Interaction logic for CreateMemberDialog.xaml
    /// </summary>
    public partial class CreateMemberDialog : Window
    {
        //We create an instance of createMemberViewModel in a private Field.
        //This function does not need to be used outside of the codebehind.
        private CreateMemberViewModel createMemberViewModel;
        
        private static Random random = new Random();

        //Instantiating a new object of createMemberViewModel
        //and assigning the datacontext to the createMemberViewModel object.
        public CreateMemberDialog()
        {
            InitializeComponent();
            createMemberViewModel = new CreateMemberViewModel();
            DataContext = createMemberViewModel;
        }

        //This function is for creating a member.
        private void BtnCreateMember_Click(object sender, RoutedEventArgs e)
        {
            //Using an instance of the random class we generate a password
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] stringChars = new char[12];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            var finalString = new string(stringChars);

            //We create variables to hold the textbox values
            string name = TbMemberName.Text;
            string password = finalString;
            string phoneNumber = TbPhoneNumber.Text;
            string email = TbEmail.Text;
          
            //We use a try catch statement to catch the exception thrown if the email is invalid
            try
            {
                //We send an email to the new member and create the member with the user values
                createMemberViewModel.SendMemberMail(name, password, email);
                MessageBox.Show("Medlem blev oprettet");
                createMemberViewModel.CreateMember(name, password, phoneNumber, email);
                this.DialogResult = true;
            }
            catch
            {
                MessageBox.Show("Email er ugyldig");
            }
        }

        //Checks if it's possible to Create Member.
        private void CheckCreateButton()
        {
            //This checks if the textboxes are empty and if they aren't the CreateMember button is enabled.
            if (BtnCreateMember != null)
            {
                BtnCreateMember.IsEnabled = !(TbMemberName.Text == "" || TbPhoneNumber.Text == "" || TbEmail.Text == "");
            }
        }

        //The following 3 method checks if the Create member button should be enabled
        //whenever text is changed in any of the textboxes
        private void TbMemberName_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckCreateButton();
        }

        private void TbPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckCreateButton();
        }

        private void TbEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckCreateButton();
        }

        //Deletes the format example text when double-clicking in textbox and resets the style
        private void TbEmail_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TbEmail.Text = "";
            TbEmail.Foreground = Brushes.Black;
            TbEmail.FontStyle = FontStyles.Normal;
        }
    }
}
