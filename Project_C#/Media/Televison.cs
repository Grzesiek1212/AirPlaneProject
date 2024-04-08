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
    public class Televison : MyMedia, IMediaVisitor
    {
        public Televison(string Name) : base(Name)
        {
        }

        public override string VisitAirport(Airport airport)
        {
            return $"An image of {airport.Name} airport.";
        }

        public override string VisitPassengerPlane(PassengerPlane plane)
        {
            return $"An image of {plane.Serial} cargo plane.";
        }

        public override string VisitCargoPlane(CargoPlane plane)
        {
            return $"An image of {plane.Serial} passenger plane.";
        }
    }
}
