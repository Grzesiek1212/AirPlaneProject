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
    public class Newspaper : MyMedia, IMediaVisitor
    {
        public Newspaper(string Name) : base(Name)
        {
        }

        public override string VisitAirport(Airport airport)
        {
            return $"{Name} - A report from the {airport.Name} airport, {airport.Country}";
        }

        public override string VisitPassengerPlane(PassengerPlane plane)
        {
            return $"{Name} - An interview with the crew of {plane.Serial}.";
        }

        public override string VisitCargoPlane(CargoPlane plane)
        {
            return $"Breaking news! {plane.Model} aircraft loses EASA fails certification after inspection of {plane.Serial}.";
        }
    }
}
