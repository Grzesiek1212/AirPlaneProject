using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_C_;
using Projekt_PO.ProjectObjects;

namespace Projekt_PO.Factory
{
    public class PassengerFactory : IObjectFactory
    {
        public Myobject CreateObject(params string[] data)
        {
            ulong ID = ulong.Parse(data[1]);
            string Name = data[2];
            ulong Age = ulong.Parse(data[3]);
            string Phone = data[4];
            string Email = data[5];
            string Class = data[6];
            ulong Miles = ulong.Parse(data[7]);

            AirportFlightLists airportFlightLists = AirportFlightLists.Instance;
            Passenger passenger = new Passenger(ID, Name, Age, Phone, Email, Class, Miles);
            airportFlightLists.AddHuman(passenger);
            return passenger;
        }
    }

}
