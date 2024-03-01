using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_PO.ProjectObjects
{
    internal class Cargo : Myobject
    {
        public ulong ID { get; set; }
        public float Weight { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public Cargo(ulong ID, float Weight, string Code, string Description)
        {
            this.ID = ID;
            this.Weight = Weight;
            this.Code = Code;
            this.Description = Description;
        }

    }
}
