using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_C_;
using Projekt_PO.ProjectObjects;

namespace Projekt_PO.Factory
{
    public class FlightFactory : IObjectFactory
    {
        public Myobject CreateObject(params string[] data)
        {
            AirportFlightLists airportFlightLists = AirportFlightLists.Instance; // There we use this singleton to add this object and find references
            ulong ID = ulong.Parse(data[1]);
            ulong OriginID = ulong.Parse(data[2]);
            ulong TargetID = ulong.Parse(data[3]);
            Airport Origin = airportFlightLists.GetAirport(OriginID);
            Airport Target = airportFlightLists.GetAirport(TargetID);
            string TakeoffTime = data[4];
            string LandingTime = data[5];
            float Longitude = float.Parse(data[6], System.Globalization.CultureInfo.InvariantCulture);
            float Latitude = float.Parse(data[7], System.Globalization.CultureInfo.InvariantCulture);
            float AMSL = float.Parse(data[8], System.Globalization.CultureInfo.InvariantCulture);
            ulong Plane_id = ulong.Parse(data[9]);
            Plane plane = airportFlightLists.GetPlane(Plane_id);

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

            Flight flight = new Flight(ID, Origin, Target, TakeoffTime, LandingTime, Longitude, Latitude, Origin.Longitude, Origin.Latitude, AMSL, plane, Crew_ids, Load_ids);

            airportFlightLists.AddFlight(flight);

            return flight;
        }
    }
}
