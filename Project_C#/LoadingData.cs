using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Projekt_PO.Factory;

namespace Projekt_PO
{
    public class LoadingData
    {
        public static List<Myobject> DataProcesor(string[] lines)
        {
            List<Myobject> entities = new List<Myobject>();
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
            return entities;
        }

    }



}
