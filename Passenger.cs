using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt1
{
    internal class Passenger : Myobject
    {
        public ulong ID { get; set; }
        public string name { get; set; }
        public ulong age { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string Class { get; set; }
        public ulong miles { get; set; }

        public Passenger(ulong ID, string name, ulong age, string phone, string email, string Class, ulong miles)
        {
            this.ID = ID;
            this.name = name;
            this.age = age;
            this.phone = phone;
            this.email = email;
            this.Class = Class;
            this.miles = miles;
        }

    }
    public class PassengerFactory : IObjectFactory
    {
        public Myobject CreateObject(params string[] data)
        {
            ulong id = ulong.Parse(data[1]);
            string name = data[2];
            ulong age = ulong.Parse(data[3]);
            string phone = data[4];
            string email = data[5];
            string Class = data[6];
            ulong miles = ulong.Parse(data[7]);

            return new Passenger(id, name, age, phone, email, Class, miles);
        }
    }
}
