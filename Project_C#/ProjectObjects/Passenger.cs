using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_PO.ProjectObjects
{
    internal class Passenger : Human
    {
        public string Name { get; set; }
        public ulong Age { get; set; }
        public string Class { get; set; }
        public ulong Miles { get; set; }

        public Passenger(ulong ID, string Name, ulong Age, string Phone, string Email, string Class, ulong Miles)
        {
            this.ID = ID;
            this.Name = Name;
            this.Age = Age;
            this.Phone = Phone;
            this.Email = Email;
            this.Class = Class;
            this.Miles = Miles;
        }

    }
}
