using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Projekt1
{
    internal class Crew : Myobject
    {
        public ulong ID { get; set; }
        public string name { get; set; }
        public ulong age { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public ushort practice { get; set; }
        public string role { get; set; }

        public Crew(ulong ID, string name, ulong age, string phone, string email, ushort practice, string role)
        {
            this.ID = ID;
            this.name = name;
            this.age = age;
            this.phone = phone;
            this.email = email;
            this.practice = practice;
            this.role = role;
        }

    }

    public class CrewFactory : IObjectFactory
    {
        public Myobject CreateObject(params string[] data)
        {
            ulong id = ulong.Parse(data[1]);
            string name = data[2];
            ulong age = ulong.Parse(data[3]);
            string phone = data[4];
            string email = data[5];
            ushort practice = ushort.Parse(data[6]);
            string role = data[7];

            return new Crew(id, name, age, phone, email, practice, role);
        }
    }
}
