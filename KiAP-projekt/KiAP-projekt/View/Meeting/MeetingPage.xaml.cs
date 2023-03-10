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
    /// Interaction logic for MeetingPage.xaml
    /// </summary>
    public partial class MeetingPage : Page
    {
        //this is private so that it can not be seen outside of the class
        private MainViewModel mainViewModel;
        //initializecomponent() defines everything we see on the form
        //Everything done on the form in VS.NET using designers generates code.
        //Every single control added and property set will generate code and that code goes into InitializeComponent() method.
        //then we initialize mainViewModel, and the datacontext sets or gets mainViewModel because it participates in databinding
        public MeetingPage()
        {
            InitializeComponent();

            mainViewModel = new MainViewModel();
            DataContext = mainViewModel;
        }

        //auto generated button click for CreateMeeting.
        //We make and show a CreateMeetingDialog.
        //The if statement will update the list of upcoming meetings if the dialog result is true
        private void BtnCreateMeeting_Click(object sender, RoutedEventArgs e)
        {
            //Opening the CreateMeetingDialog window
            CreateMeetingDialog createMeetingDialog = new CreateMeetingDialog();
            if (createMeetingDialog.ShowDialog() == true)
            {
                mainViewModel.UpdateUpcomingMeetingList();
            }

        }
        //auto generated button click for ChangeMeeting.
        //the if statement makes a dialog show and if the dialog result is true it updates the list of upcoming meetings.
        private void BtnChangeMeeting_Click(object sender, RoutedEventArgs e)
        {
            ChangeMeetingDialog changeMeetingDialog = new ChangeMeetingDialog();
            if(changeMeetingDialog.ShowDialog() == true)
            {
                mainViewModel.UpdateUpcomingMeetingList();
            }
        }
        //auto generated button click for Delete Meeting. the switch statement shows a veryfication of deleting the meeting.
        //the cases just show the yes and no option that you can click when verification is showing.
        //then it deletes the meeting if "yes" and updates the meeting list.
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Er du sikker på du vil slette dette klyngemøde", "Slet klyngemøde", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MessageBox.Show("Klyngemødet er nu slettet", "Bekræftelse");
                    break;
                case MessageBoxResult.No:
                    return;
            }

            int id = (LbClusterMeetings.SelectedItem as ClusterMeetingViewModel).ClusterMeetingID;
            mainViewModel.DeleteClusterMeeting(id);
            mainViewModel.UpdateUpcomingMeetingList();

        }

        //the back button is taking you back to the main window page
        //by setting the content of the page to null.
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Content = null;
        }

        //this is the method that was supposed to show the clusterMeetings info.
        //It was not fully implemented and for now simply shows the number the selected meeting holds in the list.
        //we keep the code as it is just an idea that is not fully implemented but good to talk about.
        private void LbClusterMeetings_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (LbClusterMeetings.SelectedItem != null)
            {
                MessageBox.Show((LbClusterMeetings.SelectedIndex + 1).ToString());
            }
        }
    }
}