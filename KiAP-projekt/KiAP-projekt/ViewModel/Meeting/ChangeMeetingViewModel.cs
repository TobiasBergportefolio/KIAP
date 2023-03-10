using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiAP_projekt.Model;

namespace KiAP_projekt.ViewModel
{
    public class ChangeMeetingViewModel : INotifyPropertyChanged
    {
        //This class is used to handle UI input from the UI layer
        private ClusterMeetingRepository clusterMeetingRepo;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ClusterMeetingViewModel> ClusterMeetingViewModels { get; set; }
        
        private ClusterMeetingViewModel selectedClusterMeeting;

        //Fully implemented property to be able to use the inotifypropertychanged intreface
        public ClusterMeetingViewModel SelectedClusterMeeting
        {
            get { return selectedClusterMeeting; }
            set {
                selectedClusterMeeting = value;
                OnPropertyChanged("SelectedClusterMeeting");
            }
        }

        //We have an array of ClusterPackages to use for databinding.
        public string[] package { get; set; } = { "Type 2-diabetes - behandling og kvalitet", "Type 2-diabetes - organisering og opfølgning", "KOL - diagonstik", "KOL - behandling", "Trivsel og arbejdsglæde", "Smertestillende medicin" };

        //We have an array of duration  to use for databinding.
        public double[] duration { get; set; } = { 1, 1.5, 2, 2.5, 3, 3.5, 4, 4.5, 5, 5.5, 6 };

        //In the constructor we instantiate the clusterMEetingRepo and the list of ClusterMeetingViewModels
        public ChangeMeetingViewModel()
        {
            clusterMeetingRepo = new ClusterMeetingRepository();
            ClusterMeetingViewModels = new ObservableCollection<ClusterMeetingViewModel>();

            //In this loop we get all the meetings by calling the GetAll method from the repository
            //In each iteration of the loop we then instantiate a new ClusterMeetingViewModel
            //using the ClusterMeeting of the current iteration as an argument for the ClusterMeetingViewModel constructor
            //Lastly we add the ClusterMeetingViewModel instance to the ClusterMeetingViewModels list
            foreach (ClusterMeeting cm in clusterMeetingRepo.GetAll())
            {
                ClusterMeetingViewModels.Add(new ClusterMeetingViewModel(cm));
            }

        }

        //Address, city and postalCode are optional parameters
        public void UpdateClusterMeeting(int id, DateTime date, DateTime time, double duration, string clusterPackageName, DateTime registrationDeadline, bool online, string address = null, string postalCode = null, string city = null)
        {
            //Here we simply call the update method from the repository
            clusterMeetingRepo.Update(id, date, time, duration, clusterPackageName, registrationDeadline, online, address, postalCode, city);
        }

        //this method raises the OnpPropertyChanged event and the propertyName is used as the parameter.
        //It is connected with the fully implemented property above this.
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
