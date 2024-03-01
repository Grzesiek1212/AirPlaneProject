using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_PO.ProjectObjects
{
    internal class Flight : Myobject
    {
        public ulong ID { get; set; }
        public ulong Origin { get; set; }
        public ulong Target { get; set; }
        public string TakeoffTime { get; set; }
        public string LandingTime { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public float AMSL { get; set; }
        public ulong Plane_id { get; set; }
        public List<ulong> Crew_ids { get; set; }
        public List<ulong> Load_ids { get; set; }

        public Flight(ulong ID, ulong Origin, ulong Target, string TakeoffTime, string LandingTime, float Longitude, float Latitude, float AMSL, ulong Plane_id, List<ulong> Crew_ids, List<ulong> Load_ids)
        {
            this.ID = ID;
            this.Origin = Origin;
            this.Target = Target;
            this.TakeoffTime = TakeoffTime;
            this.LandingTime = LandingTime;
            this.Longitude = Longitude;
            this.Latitude = Latitude;
            this.AMSL = AMSL;
            this.Plane_id = Plane_id;
            this.Crew_ids = Crew_ids;
            this.Load_ids = Load_ids;
        }

    }
}