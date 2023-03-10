using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiAP_projekt.Model;

namespace KiAP_projekt.ViewModel
{
    //This class would be used for a functionality which is not yet fully implemented.
    //This class would be used to present the Cluster class to the view layer
    public class ClusterViewModel
    {
        private Cluster cluster;

        public string ClusterName
        {
            get { return cluster.ClusterName; }
        }

        public int MemberCount
        {
            get { return cluster.MemberCount; }
        }

        //The constructor takes a Cluster as an argument.
        //It sets the Cluster field to the Cluster given as an argument when instantiating an object of this class
        public ClusterViewModel(Cluster cluster)
        {
            this.cluster = cluster;
        }
    }
}
