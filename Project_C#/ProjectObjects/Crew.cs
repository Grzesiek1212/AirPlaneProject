using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_PO.ProjectObjects
{
    internal class Crew : Myobject
    {
        public ulong ID { get; set; }
        public string Name { get; set; }
        public ulong Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public ushort Practice { get; set; }
        public string Role { get; set; }

        public Crew(ulong ID, string Name, ulong Age, string Phone, string Email, ushort Practice, string Role)
        {
            this.ID = ID;
            this.Name = Name;
            this.Age = Age;
            this.Phone = Phone;
            this.Email = Email;
            this.Practice = Practice;
            this.Role = Role;
        }

    }


}
