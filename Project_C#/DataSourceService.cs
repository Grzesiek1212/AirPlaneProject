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
        public List<Myobject> entities = new List<Myobject>();
        public AirportFlightLists airportFlightLists = AirportFlightLists.Instance;
        public List<MyMedia> medias = new List<MyMedia>();
        public event IDUpdate OnIDUpdate;
        public event PositionUpdate OnPositionUpdate;
        public event ContactInfoUpdate OnContactInfoUpdate;


        public DataSourceService(NetworkSourceSimulator.NetworkSourceSimulator source) // Class Constructor
        {
            this.source = source;
            this.source.OnIDUpdate += HandleIDUpdate;
            this.source.OnPositionUpdate += HandlePositionUpdate;
            this.source.OnContactInfoUpdate += HandleContactInfoUpdate;
        }

        public void Start() // function that start our program
        {

            //creating a thread that will broadcast messages
            Thread thread = new Thread(new ThreadStart(source.Run))
            {
                // dies automatically when program exits
                // https://learn.microsoft.com/en-us/dotnet/api/system.threading.thread.isbackground?view=net-8.0
                IsBackground = true
            };

            thread.Start(); // start the thread
        }

        private void OnNewDataReadyHandler(object sender, NewDataReadyArgs args) // Event Handler Method
        {
            ProcessNewData(args.MessageIndex);
        }

        private void HandleIDUpdate(object sender, IDUpdateArgs args)
        {
            foreach (var entity in entities)
            {
                if (entity.ID == args.ObjectID)
                {
                    entity.ID = args.NewObjectID;
                    // Loguj zmianę ID do pliku
                    break;
                }
            }
        }

        private void HandlePositionUpdate(object sender, PositionUpdateArgs args)
        {
            int delayMilliseconds = 200;
            Task.Delay(delayMilliseconds).Wait();
            foreach (var entity in airportFlightLists.flights)
            {
                if (entity.Value.ID == args.ObjectID)
                {
                    entity.Value.Longitude = args.Longitude;
                    entity.Value.Latitude = args.Latitude;
                    entity.Value.AMSL = args.AMSL;
                    entity.Value.LatitudeStart = args.Latitude;
                    entity.Value.LongitudeStart = args.Longitude;
                    // Loguj zmianę pozycji do pliku
                    break;
                }
            }
        }

        private void HandleContactInfoUpdate(object sender, ContactInfoUpdateArgs args)
        {
            foreach (var entity in airportFlightLists.people)
            {
                if (entity.Value.ID == args.ObjectID)
                {
                    entity.Value.Phone = args.PhoneNumber;
                    entity.Value.Email = args.EmailAddress;
                    // Loguj zmianę danych kontaktowych do pliku
                    break;
                }
            }
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

            entities.Add(entity);
        }

        public void TakeSnapshot()  // Takes a snapshot of the collected entities and saves it to a JSON file.
        {
            // there we create the fileName
            string currentTime = DateTime.Now.ToString("HH_mm_ss");
            string snapshotFileName = $"snapshot_{currentTime}.json";

            // using our serialization methods
            DataSerializer serializer = new MyJsonSerializer();
            string json = serializer.Serialize(entities);

            // Write our inforamtions
            File.WriteAllText(snapshotFileName, json);
            Console.WriteLine($"Snapshot został zapisany do pliku: {snapshotFileName}");

        }

        public void GenerateMediaList() // function which generate media list
        {
            medias.Add(new Televison("Telewizja Abelowa"));
            medias.Add(new Televison("Kanał TV-tensor"));

            medias.Add(new Radio("Radio Kwantyfikator"));
            medias.Add(new Radio(" Radio Shmem"));

            medias.Add(new Newspaper("Gazeta Kategoryczna"));
            medias.Add(new Newspaper("Dziennik Politechniczny"));

        }

        public void TakeReport() // function which make a report
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
    }

}
