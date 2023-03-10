using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiAP_projekt.Model;

namespace KiAP_projekt.ViewModel
{
    public class CreateMeetingViewModel
    {
        //below we see ClusterMeetingRepository and ClusterRepository gets instanciated.
        private ClusterMeetingRepository clusterMeetingRepository = new ClusterMeetingRepository();
        private ClusterRepository clusterRepository = new ClusterRepository();

        //an array of ClusterPackages used for databinding.
        public string[] package { get; set; } = { "Type 2-diabetes - behandling og kvalitet", "Type 2-diabetes - organisering og opfølgning", "KOL - diagonstik", "KOL - behandling", "Trivsel og arbejdsglæde", "Smertestillende medicin" };

        //an array of duration values to be used for databinding.
        public double[] duration { get; set; } = {1, 1.5, 2, 2.5, 3, 3.5, 4, 4.5, 5, 5.5, 6 };

        public void CreateClusterMeeting(DateTime date, DateTime time, double duration, string clusterPackageName, DateTime registrationDeadline, bool online, string address = null, string postalCode = null, string city = null)
        {
            //Instantiate ClusterMeeting and call Add method from repository
            ClusterMeeting clusterMeeting = new ClusterMeeting(clusterPackageName, date, time, duration, registrationDeadline, online, address, postalCode, city);
            clusterMeetingRepository.Add(clusterMeeting);
        }

        //This method is used for a functionality using Cluster which is not yet fully implemented
        public void CreateClusterMeeting(DateTime date, DateTime time, double duration, string clusterPackageName, DateTime registrationDeadline, bool online, ClusterViewModel clusterVM, string address = null, string postalCode = null, string city = null)
        {
            Cluster c = clusterRepository.GetByName(clusterVM.ClusterName);
            //Instantiate ClusterMeeting and call Add method from repository
            ClusterMeeting clusterMeeting = new ClusterMeeting(clusterPackageName, date, time, duration, registrationDeadline, online, c, address, postalCode, city);
            //ClusterMeetingViewModel clusterMeetingViewModel = new ClusterMeetingViewModel(clusterMeeting);
            clusterMeetingRepository.Add(clusterMeeting);
        }

        //This method sends an emial to the participating members of the meeting that a meeting has been created
        public void SendMeetingMail(string clusterPackageName, DateTime date, DateTime time, double duration, DateTime registrationDeadline)
        {
            clusterMeetingRepository.SendMail(clusterPackageName, date, time, duration, registrationDeadline);
        }
    }
}
