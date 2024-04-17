using Projekt_PO;
using Projekt_PO.ProjectObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_C_.ServerFactory
{
    public class FlightServerFactory : IObjectServerFactory
    {
        public Myobject CreateObject(byte[] messageBytes)
        {
            AirportFlightLists airportFlightLists = AirportFlightLists.Instance;
            int FML = BitConverter.ToInt32(messageBytes, 3);
            ulong ID = BitConverter.ToUInt64(messageBytes, 7);
            ulong OriginID = BitConverter.ToUInt64(messageBytes, 15);
            ulong TargetID = BitConverter.ToUInt64(messageBytes, 23);
            Airport Origin = airportFlightLists.GetAirport(OriginID);
            Airport Target = airportFlightLists.GetAirport(TargetID);
            long takeoffEpoch = BitConverter.ToInt64(messageBytes, 31);
            long landingEpoch = BitConverter.ToInt64(messageBytes, 39);
            string TakeoffTime = DateTime.UnixEpoch.AddMilliseconds(takeoffEpoch).ToString("HH:mm:ss", CultureInfo.InvariantCulture);
            string LandingTime = DateTime.UnixEpoch.AddMilliseconds(landingEpoch).ToString("HH:mm:ss", CultureInfo.InvariantCulture);
            float Longitude = -1;
            float Latitude = -1;
            float AMSL = -1;
            ulong Plane_id = BitConverter.ToUInt64(messageBytes, 47);
            ushort CC = BitConverter.ToUInt16(messageBytes, 55);
            ushort PCC = BitConverter.ToUInt16(messageBytes, 57 + 8 * CC);

            List<ulong> Crew_ids = new List<ulong>();
            List<ulong> Load_ids = new List<ulong>();

            for (int i = 0; i < CC; i++)
            {
                ulong ID_crew = BitConverter.ToUInt64(messageBytes, 57 + i * 8);
                Crew_ids.Add(ID_crew);
            }

            for (int i = 0; i < PCC; i++)
            {
                ulong ID_Load = BitConverter.ToUInt64(messageBytes, 59 + 8 * CC + i * 8);
                Load_ids.Add(ID_Load);
            }

            Flight flight = new Flight(ID, Origin, Target, TakeoffTime, LandingTime, Longitude, Latitude, AMSL, Plane_id, Crew_ids, Load_ids);

            airportFlightLists.AddFlight(flight);

            return flight;
        }
    }
}
