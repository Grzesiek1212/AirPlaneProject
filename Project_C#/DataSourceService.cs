using NetworkSourceSimulator;
using Projekt_PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Project_C_
{
    /// Represents a service responsible for handling data from a network source.
    class DataSourceService
    {
        private NetworkSourceSimulator.NetworkSourceSimulator source;
        private List<Myobject> entities = new List<Myobject>();


        public DataSourceService(NetworkSourceSimulator.NetworkSourceSimulator source) // Class Constructor
        {
            this.source = source;
        }

        public void Start() // function that start our program
        {
            source.OnNewDataReady += OnNewDataReadyHandler; //Subscription and event creation

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

        private void ProcessNewData(int messageIndex)
        {
            // we recive a information
            Message message = source.GetMessageAt(messageIndex);

            // we take a identificator of class
            string firstElement = Encoding.ASCII.GetString(message.MessageBytes, 0, 3);

            // we use our fuction to take a appropriate factory
            IObjectFactory factory = EntityFactory.GetEntityFactory1(firstElement);

            // there we create a object and we add it to our list
            Myobject entity = factory.CreateObject(message.MessageBytes);
            entities.Add(entity);

            Console.WriteLine($"Nowa wiadomość otrzymana, udało utowrzyć sie element {entity}");

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

            // clear the list
            entities.Clear();
        }
    }

}
