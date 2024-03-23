using Projekt_PO.ProjectObjects;
using Projekt_PO;
using System.Text;

namespace Project_C_.ServerFactory
{
    internal class AirportServerFactory : IObjectServerFactory
    {
        public Myobject CreateObject(byte[] messageBytes)
        {
            int FML = BitConverter.ToInt32(messageBytes, 3);
            ulong ID = BitConverter.ToUInt64(messageBytes, 7);
            ushort NL = BitConverter.ToUInt16(messageBytes, 15);
            string Name = Encoding.ASCII.GetString(messageBytes, 17, NL);
            string Code = Encoding.ASCII.GetString(messageBytes, 17 + NL, 3);
            float Longitude = BitConverter.ToSingle(messageBytes, 20 + NL);
            float latitude = BitConverter.ToSingle(messageBytes, 24 + NL);
            float AMSL = BitConverter.ToSingle(messageBytes, 28 + NL);
            string Country = Encoding.ASCII.GetString(messageBytes, 32 + NL, 3);

            return new Airport(ID, Name, Code, Longitude, latitude, AMSL, Country);
        }
    }
}
