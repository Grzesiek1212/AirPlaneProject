using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt_PO.ProjectObjects;

namespace Projekt_PO.Factory
{
    public class PassengerPlaneFactory : IObjectFactory
    {
        public Myobject CreateObject(params string[] data)
        {
            ulong id = ulong.Parse(data[1]);
            string Serial = data[2];
            string Country = data[3];
            string Model = data[4];
            ushort FirstClassSize = ushort.Parse(data[5]);
            ushort BusinessClassSize = ushort.Parse(data[6]);
            ushort EconomyClassSize = ushort.Parse(data[7]);

            return new PassengerPlane(id, Serial, Country, Model, FirstClassSize, BusinessClassSize, EconomyClassSize);

        }
        public Myobject CreateObject(byte[] messageBytes)
        {
            int FML = BitConverter.ToInt32(messageBytes, 3);
            ulong id = BitConverter.ToUInt64(messageBytes, 7);
            string Serial = Encoding.ASCII.GetString(messageBytes, 15, 10);
            string Country = Encoding.ASCII.GetString(messageBytes, 25, 3);
            ushort ML = BitConverter.ToUInt16(messageBytes, 28);
            string Model = Encoding.ASCII.GetString(messageBytes, 30, ML);
            ushort FirstClassSize = BitConverter.ToUInt16(messageBytes, 30 + ML);
            ushort BusinessClassSize = BitConverter.ToUInt16(messageBytes, 32 + ML);
            ushort EconomyClassSize = BitConverter.ToUInt16(messageBytes, 34 + ML);

            return new PassengerPlane(id, Serial, Country, Model, FirstClassSize, BusinessClassSize, EconomyClassSize);
        }
    }
}
