using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt1
{
    internal class CargoPlane : Myobject
    {
        public ulong ID { get; set; }
        public string serial { get; set; }
        public string country { get; set; }
        public string model { get; set; }
        public float MaxLoad { get; set; }

        public CargoPlane(ulong ID, string serial, string country, string model, float MaxLoad)
        {
            this.ID = ID;
            this.serial = serial;
            this.country = country;
            this.model = model;
            this.MaxLoad = MaxLoad;
        }

    }
    public class CargoPlaneFactory : IObjectFactory
    {
        public Myobject CreateObject(params string[] data)
        {
            ulong id = ulong.Parse(data[1]);
            string serial = data[2];
            string country = data[3];
            string model = data[4];
            float MaxLoad = float.Parse(data[5]);

            return new CargoPlane(id, serial, country, model, MaxLoad);
        }
    }
}
