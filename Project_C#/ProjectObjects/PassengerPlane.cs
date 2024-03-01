using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_PO.ProjectObjects
{
    internal class PassengerPlane : Myobject
    {
        public ulong ID { get; set; }
        public string Serial { get; set; }
        public string Country { get; set; }
        public string Model { get; set; }
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
    }
}
