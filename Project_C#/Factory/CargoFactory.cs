using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt_PO.ProjectObjects;

namespace Projekt_PO.Factory
{
    public class CargoFactory : IObjectFactory
    {
        public Myobject CreateObject(params string[] data)
        {
            ulong ID = ulong.Parse(data[1]);
            float Weight = float.Parse(data[2], System.Globalization.CultureInfo.InvariantCulture);
            string Code = data[3];
            string Description = data[4];

            return new Cargo(ID, Weight, Code, Description);
        }

    }
}
