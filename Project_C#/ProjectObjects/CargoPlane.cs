using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_C_;

namespace Projekt_PO.ProjectObjects
{
    public class CargoPlane : Plane, IReportable
    {
        public float MaxLoad { get; set; }

        public CargoPlane(ulong ID, string Serial, string Country, string Model, float MaxLoad)
        {
            this.ID = ID;
            this.Serial = Serial;
            this.Country = Country;
            this.Model = Model;
            this.MaxLoad = MaxLoad;
        }
        // Accept function to create a suitable message
        public string Accept(IMediaVisitor visitor)
        {
            return visitor.VisitCargoPlane(this);
        }
    }
}
