using ExCSS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_C_.Community;
using Projekt_PO.ProjectObjects;

namespace Project_C_
{
    // Make a vistor interface to implement message of some object
    public interface IReportable
    {
        public string Accept(IMediaVisitor visitor);
    }


    // interafce that define the visit methods
    public interface IMediaVisitor
    {
        string VisitAirport(Airport airport);
        string VisitPassengerPlane(PassengerPlane plane);
        string VisitCargoPlane(CargoPlane plane);
    }

}
