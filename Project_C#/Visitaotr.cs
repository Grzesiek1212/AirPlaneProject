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

    public interface IReportable
    {
        public string Accept(IMediaVisitor visitor);
    }

    public interface IMediaVisitor
    {    
    string VisitAirport(Airport airport);
    string VisitPassengerPlane(PassengerPlane plane);
    string VisitCargoPlane(CargoPlane plane);
    }

}
