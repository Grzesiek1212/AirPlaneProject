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
            // we create a Data Source Object
            string currentDirectory = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(currentDirectory, "example_data.ftr");
            bool isRunning = true; // this flag tells us if the program is still running

            int minTime = 1; // in milliseconds
            int maxTime = 5; // in milliseconds

            // create the server simulator
            NetworkSourceSimulator.NetworkSourceSimulator source = new NetworkSourceSimulator.NetworkSourceSimulator(filePath, minTime, maxTime);

            // we create a source data service object and run the data source
            DataSourceService dataSourceService = new DataSourceService(source);
            dataSourceService.Start();


            Thread apka = new Thread(new ThreadStart(Runner.Run));
            apka.Start();
            Thread mapViewThread = new Thread(() => FlightsVisualization.MapView(dataSourceService, ref isRunning)) { IsBackground = true};
            mapViewThread.Start();



            bool takeSnapshot = false; // this flag tells if the program do a Snapshot
            bool takereport = false; // this flag tells us if the program do a report

            dataSourceService.GenerateMediaList(); // Generate a media List

            // A loop that listens for commands entered through the console
            while (isRunning)
            {
                Console.WriteLine("Type 'print' to take a snapshot,'report' to take a media report, 'exit' to exit.");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "report":
                        takereport = true; // Set a report flag
                        break;
                    case "print":
                        takeSnapshot = true; // Set snapshot flag
                        break;
                    case "exit":
                        isRunning = false;
                        mapViewThread.Join();
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
                if (takereport)
                {
                    dataSourceService.TakeReport();
                    takereport = false; // Reset report flag
                }
            }

            Console.WriteLine("The app has been disabled.");

        }
    }
}
