using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_C_;

namespace Projekt_PO.ProjectObjects
{
    public class CargoPlane : Myobject, IReportable
    {
        public ulong ID { get; set; }
        public string Serial { get; set; }
        public string Country { get; set; }
        public string Model { get; set; }
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
