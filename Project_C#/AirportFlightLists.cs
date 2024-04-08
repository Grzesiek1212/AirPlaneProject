using Projekt_PO.ProjectObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Projekt_PO;

namespace Project_C_
{
    public class AirportFlightLists
    {
        // Create the SINGLETON class which contains  two Dictionary 
        // Dictionary calls flights contains all flights in our Data
        // Dictionary calls airports contains all airports in our Data

        private static readonly object padlock = new object();
        private static AirportFlightLists instance = null;

        public ConcurrentDictionary<ulong, Flight> flights;
        public ConcurrentDictionary<ulong, Airport> airports;
        public List<IReportable> objects;

        public AirportFlightLists()
        {
            flights = new ConcurrentDictionary<ulong, Flight>();
            airports = new ConcurrentDictionary<ulong, Airport>();
            objects = new List<IReportable>();
        }
        public static AirportFlightLists Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new AirportFlightLists();
                    }
                    return instance;
                }
            }
        }
        public void AddFlight(Flight flight)
        {
            lock (flights) // we must lock dictionary
            {
                flights.TryAdd(flight.ID, flight);
            }
        }
        public void RemoveFlight(Flight flight)
        {
            lock (flights) // we must lock dictionary
            {
                flights.TryRemove(flight.ID, out _);
            }
        }
        public void AddAirport(Airport airport)
        {
            lock (airports) // we must lock dictionary
            {
                airports.TryAdd(airport.ID, airport);
            }
        }

        public void AddIreportableObject(IReportable myobject)
        {
            lock (objects) // we must lock list
            {
                objects.Add(myobject);
            }
        }

        public void removeIreportableObject(IReportable myobject)
        {
            lock (objects) // we must lock list
            {
                objects.Remove(myobject);
            }
        }

        public void RemoveAirport(Airport airport)
        {
            lock (airports) // we must lock dictionary
            {
                airports.TryRemove(airport.ID, out _);
            }
        }
        public ConcurrentDictionary<ulong, Flight> GetFlights()
        {
            // When we want get flights dictionary, we lock it and return the copy of it
            lock (flights)
            {
                return new ConcurrentDictionary<ulong, Flight>(flights);
            }
        }
        public ConcurrentDictionary<ulong, Airport> GetAirports()
        {
            // When we want get airports dictionary, we lock it and return the copy of it
            lock (airports)
            {
                return new ConcurrentDictionary<ulong, Airport>(airports);
            }
        }

        public List<IReportable> GetIreportableObject()
        {
            // When we want get airports dictionary, we lock it and return the copy of it
            lock (objects)
            {
                return new List<IReportable>(objects);
            }
        }
    }
}
