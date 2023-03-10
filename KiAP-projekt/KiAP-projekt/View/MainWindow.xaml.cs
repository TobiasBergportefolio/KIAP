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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KiAP_projekt.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //We create an instance of MainViewModel in a private Field
        //as this object will not be accessed outside of this class.
        private MainViewModel mainViewModel;
        public MainWindow()
        {
            //Instantiating a new object of MainViewModel
            mainViewModel = new MainViewModel();
            InitializeComponent();

            // The if-else statements enables which buttons the user interface shows based on the users role 
            if (mainViewModel.GetRole() == "Klyngemedlem")
            {
                BtnClusterMeeting.IsEnabled = false;
                BtnClusterMembers.IsEnabled = false;
                BtnFollowUp.IsEnabled = true;
                BtnMyPage.IsEnabled = true;
                BtnAdmin.IsEnabled = false;
            }
            else if (mainViewModel.GetRole() == "Klyngekoordinator")
            {
                BtnClusterMeeting.IsEnabled = true;
                BtnClusterMembers.IsEnabled = true;
                BtnFollowUp.IsEnabled = true;
                BtnMyPage.IsEnabled = true;
                BtnAdmin.IsEnabled = false;
            }
            else if (mainViewModel.GetRole() == "Admin")
            {
                BtnClusterMeeting.IsEnabled = true;
                BtnClusterMembers.IsEnabled = true;
                BtnFollowUp.IsEnabled = true;
                BtnMyPage.IsEnabled = true;
                BtnAdmin.IsEnabled = true;
            }
        }

        //When clicking on the button Klyngemøder the content of the Frame is set to the MeetingPage. 
        private void BtnClusterMeeting_Click(object sender, RoutedEventArgs e)
        {
            //Instantiating a new object of the MeetingPage navigationframe
            MeetingPage meetingPage = new MeetingPage();
            //Sets the content of the frame to the MeetingPage
            ContentNavigationFrame.Navigate(meetingPage);
        }

        //Creating a click event for the KiAP logo
        private void ImgKiapLogo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Return to the MainWindow when clicking on the logo by setting the Frame's content to null.
            ContentNavigationFrame.Content = null;
        }

        //When clicking on the button Klyngemøder the content of the Frame is set to the MembersgPage. 
        private void BtnClusterMembers_Click(object sender, RoutedEventArgs e)
        {
            //Instantiating a new object of the MeetingPage navigationframe
            MembersPage membersPage = new MembersPage();
            //Sets the content of the frame to the MembersPage
            ContentNavigationFrame.Navigate(membersPage);
        }
    }
}
