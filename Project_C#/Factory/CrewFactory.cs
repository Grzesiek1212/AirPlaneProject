using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Project_C_;
using Projekt_PO.ProjectObjects;

namespace Projekt_PO.Factory
{
    public class CrewFactory : IObjectFactory
    {
        public Myobject CreateObject(params string[] data)
        {
            ulong ID = ulong.Parse(data[1]);
            string Name = data[2];
            ulong Age = ulong.Parse(data[3]);
            string Phone = data[4];
            string Email = data[5];
            ushort Practice = ushort.Parse(data[6]);
            string Role = data[7];

            AirportFlightLists airportFlightLists = AirportFlightLists.Instance; // There we use this singleton to add this object
            Crew crew = new Crew(ID, Name, Age, Phone, Email, Practice, Role);
            airportFlightLists.AddHuman(crew);
            return crew;
        }

    }
}
