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
using System.Collections.Concurrent;
using Avalonia.Controls.ApplicationLifetimes;

namespace Projekt_PO
{
    class Program
    {
        static void Main()
        {
            // Take the data from file and make objects
            string currentDirectory = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(currentDirectory, "example_data.ftr");
            string[] lines = File.ReadAllLines(filePath);
            List<Myobject> objects = new List<Myobject>();
            objects = LoadingData.DataProcesor(lines);

            // Take a path to file to make a change simulator
            string filePath1 = Path.Combine(currentDirectory, "example.ftre");

            // Make a log file
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            string logFileName = $"log_{currentDate}.txt";
            string logFilePath = Path.Combine(currentDirectory, logFileName);

            int minTime = 1; // In milliseconds
            int maxTime = 5; // In milliseconds

            // Create the server simulator
            NetworkSourceSimulator.NetworkSourceSimulator source = new NetworkSourceSimulator.NetworkSourceSimulator(filePath1, minTime, maxTime);

            // We create a source data service object and run the data source
            DataSourceService dataSourceService = new DataSourceService(source, logFilePath);
            dataSourceService.entities = objects;


            Thread apka = new Thread(new ThreadStart(Runner.Run));
            apka.Start();

            bool isRunning = true; // This flag tells us if the program is still running

            Thread mapViewThread = new Thread(() => FlightsVisualization.MapView(dataSourceService, ref isRunning)) { IsBackground = true };
            mapViewThread.Start();

            // Make a pause to watch the uploaded changes
            int delayMilliseconds = 2000;
            Task.Delay(delayMilliseconds).Wait();
            dataSourceService.Start();

            dataSourceService.GenerateMediaList(); // Generate a media List

            // A loop that listens for commands entered through the console
            while (isRunning)
            {
                Console.WriteLine("Type 'print' to take a snapshot,'report' to take a media report, 'exit' to exit.");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "report":
                        dataSourceService.TakeReport();
                        break;
                    case "print":
                        dataSourceService.TakeSnapshot();
                        break;
                    case "exit":
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid command.");
                        break;
                }
            }

            dataSourceService.LogToFile("Close Applications\n\n");
            Console.WriteLine("The app has been disabled.");
        }
    }
}
