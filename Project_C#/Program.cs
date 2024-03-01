using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Projekt_PO
{
    class Program
    {
        static void Main(string[] args)
        {
            // there we open a file
            string currentDirectory = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(currentDirectory, "example_data.ftr");
            string[] lines = File.ReadAllLines(filePath);
           
            // Loading Data by DataProcesor function
            List < Myobject > entities = LoadingData.DataProcesor(lines);

            // we create the object of Myjsonserilizer class to use the function of this class which serialize to this type which we want
            DataSerializer serializer = new MyJsonSerializer();
            string json = serializer.Serialize(entities);

            // we set the file path of our file and  write down our serializations
            string outputFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output.json");
            File.WriteAllText(outputFilePath, json);

        }

    }
}
