using Mapsui.Providers.Wfs.Utilities;
using NetworkSourceSimulator;
using Project_C_.Community;
using Projekt_PO;
using Projekt_PO.ProjectObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Project_C_
{
    /// Represents a service responsible for handling data from a network source.
    public class DataSourceService
    {
        private NetworkSourceSimulator.NetworkSourceSimulator source;
        public AirportFlightLists airportFlightLists = AirportFlightLists.Instance;
        public List<MyMedia> medias = new List<MyMedia>();
        public event IDUpdate OnIDUpdate;
        public event PositionUpdate OnPositionUpdate;
        public event ContactInfoUpdate OnContactInfoUpdate;
        public string logFilePath;

        public DataSourceService(NetworkSourceSimulator.NetworkSourceSimulator source, string logFilePath) // Class Constructor
        {
            this.source = source;
            this.source.OnIDUpdate += HandleIDUpdate;
            this.source.OnPositionUpdate += HandlePositionUpdate;
            this.source.OnContactInfoUpdate += HandleContactInfoUpdate;
            this.logFilePath = logFilePath;
            LogToFile("Opening Applications"); // In the log file inform about staring write logs
        }

        public void Start() // Function that start our program
        {
            // Creating a thread that will broadcast messages
            Thread thread = new Thread(new ThreadStart(source.Run))
            {
                // Dies automatically when program exits
                // https://learn.microsoft.com/en-us/dotnet/api/system.threading.thread.isbackground?view=net-8.0
                IsBackground = true
            };

            thread.Start(); // Start the thread
        }

        private void OnNewDataReadyHandler(object sender, NewDataReadyArgs args) // Event Handler Method
        {
            ProcessNewData(args.MessageIndex);
        }

        private void HandleIDUpdate(object sender, IDUpdateArgs args) // Event Handler Method
        {
            foreach (var entity in airportFlightLists.entities)
            {
                if (entity.ID == args.ObjectID)
                {
                    // We must check that is there any object with the ID for what we want ot change
                    foreach (var entity2 in airportFlightLists.entities)
                    {
                        if (entity2.ID == args.NewObjectID)
                        {
                            LogToFile($" ERROR - Object with ID: {args.ObjectID} has aleready exists");
                            return;
                        }
                    }

                    // We write the log to the file and change
                    entity.ID = args.NewObjectID;
                    LogToFile($"Update the ID object ID: {args.ObjectID} -> {args.NewObjectID}");
                    return;
                }
            }

            // If we don't find the object with this id we write the error log
            LogToFile($" ERROR - The object with ID: {args.ObjectID} doesn't exist");
        }

        private void HandlePositionUpdate(object sender, PositionUpdateArgs args) // Event Handler Method
        {
            int delayMilliseconds = 200;
            Task.Delay(delayMilliseconds).Wait();

            foreach (var entity in airportFlightLists.flights)
            {
                if (entity.Value.ID == args.ObjectID)
                {
                    // We must check that is there any flight with the same coordinates
                    foreach (var entity2 in airportFlightLists.flights)
                    {
                        if (entity2.Value.Longitude == args.Longitude && entity2.Value.Latitude == args.Latitude && entity2.Value.AMSL == args.AMSL)
                        {
                            LogToFile($" ERROR - the Object with this co-ordinates exists ");
                            return;
                        }
                    }

                    // We write the log to the file and change
                    LogToFile($"Update the position of object with ID: {args.ObjectID} - (Longitude: {entity.Value.Longitude}, Latitude: {entity.Value.Latitude}, AMSL: {entity.Value.AMSL})--->(Longitude: {args.Longitude}, Latitude: {args.Latitude}, AMSL: {args.AMSL})");
                    entity.Value.Longitude = args.Longitude;
                    entity.Value.Latitude = args.Latitude;
                    entity.Value.AMSL = args.AMSL;
                    entity.Value.LatitudeStart = args.Latitude;
                    entity.Value.LongitudeStart = args.Longitude;
                    return;
                }
            }

            // If we don't find the object with this id we write the error log
            LogToFile($" ERROR - The object with ID: {args.ObjectID} doesn't exists");
        }

        private void HandleContactInfoUpdate(object sender, ContactInfoUpdateArgs args) // Event Handler Method
        {
            foreach (var entity in airportFlightLists.people)
            {
                if (entity.Value.ID == args.ObjectID)
                {
                    // We must check that is there any person with the same email or phone
                    foreach (var entity2 in airportFlightLists.people)
                    {
                        if (entity2.Value.Email == args.EmailAddress || entity2.Value.Phone == args.PhoneNumber)
                        {
                            LogToFile($" ERROR - The human with the same email or phone number exists");
                            return;
                        }
                    }

                    // We write the log to the file nad change
                    LogToFile($"Update the contact adress of object ID: {args.ObjectID} - (Email: {entity.Value.Email}, Phone: {entity.Value.Phone})--->(Email: {args.EmailAddress}, Phone: {args.PhoneNumber}) ");
                    entity.Value.Phone = args.PhoneNumber;
                    entity.Value.Email = args.EmailAddress;
                    return;
                }
            }
            // If we don't find the object with this id we write the error log
            LogToFile($" ERROR - The object with ID: {args.ObjectID} doesn't exist");
        }
        private void ProcessNewData(int messageIndex)
        {
            // we recive a information
            Message message = source.GetMessageAt(messageIndex);

            // we take a identificator of class
            string firstElement = Encoding.ASCII.GetString(message.MessageBytes, 0, 3);

            // we use our fuction to take a appropriate factory
            IObjectServerFactory factory = EntityFactory.GetEntityFactory1(firstElement);

            // there we create a object and we add it to our list
            Myobject entity = factory.CreateObject(message.MessageBytes);

            airportFlightLists.entities.Add(entity);
        }

        public void TakeSnapshot()  // Takes a snapshot of the collected entities and saves it to a JSON file.
        {
            // There we create the fileName
            string currentTime = DateTime.Now.ToString("HH_mm_ss");
            string snapshotFileName = $"snapshot_{currentTime}.json";

            // Using our serialization methods
            DataSerializer serializer = new MyJsonSerializer();
            string json = serializer.Serialize(airportFlightLists.entities);

            // Write our inforamtions
            File.WriteAllText(snapshotFileName, json);
            Console.WriteLine($"Snapshot has been written to file: {snapshotFileName}");

        }

        public void GenerateMediaList() // Function which generate media list
        {
            medias.Add(new Televison("Telewizja Abelowa"));
            medias.Add(new Televison("Kanał TV-tensor"));

            medias.Add(new Radio("Radio Kwantyfikator"));
            medias.Add(new Radio(" Radio Shmem"));

            medias.Add(new Newspaper("Gazeta Kategoryczna"));
            medias.Add(new Newspaper("Dziennik Politechniczny"));

        }

        public void TakeReport() // Function which make a report
        {
            // Construct the newsGenerator object
            Newsgenerator newsgenerator = new Newsgenerator(medias, airportFlightLists.objects);

            string news;

            // generate a new message until it is possible
            while ((news = newsgenerator.GenerateNextNews()) != null)
            {
                Console.WriteLine(news);
            }
        }
        public void LogToFile(string logMessage)
        {
            // there we write the text to the log file
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine($"{DateTime.Now:HH:mm:ss} - {logMessage}");
            }
        }
    }

}
