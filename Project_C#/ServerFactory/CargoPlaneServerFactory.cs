using Projekt_PO.ProjectObjects;
using Projekt_PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_C_.ServerFactory
{
    public class CargoPlaneServerFactory : IObjectServerFactory
    {
        public Myobject CreateObject(byte[] messageBytes, AirportFlightLists airportFlightLists)
        {
            int FML = BitConverter.ToInt32(messageBytes, 3);
            ulong id = BitConverter.ToUInt64(messageBytes, 7);
            string Serial = Encoding.ASCII.GetString(messageBytes, 15, 10);
            string Country = Encoding.ASCII.GetString(messageBytes, 25, 3);
            ushort ML = BitConverter.ToUInt16(messageBytes, 28);
            string Model = Encoding.ASCII.GetString(messageBytes, 30, ML);
            float MaxLoad = BitConverter.ToSingle(messageBytes, 30 + ML);

            return new CargoPlane(id, Serial, Country, Model, MaxLoad);
        }
    }
}
