using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiAP_projekt.Model
{
    public class ClusterMeeting
    {
        public Cluster Cluster { get; }

        public int ClusterMeetingID { get; set; }

        public string ClusterPackageName { get; set; }

        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

        public double Duration { get; set; }

        public bool Online { get; set; }

        public string Address { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public bool Finished { get; set; }

        public DateTime RegistrationDeadline { get; set; }

        //This constructor assigns values to the properties when an object is instantiated.
        public ClusterMeeting(string clusterPackageName, DateTime date, DateTime time, double duration, DateTime registrationDeadline, bool online, string address = null, string postalCode = null, string city = null)
        {
            ClusterPackageName = clusterPackageName;
            Date = date;
            Time = time;
            Duration = duration;
            RegistrationDeadline = registrationDeadline;
            Online = online;
            Address = address;
            PostalCode = postalCode;
            City = city;
        }

        //This is a functionality to create a reference between ClusterMeeting and Cluster. 
        //This is used for a functionality which is not yet fully implemented.
        public ClusterMeeting(string clusterPackageName, DateTime date, DateTime time, double duration, DateTime registrationDeadline, bool online, Cluster cluster, string address = null, string postalCode = null, string city = null)
        {
            ClusterPackageName = clusterPackageName;
            Date = date;
            Time = time;
            Duration = duration;
            RegistrationDeadline = registrationDeadline;
            Online = online;
            Cluster = cluster;
            Address = address;
            PostalCode = postalCode;
            City = city;
        }

        //We made constructor chaining to have a constructor which also assigns a value to ClusterMeetingID.
        //This is used when pulling data from the database where they would be assigned an ID.
        public ClusterMeeting(int clusterMeetingID, string clusterPackageName, DateTime date, DateTime time, double duration, DateTime registrationDeadline, bool online, string address = null, string postalCode = null, string city = null)
            : this(clusterPackageName, date, time, duration, registrationDeadline, online, address, postalCode, city)
        {
            ClusterMeetingID = clusterMeetingID;
        }

    }
}
