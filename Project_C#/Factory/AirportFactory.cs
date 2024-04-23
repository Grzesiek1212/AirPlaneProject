using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_C_;
using Projekt_PO.ProjectObjects;

namespace Projekt_PO.Factory
{
    public class AirportFactory : IObjectFactory
    {
        public Myobject CreateObject(params string[] data)
        {
            ulong ID = ulong.Parse(data[1]);
            string Name = data[2];
            string Code = data[3];
            float Longitude = float.Parse(data[4], System.Globalization.CultureInfo.InvariantCulture);
            float latitude = float.Parse(data[5], System.Globalization.CultureInfo.InvariantCulture);
            float AMSL = float.Parse(data[6], System.Globalization.CultureInfo.InvariantCulture);
            string Country = data[7];

            AirportFlightLists airportFlightLists = AirportFlightLists.Instance;
            Airport airport = new Airport(ID, Name, Code, Longitude, latitude, AMSL, Country);
            airportFlightLists.AddAirport(airport);
            airportFlightLists.AddIreportableObject(airport);
            return airport;
        }

    }
}
