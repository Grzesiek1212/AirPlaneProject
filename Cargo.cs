using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt1
{
    internal class Cargo : Myobject
    {
        public ulong ID { get; set; }
        public float weight { get; set; }
        public string Code { get; set; }
        public string description { get; set; }

        public Cargo(ulong ID, float weight, string Code, string description)
        {
            this.ID = ID;
            this.weight = weight;
            this.Code = Code;
            this.description = description;
        }

    }

    public class CargoFactory : IObjectFactory
    {
        public Myobject CreateObject(params string[] data)
        {
            ulong id = ulong.Parse(data[1]);
            float weight = float.Parse(data[2], System.Globalization.CultureInfo.InvariantCulture);
            string code = data[3];
            string description = data[4];

            return new Cargo(id, weight, code, description);
        }
    }
}
