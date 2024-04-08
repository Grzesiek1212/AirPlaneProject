using Projekt_PO;
using Projekt_PO.ProjectObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Project_C_.Community
{
    public class Radio : MyMedia, IMediaVisitor
    {
        public Radio(string Name) : base(Name)
        {
        }

        public override string VisitAirport(Airport airport)
        {
            return $"Reporting for {Name} Ladies and gentelmen, we are at the {airport.Name} airport.";
        }

        public override string VisitPassengerPlane(PassengerPlane plane)
        {
            return $"Reporting for {Name} Ladies and gentelmen, we are seeing the {plane.Serial} aircraft fly above us.";
        }

        public override string VisitCargoPlane(CargoPlane plane)
        {
            return $"Reporting for {Name} Ladies and gentelmen, we've just witnessed  {plane.Serial} take off.";
        }
    }
}
