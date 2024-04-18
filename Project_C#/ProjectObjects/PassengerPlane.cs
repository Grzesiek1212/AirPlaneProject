using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_C_;

namespace Projekt_PO.ProjectObjects
{
    public class PassengerPlane : Plane, IReportable
    {
        public ushort FirstClassSize { get; set; }
        public ushort BusinessClassSize { get; set; }
        public ushort EconomyClassSize { get; set; }

        public PassengerPlane(ulong ID, string Serial, string Country, string Model, ushort FirstClassSize, ushort BusinessClassSize, ushort EconomyClassSize)
        {
            this.ID = ID;
            this.Serial = Serial;
            this.Country = Country;
            this.Model = Model;
            this.FirstClassSize = FirstClassSize;
            this.BusinessClassSize = BusinessClassSize;
            this.EconomyClassSize = EconomyClassSize;
        }

        // Accept function to create a suitable message
        public string Accept(IMediaVisitor visitor)
        {
            return visitor.VisitPassengerPlane(this);
        }
    }
}
