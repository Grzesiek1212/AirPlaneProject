using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt_PO.ProjectObjects;

namespace Projekt_PO.Factory
{
    public class CargoPlaneFactory : IObjectFactory
    {
        public Myobject CreateObject(params string[] data)
        {
            ulong id = ulong.Parse(data[1]);
            string Serial = data[2];
            string Country = data[3];
            string Model = data[4];
            float MaxLoad = float.Parse(data[5], System.Globalization.CultureInfo.InvariantCulture);

            return new CargoPlane(id, Serial, Country, Model, MaxLoad);
        }
        public Myobject CreateObject(byte[] messageBytes)
        {
            int FML = BitConverter.ToInt32(messageBytes, 3);
            ulong id = BitConverter.ToUInt64(messageBytes, 7);
            string Serial = Encoding.ASCII.GetString(messageBytes,15,10);
            string Country = Encoding.ASCII.GetString(messageBytes, 25, 3);
            ushort ML = BitConverter.ToUInt16(messageBytes, 28);
            string Model = Encoding.ASCII.GetString(messageBytes, 30, ML);
            float MaxLoad = BitConverter.ToSingle(messageBytes, 30+ML);

            return new CargoPlane(id, Serial, Country, Model, MaxLoad);
        }
    }


}
