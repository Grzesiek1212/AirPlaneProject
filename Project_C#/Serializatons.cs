using NetworkSourceSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Projekt_PO
{
    public abstract class DataSerializer // This main class is resposible for Serialize
    {
        public abstract string Serialize(List<Myobject> entities, JsonSerializerOptions? options = null);
    }

    public class MyJsonSerializer : DataSerializer // Deriviatives of DataSerializer class who define which type of serialize we want
    {
        public override string Serialize(List<Myobject> entities, JsonSerializerOptions? options = null)
        {
            return JsonSerializer.Serialize(entities, new JsonSerializerOptions { WriteIndented = true });

        }
    }


}
