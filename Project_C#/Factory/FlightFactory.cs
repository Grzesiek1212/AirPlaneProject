using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt_PO.ProjectObjects;

namespace Projekt_PO.Factory
{
    public class FlightFactory : IObjectFactory
    {
        public Myobject CreateObject(params string[] data)
        {
            ulong id = ulong.Parse(data[1]);
            ulong Origin = ulong.Parse(data[2]);
            ulong Target = ulong.Parse(data[3]);
            string TakeoffTime = data[4];
            string LandingTime = data[5];
            float Longitude = float.Parse(data[6], System.Globalization.CultureInfo.InvariantCulture);
            float Latitude = float.Parse(data[7], System.Globalization.CultureInfo.InvariantCulture);
            float AMSL = float.Parse(data[8], System.Globalization.CultureInfo.InvariantCulture);
            ulong Plane_id = ulong.Parse(data[9]);

            string[] values_crew_ids = data[10].Trim('[', ']').Split(';');
            string[] values_load_ids = data[11].Trim('[', ']').Split(';');
            List<ulong> Crew_ids = new List<ulong>();
            List<ulong> Load_ids = new List<ulong>();

            for (int i = 0; i < values_crew_ids.Length; i++)
            {
                Crew_ids.Add(ulong.Parse(values_crew_ids[i]));
            }
            for (int i = 0; i < values_load_ids.Length; i++)
            {
                Load_ids.Add(ulong.Parse(values_load_ids[i]));
            }

            return new Flight(id, Origin, Target, TakeoffTime, LandingTime, Longitude, Latitude, AMSL, Plane_id, Crew_ids, Load_ids);
        }
        public Myobject CreateObject(byte[] messageBytes)
        {
            int FML = BitConverter.ToInt32(messageBytes, 3);
            ulong ID = BitConverter.ToUInt64(messageBytes, 7);
            ulong Origin = BitConverter.ToUInt64(messageBytes, 15);
            ulong Target = BitConverter.ToUInt64(messageBytes, 23);
            long takeoffEpoch = BitConverter.ToInt64(messageBytes,31);
            long landingEpoch = BitConverter.ToInt64(messageBytes,39);
            string TakeoffTime = DateTime.UnixEpoch.AddMilliseconds(takeoffEpoch).ToString(CultureInfo.InvariantCulture);
            string LandingTime = DateTime.UnixEpoch.AddMilliseconds(landingEpoch).ToString(CultureInfo.InvariantCulture);
            float Longitude = -1;
            float Latitude = -1;
            float AMSL = -1;
            ulong Plane_id = BitConverter.ToUInt64(messageBytes, 47);
            ushort CC = BitConverter.ToUInt16(messageBytes, 55);
            ushort PCC = BitConverter.ToUInt16(messageBytes, 57+8*CC);

            List<ulong> Crew_ids = new List<ulong>();
            List<ulong> Load_ids = new List<ulong>();

            for(int i = 0; i < 8;i++)
            {
                try
                {
                    ulong ID_crew = BitConverter.ToUInt64(messageBytes, 47 + i * 8);
                    Crew_ids.Add(ID_crew);

                    ulong ID_Load = BitConverter.ToUInt64(messageBytes, 59 + 8 * CC + i * 8);
                    Crew_ids.Add(ID_Load);
                }
                catch { };
            }


            return new Flight(ID, Origin, Target, TakeoffTime, LandingTime, Longitude, Latitude, AMSL, Plane_id, Crew_ids, Load_ids);
        }
    }
}
