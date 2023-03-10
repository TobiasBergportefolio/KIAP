using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiAP_projekt.Model;

namespace KiAP_projekt.ViewModel
{
    public class ClusterMeetingViewModel
    {   //This class is used to present the information of our ClusterMeeting class to the View layer
        //to ensure the View layer has no knowledge of the Model layer.
        private ClusterMeeting clusterMeeting;

        public int ClusterMeetingID
        {
            get { return clusterMeeting.ClusterMeetingID; }
        }

        public string ClusterPackageName
        {
            get { return clusterMeeting.ClusterPackageName; }
        }


        public DateTime Date
        {
            get { return clusterMeeting.Date; }
        }

        public string Time
        {
            get { return clusterMeeting.Time.ToString("HH':'mm"); }
        }

        public double Duration
        {
            get { return clusterMeeting.Duration; }
        }

        public bool Online
        {
            get { return clusterMeeting.Online; }
        }

        public string Address
        {
            get { return clusterMeeting.Address; }
        }

        public string PostalCode
        {
            get { return clusterMeeting.PostalCode; }
        }

        public string City
        {
            get { return clusterMeeting.City; }
        }

        public bool Finished
        {
            get { return clusterMeeting.Finished; }
        }

        public DateTime RegistrationDeadline
        {
            get { return clusterMeeting.RegistrationDeadline; }
        }
        //Constructer that sets the field clustermeeting to the parameter clusterMeeting.
        public ClusterMeetingViewModel(ClusterMeeting clusterMeeting)
        {
            this.clusterMeeting = clusterMeeting;
        }

        //sets ToString method to display the ClusterPackageName.
        public override string ToString()
        {
            return ClusterPackageName;
        }
    }
}
