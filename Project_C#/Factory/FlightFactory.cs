using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt_PO.ProjectObjects;

namespace Projekt_PO.Factory
{
    public class FlightFactory : IObjectFactory
    {
        public Myobject CreateObject(params string[] data)
        {
            ulong id = ulong.Parse(data[1]);
            ulong Origin = ulong.Parse(data[2]);
            ulong Target = ulong.Parse(data[3]);
            string TakeoffTime = data[4];
            string LandingTime = data[5];
            float Longitude = float.Parse(data[6], System.Globalization.CultureInfo.InvariantCulture);
            float Latitude = float.Parse(data[7], System.Globalization.CultureInfo.InvariantCulture);
            float AMSL = float.Parse(data[8], System.Globalization.CultureInfo.InvariantCulture);
            ulong Plane_id = ulong.Parse(data[9]);

            string[] values_crew_ids = data[10].Trim('[', ']').Split(';');
            string[] values_load_ids = data[11].Trim('[', ']').Split(';');
            List<ulong> Crew_ids = new List<ulong>();
            List<ulong> Load_ids = new List<ulong>();

            for (int i = 0; i < values_crew_ids.Length; i++)
            {
                Crew_ids.Add(ulong.Parse(values_crew_ids[i]));
            }
            for (int i = 0; i < values_load_ids.Length; i++)
            {
                Load_ids.Add(ulong.Parse(values_load_ids[i]));
            }

            return new Flight(id, Origin, Target, TakeoffTime, LandingTime, Longitude, Latitude, AMSL, Plane_id, Crew_ids, Load_ids);
        }
    }
}
