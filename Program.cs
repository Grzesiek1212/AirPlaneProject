using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Projekt1
{
    class Program
    {
        static void Main(string[] args)
        {
            // there we open a file
            string currentDirectory = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(currentDirectory, "example_data.ftr");

            List<Myobject> entities = new List<Myobject>();

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] elements = line.Split(',');

                string firstElement = elements[0];

                // we use our fuction to take a appropriate factory
                IObjectFactory factory = EntityFactory.GetEntityFactory(firstElement);

                // there we create a object and we add it to our list
                Myobject entity = factory.CreateObject(elements);
                entities.Add(entity);
            }

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            // we create the object of Myjsonserilizer class to use the function of this class which serialize to this type which we want
            DataSerializer serializer = new MyJsonSerializer();
            string json = serializer.Serialize(entities, options);

            // we set the file path of our file and  write down our serializations
            string outputFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output.json");
            File.WriteAllText(outputFilePath, json);

        }

    }
}
