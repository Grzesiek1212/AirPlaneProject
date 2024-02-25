using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt1
{
    internal class Flight : Myobject
    {
        public ulong ID { get; set; }
        public ulong origin { get; set; }
        public ulong target { get; set; }
        public string TakeoffTime { get; set; }
        public string LandingTime { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public float AMSL { get; set; }
        public ulong plane_id { get; set; }
        public ulong[] crew_ids { get; set; }
        public ulong[] load_ids { get; set; }

        public Flight(ulong ID, ulong origin, ulong target, string TakeoffTime, string LandingTime, float Longitude, float Latitude, float AMSL, ulong plane_id, ulong[] crew_ids, ulong[] load_ids)
        {
            this.ID = ID;
            this.origin = origin;
            this.target = target;
            this.TakeoffTime = TakeoffTime;
            this.LandingTime = LandingTime;
            this.Longitude = Longitude;
            this.Latitude = Latitude;
            this.AMSL = AMSL;
            this.plane_id = plane_id;
            this.crew_ids = crew_ids;
            this.load_ids = load_ids;
        }

    }
    public class FlightFactory : IObjectFactory
    {
        public Myobject CreateObject(params string[] data)
        {
            ulong id = ulong.Parse(data[1]);
            ulong origin = ulong.Parse(data[2]);
            ulong target = ulong.Parse(data[3]);
            string TakeoffTime = data[4];
            string LandingTime = data[5];
            float Longitude = float.Parse(data[6], System.Globalization.CultureInfo.InvariantCulture);
            float Latitude = float.Parse(data[7], System.Globalization.CultureInfo.InvariantCulture);
            float AMSL = float.Parse(data[8], System.Globalization.CultureInfo.InvariantCulture);
            ulong plane_id = ulong.Parse(data[9]);

            string[] values_crew_ids = data[10].Trim('[', ']').Split(';');
            string[] values_load_ids = data[11].Trim('[', ']').Split(';');
            ulong[] crew_ids = new ulong[values_crew_ids.Length];
            ulong[] load_ids = new ulong[values_load_ids.Length];

            for (int i = 0; i < values_crew_ids.Length; i++)
            {
                crew_ids[i] = ulong.Parse(values_crew_ids[i]);
            }
            for (int i = 0; i < values_load_ids.Length; i++)
            {
                load_ids[i] = ulong.Parse(values_load_ids[i]);
            }

            return new Flight(id, origin, target, TakeoffTime, LandingTime, Longitude, Latitude, AMSL, plane_id, crew_ids, load_ids);
        }
    }
}

//FL ,   1094,     9,    8,  18:15  ,03:22,  92.35   ,-17.35    ,9337   ,12   ,[54;107;225;287;325] ,[37;26;26;26]