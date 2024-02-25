using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt1
{
    internal class PassengerPlane : Myobject
    {
        public ulong ID { get; set; }
        public string serial { get; set; }
        public string country { get; set; }
        public string model { get; set; }
        public ushort FirstClassSize { get; set; }
        public ushort BusinessClassSize { get; set; }
        public ushort EconomyClassSize { get; set; }

        public PassengerPlane(ulong ID, string serial, string country, string model, ushort FirstClassSize, ushort BusinessClassSize, ushort EconomyClassSize)
        {
            this.ID = ID;
            this.serial = serial;
            this.country = country;
            this.model = model;
            this.FirstClassSize = FirstClassSize;
            this.BusinessClassSize = BusinessClassSize;
            this.EconomyClassSize = EconomyClassSize;
        }
    }
    public class PassengerPlaneFactory : IObjectFactory
    {
        public Myobject CreateObject(params string[] data)
        {
            ulong id = ulong.Parse(data[1]);
            string serial = data[2];
            string country = data[3];
            string model = data[4];
            ushort FirstClassSize = ushort.Parse(data[5]);
            ushort BusinessClassSize = ushort.Parse(data[6]);
            ushort EconomyClassSize = ushort.Parse(data[7]);

            return new PassengerPlane(id, serial, country, model, FirstClassSize, BusinessClassSize, EconomyClassSize);

        }
    }
}
