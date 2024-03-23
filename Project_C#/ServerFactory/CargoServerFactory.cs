using Projekt_PO.ProjectObjects;
using Projekt_PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_C_.ServerFactory
{
    public class CargoServerFactory : IObjectServerFactory
    {
        public Myobject CreateObject(byte[] messageBytes)
        {
            int FML = BitConverter.ToInt32(messageBytes, 3);
            ulong ID = BitConverter.ToUInt64(messageBytes, 7);
            float Weight = BitConverter.ToSingle(messageBytes, 15);
            string Code = Encoding.ASCII.GetString(messageBytes, 19, 6);
            ushort DL = BitConverter.ToUInt16(messageBytes, 25);
            string Description = Encoding.ASCII.GetString(messageBytes, 27, DL);

            return new Cargo(ID, Weight, Code, Description);
        }
    }
}
