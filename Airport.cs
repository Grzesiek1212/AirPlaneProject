using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt1
{
    internal class Airport : Myobject
    {
        public ulong ID { get; set; }
        public string name { get; set; }
        public string Code { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public float AMSL { get; set; }
        public string country { get; set; }

        public Airport(ulong ID, string name, string Code, float Longitude, float Latitude, float AMSL, string country)
        {
            this.ID = ID;
            this.name = name;
            this.Code = Code;
            this.Latitude = Latitude;
            this.Longitude = Longitude;
            this.AMSL = AMSL;
            this.country = country;
        }

    }
    public class AirportFactory : IObjectFactory
    {
        public Myobject CreateObject(params string[] data)
        {
            ulong id = ulong.Parse(data[1]);
            string name = data[2];
            string code = data[3];
            float Longitude = float.Parse(data[4], System.Globalization.CultureInfo.InvariantCulture);
            float latitude = float.Parse(data[5], System.Globalization.CultureInfo.InvariantCulture);
            float AMSL = float.Parse(data[6], System.Globalization.CultureInfo.InvariantCulture);
            string country = data[7];

            return new Airport(id, name, code, Longitude, latitude, AMSL, country);
        }
    }
}
