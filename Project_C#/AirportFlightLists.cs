using Projekt_PO.ProjectObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_C_
{
    public class AirportFlightLists
    {
        public List<Flight> flights;
        public List<Airport> airports;

        public AirportFlightLists()
        {
            flights = new List<Flight>();
            airports = new List<Airport>();
        }

        public void AddFlight(Flight flight)
        {
            lock (flights)
            {
                if (flight != null)
                {
                    flights.Add(flight);
                }
            }
        }

        public void RemoveFlight(Flight flight)
        {
            if (flights.Contains(flight))
                flights.Remove(flight);
        }

        public void AddAirport(Airport airport)
        {
            lock (airports)
            {
                if (airport != null)
                    airports.Add(airport);
            }
        }

        public void RemoveAirport(Airport airport)
        {
            if (airports.Contains(airport))
                airports.Remove(airport);
        }

        public List<Flight> GetFlights()
        {
            lock (flights)
            {
                return flights;
            }
        }


        public List<Airport> GetAirports()
        {
            lock (airports)
            {
                return airports;
            }
        }

    }
}
