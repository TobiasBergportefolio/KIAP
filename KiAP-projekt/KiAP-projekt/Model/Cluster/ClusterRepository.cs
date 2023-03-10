using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiAP_projekt.Model
{
    //This class would be used for a functionality which is not yet fully implemented.
    public class ClusterRepository
    {
        //We make a list of the class Cluster with the name clusters.
        List<Cluster> clusters = new List<Cluster>();

        //This method is a placeholder for a functionality 
        //which would be used for further development
        public Cluster GetByName(string name)
        {
            Cluster result = new Cluster(name);
            return result;
        }
    }
}
