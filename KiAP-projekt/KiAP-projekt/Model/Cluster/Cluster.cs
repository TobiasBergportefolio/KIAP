using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiAP_projekt.Model
{
    //This class would be used for a functionality which is not yet fully implemented.
    public class Cluster
    {
        public string ClusterName { get; set; }

        public int MemberCount { get; set; }

        //This constructor gives the Cluster a name.
        public Cluster(string clusterName)
        {
            ClusterName = clusterName;
        }

    }
}
