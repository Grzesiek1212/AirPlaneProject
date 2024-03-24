using Projekt_PO;
using Projekt_PO.ProjectObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_C_.ServerFactory
{
    public class PassengerServerFactory : IObjectServerFactory
    {
        public Myobject CreateObject(byte[] messageBytes, AirportFlightLists airportFlightLists)
        {
            int FML = BitConverter.ToInt32(messageBytes, 3);
            ulong ID = BitConverter.ToUInt64(messageBytes, 7);
            ushort NL = BitConverter.ToUInt16(messageBytes, 15);
            string Name = Encoding.ASCII.GetString(messageBytes, 17, NL);
            ushort Age = BitConverter.ToUInt16(messageBytes, 17 + NL);
            string Phone = Encoding.ASCII.GetString(messageBytes, 19 + NL, 12);
            ushort EL = BitConverter.ToUInt16(messageBytes, 31 + NL);
            string Email = Encoding.ASCII.GetString(messageBytes, 33 + NL, EL);
            string Class = Encoding.ASCII.GetString(messageBytes, 33 + NL + EL, 1);
            ulong Miles = BitConverter.ToUInt64(messageBytes, 34 + NL + EL);

            return new Passenger(ID, Name, Age, Phone, Email, Class, Miles);
        }
    }
}
