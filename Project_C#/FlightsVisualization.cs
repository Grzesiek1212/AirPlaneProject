using FlightTrackerGUI;
using Mapsui.Projections;
using Projekt_PO.ProjectObjects;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_C_
{
    public class FlightsVisualization
    {
        public static void MapView(DataSourceService dataSourceService)
        {
            FlightsGUIData flightsGUIData = new FlightsGUIData();
            List<FlightGUI> flightsGUI = new List<FlightGUI>();
            List<Flight> actualflights = new List<Flight>();
            double currentTime;

            while (true)
            {
                // Take current Time in seconds, we do not take into account days, months and years in this simulation
                currentTime = (DateTime.Now - DateTime.Now.Date).TotalSeconds;

                // Actualization of actualflights list
                UpdateActualFlightsList(dataSourceService, currentTime, actualflights);

                // GUI actualization
                UpdateGUI(dataSourceService, actualflights, flightsGUI, flightsGUIData, currentTime);

                Thread.Sleep(1);
            }
        }
        private static void UpdateActualFlightsList(DataSourceService dataSourceService, double currentTime, List<Flight> actualflights)
        {
            // Go for copy of Dictionary of all flights in data 
            foreach (var flight in dataSourceService.airportFlightLists.GetFlights())
            {
                // Parse the take of time and landing time to number of seconds
                TimeOnly.TryParse(flight.Value.TakeoffTime, out TimeOnly start);
                TimeOnly.TryParse(flight.Value.LandingTime, out TimeOnly end);
                double startSec = (start - new TimeOnly(0, 0)).TotalSeconds;
                double endSec = (end - new TimeOnly(0, 0)).TotalSeconds;

                // if take off time is later than landing time we predict that we are landing in next day
                if (startSec >= endSec) endSec += 24 * 3600;

                // choose the flights which are actual
                if (endSec <= currentTime)
                {
                    actualflights.Remove(flight.Value);
                    continue;
                }
                if (startSec <= currentTime && !actualflights.Contains(flight.Value))
                    actualflights.Add(flight.Value);
            }
        }
        private static void UpdateGUI(DataSourceService dataSourceService, List<Flight> actualflights, List<FlightGUI> flightsGUI, FlightsGUIData flightsGUIData, double currentTime)
        {
            flightsGUI.Clear();
            ConcurrentDictionary<ulong, Airport> airports = dataSourceService.airportFlightLists.GetAirports();

            foreach (Flight flight in actualflights)
            {
                // Find the origin airport and target airport of the flight we to consider
                Airport originAirport = flight.Origin;
                Airport targetAirport = flight.Target;

                // Calculate the ratation of the plane in the flight
                double mapCoordRotation = CalculateCoordRotation(originAirport, targetAirport);

                // Update the plane localization
                UpgradePosition(flight, originAirport, targetAirport, currentTime);

                // Convert the type of object
                FlightGUI flightGUI = ConvertFlightToFlightGUI(flight, mapCoordRotation);

                // Add to list
                flightsGUI.Add(flightGUI);
            }

            // Update the the list
            flightsGUIData.UpdateFlights(flightsGUI);
            Runner.UpdateGUI(flightsGUIData);
        }
        private static double CalculateCoordRotation(Airport originAirport, Airport targetAirport)
        {
            // Convert the longitude and latitude of the origin airport to X,Y coordinates on the map
            (double x_start, double y_start) = SphericalMercator.FromLonLat(originAirport.Longitude, originAirport.Latitude);

            // Convert the longitude and latitude of the target airport to X,Y coordinates on the map
            (double x_end, double y_end) = SphericalMercator.FromLonLat(targetAirport.Longitude, targetAirport.Latitude);

            // Calculate the difference in X and Y coordinates between airports
            double deltaX = x_end - x_start;
            double deltaY = y_end - y_start;

            // Calculate the angle between two points and return
            return Math.Atan2(deltaX, deltaY);
        }
        private static void UpgradePosition(Flight flight, Airport originAirport, Airport targetAirport, double currentTime)
        {
            double startSec, endSec;
            TimeOnly start, end;

            // Find the departure and arrival times
            TimeOnly.TryParse(flight.TakeoffTime, out start);
            TimeOnly.TryParse(flight.LandingTime, out end);
            startSec = (start - new TimeOnly(0, 0)).TotalSeconds;
            endSec = (end - new TimeOnly(0, 0)).TotalSeconds;
            if (startSec > endSec) endSec += 24 * 3600;

            // Calculate the time difference between departure and arrival
            double totalFlightTime = endSec - startSec;
            double elapsedFlightTime = currentTime - startSec;

            // Calculate the progress of the flight based on elapsed time
            double progress = elapsedFlightTime / totalFlightTime;

            // Calculate the distance of whole filght
            float distance_x = targetAirport.Longitude - originAirport.Longitude;
            float distance_y = targetAirport.Latitude - originAirport.Latitude;

            // Update the localization of the plane
            flight.Latitude = (float)(originAirport.Latitude + progress * distance_y);
            flight.Longitude = (float)(originAirport.Longitude + progress * distance_x);
        }
        public static FlightGUI ConvertFlightToFlightGUI(Flight flight, double MapcoordRotation)
        {
            // Covert the flight object to FlightGUI object

            FlightGUI flightGUI = new FlightGUI
            {
                ID = flight.ID,
                WorldPosition = new WorldPosition(flight.Latitude, flight.Longitude),
                MapCoordRotation = MapcoordRotation,
            };

            return flightGUI;
        }
    }
}
