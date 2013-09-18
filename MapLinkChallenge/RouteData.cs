using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MapLinkChallenge
{
    [DataContract]
    class RouteData
    {
        [DataMember]
        public int id;

        [DataMember]
        public float fuelCost;

        [DataMember]
        public double distance;

        [DataMember]
        public string totalTime;
    }
}
