using KiAP_projekt.ViewModel;
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

namespace KiAP_projekt.View
{
    /// <summary>
    /// Interaction logic for ChangeMemberDialog.xaml
    /// </summary>
    public partial class ChangeMemberDialog : Window
    {
        
        //We create an instance of changeMemberViewModel in a private Field.
        //This function does not need to be used outside of this class.
        private ChangeMemberViewModel changeMemberViewModel;

        //Instantiating the changeMemberViewModel object
        // and setting the DataContext to the changeMemberViewModel object.
        public ChangeMemberDialog()
        {
            InitializeComponent();
            changeMemberViewModel = new ChangeMemberViewModel();
            DataContext = changeMemberViewModel;
        }

        //Method to check if it's posssible to update member
        private void CheckUpdateButton()
        {
            //Checks if there is text in the three textboxes. If that's the case the update button will be enabled.
            BtnChangeMember.IsEnabled = !(TbMemberName.Text == "" || TbPhoneNumber.Text == "" || TbEmail.Text == "");
        }

        //The following three methods checks if the UpdateMemberButton should be enabled
        //whenever the text is changed in any of the textboxes.
        private void TbMemberName_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckUpdateButton();
        }

        private void TbPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckUpdateButton();
        }

        private void TbEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckUpdateButton();
        }

        //This button will change the the properties of the selected member
        private void BtnChangeMember_Click(object sender, RoutedEventArgs e)
        {
            //We find the ID of the selected item in the list of members by casting it as a member
            int id = (LbMembers.SelectedItem as MemberViewModel).MemberID;
            string name = TbMemberName.Text;
            string phoneNumber = TbPhoneNumber.Text;
            string email = TbEmail.Text;
            
            //Here we update the chosen Member with the new values in the textboxes
            //We call the updateMember method with the ID and the textbox values.
            changeMemberViewModel.UpdateMember(id, name, phoneNumber, email);
            MessageBox.Show("Medlem blev ændret");
            DialogResult = true;
        }
    }
}
