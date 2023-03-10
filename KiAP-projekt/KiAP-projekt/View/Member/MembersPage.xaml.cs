using KiAP_projekt.ViewModel;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;

namespace KiAP_projekt.View
{
    /// <summary>
    /// Interaction logic for MembersPage.xaml
    /// </summary>
    public partial class MembersPage : Page
    {
        //We create an instance of MainViewModel in a private field
        //as this object does not need to be accessed outside of this class.
        private MainViewModel mainViewModel;

        //In the constructor we instantiate a new object of MainViewModel
        //and setting the datacontext to the MainViewModel object.
        public MembersPage()
        {
            InitializeComponent();
            mainViewModel = new MainViewModel();
            DataContext = mainViewModel;
        }

        private void BtnAddMember_Click(object sender, RoutedEventArgs e)
        {
            //Instantiating a new object of createMemberDialog window to make it pop up
            //when we click on the BtnAddMember button. We update the member list if the dialog result is true
            CreateMemberDialog createMemberDialog = new CreateMemberDialog();
            if (createMemberDialog.ShowDialog()== true)
            {
                mainViewModel.UpdateMemberList();
            }

        }

        private void BtnUpdateMember_Click(object sender, RoutedEventArgs e)
        {
            //Instantiating a new object of createMemberDialog window to make it pop up
            //when we click on the BtnAddMember button. We update the member list if the dialog result is true
            ChangeMemberDialog changeMemberDialog = new ChangeMemberDialog();
            if (changeMemberDialog.ShowDialog() == true)
            {
                mainViewModel.UpdateMemberList();
            }
        }
        
        //This method deletes the selected member
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            //We create a messagebox to request a confirmation from the user.
            //If the result is yes the user is removed
            MessageBoxResult result = MessageBox.Show("Er du sikker på at du vil slette dette medlem?", "Slet medlem", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MessageBox.Show("Medlem er nu slettet", "Bekræftelse");
                    break;
                case MessageBoxResult.No:
                    return;
            }

            //deleting member from the id of selected member and updating whole listbox with new memberlist.
            int id = (LbMembers.SelectedItem as MemberViewModel).MemberID;
            mainViewModel.DeleteMember(id);
            mainViewModel.UpdateMemberList();
        }

        //This button returns to the MainWindow when it's clicked by setting the Page Content to null.
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Content = null;
        }
    }
}
