using System;
using System.Collections.Generic;
using System.Globalization;
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
    public partial class ChangeMeetingDialog : Window
    {
        //We create an instance of ChangeMeetingViewModel in a private field
        private ChangeMeetingViewModel changeMeetingViewModel;

        //In the constructor we instantiate the changeMeetingViewModel
        //and assign the DataContext to it
        public ChangeMeetingDialog()
        {
            InitializeComponent();
            changeMeetingViewModel = new ChangeMeetingViewModel();
            DataContext = changeMeetingViewModel;
        }

        //Click event that updates meeting 
        private void BtnChangeMeeting_Click(object sender, RoutedEventArgs e)
        {
            //We create a bool which will become true if any exceptions are thrown
            bool error = false;

            //We get the ID of the selected ClusterMeeting to use in the UpdateClusterMeetingMethod
            int id = (LbClusterMeetings.SelectedItem as ClusterMeetingViewModel).ClusterMeetingID;

            //Here we have exception handling of the inputs from the user
            //We create a DateTime variable called date to store the Selected date of our Date picker
            DateTime date = default;
            try
            {
                date = DpDate.SelectedDate.Value;
            }
            //This exception is thrown when a date is not picked from the DatePicker
            //as the program then attemps to assign a null value to the date value we just created
            catch (InvalidOperationException)
            {
                MessageBox.Show("Vælg venligst en dato");
                error = true;
            }

            //this DateTime value is meant to hold the time of the meeting
            DateTime time = default;
            try
            {
                //We give time the value of 01/01/0001 as well as the time given.
                //We only use the time part of this variable
                //We use a formatprovider in the parse statement to ensure that the Danish time formats are excepted
                time = DateTime.Parse("01/01/0001 " + TbTime.Text, new CultureInfo("da"));
            }
            //this exception is thrown when the time is not in the correct format
            catch (FormatException)
            {
                MessageBox.Show("Brug venligst det korrekte format");
                error = true;
            }

            //We create a DateTime variable called date to store the Selected date of our Date picker
            DateTime registrationDeadline = default;
            try
            {
                registrationDeadline = DpRegistrationDeadline.SelectedDate.Value;
            }
            //This exception is thrown when a date is not picked from the DatePicker
            //as the program then attemps to assign a null value to the date value we just created
            catch (InvalidOperationException)
            {
                MessageBox.Show("Vælg venligst en tilmeldingsfrist");
                error = true;
            }

            //We check that the registrationDeadline is before the MeetingDate
            if (DpRegistrationDeadline.SelectedDate.Value > DpDate.SelectedDate.Value)
            {
                MessageBox.Show("Tilmeldingsfristen skal falde før mødestart");
                error = true;
            }

            //These values will hold the values of our Duration combobox,
            //ClusterPackageName combobox and our online checkbox
            double duration = double.Parse(CbDuration.SelectedItem.ToString());
            string clusterPackageName = CbClusterPackageName.SelectedItem.ToString();
            bool online = ChbOnline.IsChecked.Value;

            //Here we check if our error variable has been set to true.
            //If it is true the following code will not run
            if (!error)
            {
                //We check if an address has been given.
                //If an address has been given we assign the value to a temporary variable
                if (TbAddress.Text != "")
                {
                    string address = TbAddress.Text;

                    //We now check whether online is true or not.
                    //If online is true there are no value for city or postal code so they will not be assigned
                    if (online)
                    {
                        //If online is true then we call the UpdateClusterMeeting method with address
                        //but without postal code and city
                        changeMeetingViewModel.UpdateClusterMeeting(id, date, time, duration, clusterPackageName, registrationDeadline, online, address);
                    }
                    else
                    {
                        //if online is false that means that PostalCode and City has values
                        //and we call the UpdateClusterMeeting method with these values
                        string city = TbCity.Text;
                        string postalCode = TbPostalCode.Text;

                        //Creating the updated meeting with address, postal code and city
                        changeMeetingViewModel.UpdateClusterMeeting(id, date, time, duration, clusterPackageName, registrationDeadline, online, address, city, postalCode);
                    }
                }
                else
                {
                    //if there is no address given we update the meeting without address, postal code and city
                    changeMeetingViewModel.UpdateClusterMeeting(id, date, time, duration, clusterPackageName, registrationDeadline, online);
                }
                //Confirm creation and return to MainWindow
                MessageBox.Show("Klyngemødet blev ændret");
                DialogResult = true;
            }
            
        }

        //This method checks the conditions to determine if the ChangeMeeting button should be enabled
        private void CheckChangeButton()
        {
            if (BtnChangeMeeting != null)
            {
                BtnChangeMeeting.IsEnabled = !(DpDate.SelectedDate == null || TbTime.Text == "" || CbDuration.SelectedItem == null || DpRegistrationDeadline.SelectedDate == null || CbClusterPackageName.SelectedItem == null);
            }
        }

        //The following 5 methods checks if the ChangeMeetingButton should be enabled
        //in the case of changes to the required textboxes, datepickers and comboboxes
        private void DpDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckChangeButton();
        }

        private void TbTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckChangeButton();
        }

        private void CbDuration_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckChangeButton();
        }

        private void DpRegistrationDeadline_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckChangeButton();
        }

        private void CbClusterPackageName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckChangeButton();
        }

        //This method disables the city and posatlcode textboxes
        //and changes the address label to say "Mødelink"
        //when the online checkbox is checked
        private void ChbOnline_Checked(object sender, RoutedEventArgs e)
        {
            TbCity.IsEnabled = false;
            TbPostalCode.IsEnabled = false;
            LblAddress.Content = "Mødelink:";
        }

        //This method enables the city and posatlcode textboxes
        //and changes the address label to say "Adresse"
        //when the online checkbox is unchecked
        private void ChbOnline_Unchecked(object sender, RoutedEventArgs e)
        {
            TbCity.IsEnabled = true;
            TbPostalCode.IsEnabled = true;
            LblAddress.Content = "Adresse:";
        }
    }
}
