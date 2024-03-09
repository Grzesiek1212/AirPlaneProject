using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Projekt_PO.ProjectObjects;

namespace Projekt_PO.Factory
{
    public class CrewFactory : IObjectFactory
    {
        public Myobject CreateObject(params string[] data)
        {
            ulong ID = ulong.Parse(data[1]);
            string Name = data[2];
            ulong Age = ulong.Parse(data[3]);
            string Phone = data[4];
            string Email = data[5];
            ushort Practice = ushort.Parse(data[6]);
            string Role = data[7];

            return new Crew(ID, Name, Age, Phone, Email, Practice, Role);
        }
        public Myobject CreateObject(byte[] messageBytes)
        {
            int FML = BitConverter.ToInt32(messageBytes, 3);
            ulong ID = BitConverter.ToUInt64(messageBytes, 7);
            ushort NL = BitConverter.ToUInt16(messageBytes, 15);
            string Name = Encoding.ASCII.GetString(messageBytes, 17, NL);
            ushort Age = BitConverter.ToUInt16(messageBytes, 17 + NL);
            string Phone = Encoding.ASCII.GetString(messageBytes, 19 + NL, 12);
            ushort EL = BitConverter.ToUInt16(messageBytes, 31 + NL);
            string Email = Encoding.ASCII.GetString(messageBytes, 33 + NL, EL);
            ushort Practice = BitConverter.ToUInt16(messageBytes, 33 + NL + EL);
            string Role = Encoding.ASCII.GetString(messageBytes, 35 + NL + EL, 1);

            return new Crew(ID, Name, Age, Phone, Email, Practice, Role);
        }
    }
}
