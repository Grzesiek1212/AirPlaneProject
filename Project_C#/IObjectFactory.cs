using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using Projekt_PO.ProjectObjects;
using Projekt_PO.Factory;
using Project_C_.ServerFactory;
using Project_C_;

namespace Projekt_PO
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

    public abstract class Plane:Myobject {
        public ulong ID { get; set; }
        public string? Serial { get; set; }
        public string? Country { get; set; }
        public string? Model { get; set; }
    };

    public abstract class MyMedia : IMediaVisitor // Main class, derivatives of this class are Television, Radio etc.
    {
        public string Name;

        public MyMedia(string Name)
        {
            this.Name = Name;
        }
        public abstract string VisitAirport(Airport airport);
        public abstract string VisitCargoPlane(CargoPlane plane);
        public abstract string VisitPassengerPlane(PassengerPlane plane);
    }

    public interface IObjectFactory // interface which define a function creating the objects
    {
        Myobject CreateObject(params string[] data);
    }

    public interface IObjectServerFactory
    {
        Myobject CreateObject(byte[] messageBytes);
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

        public static IObjectServerFactory GetEntityFactory1(string firstElement)
        {
            Dictionary<string, IObjectServerFactory> factoryMap = new Dictionary<string, IObjectServerFactory>
            {
                { "NAI", new AirportServerFactory() },
                { "NCP", new CargoPlaneServerFactory() },
                { "NCA", new CargoServerFactory() },
                { "NCR", new CrewServerFactory() },
                { "NPA", new PassengerServerFactory() },
                { "NPP", new PassengerPlaneServerFactory() },
                { "NFL", new FlightServerFactory() }
            };

            if (factoryMap.TryGetValue(firstElement, out IObjectServerFactory factory))
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
