using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Projekt1
{
    [Serializable] // information that seralizations of My object class is possible
    // inofmations these types are derviatives of class, object will be marked after serialzation
    [JsonDerivedType(typeof(Airport), "Airport")]
    [JsonDerivedType(typeof(PassengerPlane), "PassengerPlane")]
    [JsonDerivedType(typeof(Passenger), "Passenger")]
    [JsonDerivedType(typeof(Cargo), "Cargo")]
    [JsonDerivedType(typeof(CargoPlane), "CargoPlane")]
    [JsonDerivedType(typeof(Flight), "Flight")]
    [JsonDerivedType(typeof(Crew), "Crew")]

    public abstract class Myobject // Main class, derivatives of this class are Airport,Cargo e.t.c
    {
    }

    public interface IObjectFactory // interface which define a function creating the objects
    {
        Myobject CreateObject(params string[] data);
    }

    public static class EntityFactory // class which helps us to select the appropriate constructor
    {
        public static IObjectFactory GetEntityFactory(string firstElement)
        {
            Dictionary<string, IObjectFactory> factoryMap = new Dictionary<string, IObjectFactory>
            {
                { "AI", new AirportFactory() },
                { "CP", new CargoPlaneFactory() },
                { "CA", new CargoFactory() },
                { "C", new CrewFactory() },
                { "P", new PassengerFactory() },
                { "PP", new PassengerPlaneFactory() },
                { "FL", new FlightFactory() }
            };

            if (factoryMap.TryGetValue(firstElement, out IObjectFactory factory))
            {
                return factory; // it return facotry class where is constructor call about appropriate class
            }
            else
            {
                throw new ArgumentException($"Unsupported entity: {firstElement}");
            }
        }
    }
}
