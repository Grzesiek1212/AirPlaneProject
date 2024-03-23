using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using NetworkSourceSimulator;
using System.Net.Http.Json;
using System.Xml.Linq;
using Project_C_;
using FlightTrackerGUI;
using Mapsui.Projections;
using Projekt_PO.ProjectObjects;
using System.Globalization;
using Mapsui;
using System.Drawing;
using System.Collections;

namespace Projekt_PO
{
    class Program
    {
        static void Main(string[] args)
        {
            // we create a Data Source Object
            string currentDirectory = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(currentDirectory, "example_data.ftr");

            int minTime = 1; // in milliseconds
            int maxTime = 5; // in milliseconds

            // create the server simulator
            NetworkSourceSimulator.NetworkSourceSimulator source = new NetworkSourceSimulator.NetworkSourceSimulator(filePath, minTime, maxTime);

            // we create a source data service object and run the data source
            DataSourceService dataSourceService = new DataSourceService(source);
            dataSourceService.Start();


            Thread apka = new Thread(new ThreadStart(Runner.Run));
            apka.Start();
            MapView(dataSourceService);


            bool takeSnapshot = false; // this flag tells if the prgram do a Snapshot
            bool isRunning = true; // this flag tells us if the program is still running
            // A loop that listens for commands entered through the console
            while (isRunning)
            {
                Console.WriteLine("Type 'print' to take a snapshot, 'exit' to exit.");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "print":
                        takeSnapshot = true; // Set snapshot flag
                        break;
                    case "exit":
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid command.");
                        break;
                }
                if (takeSnapshot)
                {
                    dataSourceService.TakeSnapshot();
                    takeSnapshot = false; // Reset snapshot flag
                }
            }

            Console.WriteLine("The app has been disabled.");

        }


        
        public static void MapView(DataSourceService dataSourceService)
        {
            FlightsGUIData flightsGUIData = new FlightsGUIData();
            List<FlightGUI> flightsGUI = new List<FlightGUI>();
            FlightGUI flightGUI;
            List<Flight> flights, actualflights = new List<Flight>();
            List<Airport > airports;
            double currentTime,startSec, endSec;
            TimeOnly start = new TimeOnly();
            TimeOnly end = new TimeOnly();
            while (true)
            {
                currentTime = (DateTime.Now - DateTime.Now.Date).TotalSeconds;
                flights = dataSourceService.airportFlightLists.GetFlights();
                airports = dataSourceService.airportFlightLists.GetAirports();

                lock (flights)
                {
                    foreach (Flight flight in flights)
                    {
                        TimeOnly.TryParse(flight.TakeoffTime, out start);
                        TimeOnly.TryParse(flight.LandingTime, out end);

                        startSec = (start - new TimeOnly(0, 0)).TotalSeconds;
                        endSec = (end - new TimeOnly(0, 0)).TotalSeconds;

                        if (startSec > endSec) endSec += 24 * 3600;

                        if (endSec < currentTime)
                        {
                            actualflights.Remove(flight);
                            continue;
                        }

                        if (startSec < currentTime && !actualflights.Contains(flight))
                            actualflights.Add(flight);
                    }
                }

                flightsGUI.Clear();

                foreach (Flight flight in actualflights)
                {
                    Airport originAirport = airports.FirstOrDefault(a => a.ID == flight.Origin);
                    Airport targetAirport = airports.FirstOrDefault(a => a.ID == flight.Target);

                    UpgradePosition(flight, originAirport, targetAirport,currentTime);
                    double mapCoordRotation = calculateCoordRotation(originAirport,targetAirport);

                    flightGUI = ConvertFlightToFlightGUI(flight,mapCoordRotation);
                    flightsGUI.Add(flightGUI);
                }

                //Console.WriteLine(flightsGUI.Count);

                flightsGUIData.UpdateFlights(flightsGUI);
                Runner.UpdateGUI(flightsGUIData);
                Thread.Sleep(1);

            }
        }

        private static double calculateCoordRotation(Airport originAirport, Airport targetAirport)
        {
            // Convert the longitude and latitude of the origin airport to X,Y coordinates on the map
            (double x_start, double y_start) = SphericalMercator.FromLonLat(originAirport.Longitude, originAirport.Latitude);

            // Convert the longitude and latitude of the target airport to X,Y coordinates on the map
            (double x_end, double y_end) = SphericalMercator.FromLonLat(targetAirport.Longitude, targetAirport.Latitude);

            // Calculate the difference in X and Y coordinates between airports
            double deltaX = x_end - x_start;
            double deltaY = y_end - y_start;

            // Calculate the angle between two points
            double angle = Math.Atan2(deltaX, deltaY);

            return angle;
        }


        private static void UpgradePosition(Flight flight, Airport originAirport, Airport targetAirport, double currentTime)
        {
            double startSec, endSec;
            TimeOnly start;
            TimeOnly end;

            // Find the departure and arrival times
            TimeOnly.TryParse(flight.TakeoffTime, out start);
            TimeOnly.TryParse(flight.LandingTime, out end);
            startSec = (start - new TimeOnly(0, 0)).TotalSeconds;
            endSec = (end - new TimeOnly(0, 0)).TotalSeconds;
            if (startSec > endSec) endSec += 24 * 3600;

            // Calculate the time difference between departure and arrival
            double totalFlightTime = endSec - startSec;
            double elapsedFlightTime = currentTime - startSec;

            if ( endSec < currentTime) Console.WriteLine(1);
            // Calculate the progress of the flight based on elapsed time
            double progress = elapsedFlightTime / totalFlightTime;
            //Console.WriteLine(progress);
            float distance_x = targetAirport.Longitude - originAirport.Longitude;
            float distance_y = targetAirport.Latitude - originAirport.Latitude;

            float latitude = (float)(originAirport.Latitude + progress * distance_y);
            float longitude = (float)(originAirport.Longitude + progress * distance_x);
            

            // Update flight coordinates
            flight.Latitude = latitude;
            flight.Longitude = longitude;
        }

        public static FlightGUI ConvertFlightToFlightGUI(Flight flight,double MapcoordRotation)
        {
            ulong ID = flight.ID;
            WorldPosition position = new WorldPosition(flight.Latitude, flight.Longitude);

            FlightGUI flightGUI = new FlightGUI
            {
                ID = ID,
                WorldPosition = position,
                MapCoordRotation = MapcoordRotation,
            };

            return flightGUI;
        }
      
    }
}
