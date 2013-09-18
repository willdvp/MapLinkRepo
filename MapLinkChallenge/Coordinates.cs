using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MapLinkChallenge
{
    [DataContract]
    class Coordinates
    {
        [DataMember]
        public int id;

        [DataMember]
        public Coordinate originCoordinate;

        [DataMember]
        public Coordinate destinationCoordinate;
    }

    public class Coordinate
    {
        public double latitude;
        public double longitude;
    }
}
