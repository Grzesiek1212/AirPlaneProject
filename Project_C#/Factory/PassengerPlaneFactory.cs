using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_C_;
using Projekt_PO.ProjectObjects;

namespace Projekt_PO.Factory
{
    public class PassengerPlaneFactory : IObjectFactory
    {
        public Myobject CreateObject(params string[] data)
        {
            ulong id = ulong.Parse(data[1]);
            string Serial = data[2];
            string Country = data[3];
            string Model = data[4];
            ushort FirstClassSize = ushort.Parse(data[5]);
            ushort BusinessClassSize = ushort.Parse(data[6]);
            ushort EconomyClassSize = ushort.Parse(data[7]);

            AirportFlightLists airportFlightLists = AirportFlightLists.Instance;
            PassengerPlane plane = new PassengerPlane(id, Serial, Country, Model, FirstClassSize, BusinessClassSize, EconomyClassSize);
            airportFlightLists.AddIreportableObject(plane);
            airportFlightLists.AddPlane(plane);

            return plane;
        }
    }
}
