using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt_PO.ProjectObjects;

namespace Projekt_PO.Factory
{
    public class AirportFactory : IObjectFactory
    {
        public Myobject CreateObject(params string[] data)
        {
            ulong Id = ulong.Parse(data[1]);
            string Name = data[2];
            string code = data[3];
            float Longitude = float.Parse(data[4], System.Globalization.CultureInfo.InvariantCulture);
            float latitude = float.Parse(data[5], System.Globalization.CultureInfo.InvariantCulture);
            float AMSL = float.Parse(data[6], System.Globalization.CultureInfo.InvariantCulture);
            string Country = data[7];

            return new Airport(Id, Name, code, Longitude, latitude, AMSL, Country);
        }

    }
}
