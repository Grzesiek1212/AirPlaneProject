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
        public Myobject CreateObject(byte[] messageBytes)
        {
            int FML = BitConverter.ToInt32(messageBytes, 3);
            ulong ID = BitConverter.ToUInt64(messageBytes, 7);
            ushort NL = BitConverter.ToUInt16(messageBytes, 15);
            string Name = Encoding.ASCII.GetString(messageBytes, 17, NL);
            string Code = Encoding.ASCII.GetString(messageBytes, 17+NL, 3);
            float Longitude = BitConverter.ToSingle(messageBytes, 20 + NL);
            float latitude = BitConverter.ToSingle(messageBytes, 24 + NL);
            float AMSL = BitConverter.ToSingle(messageBytes, 28 + NL);
            string Country = Encoding.ASCII.GetString(messageBytes, 32+NL,3);

            return new Airport(ID, Name, Code, Longitude, latitude, AMSL, Country);
        }
    }
}
