using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_C_;

namespace Projekt_PO.ProjectObjects
{
    public class Airport : Myobject, IReportable
    {
        public ulong ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public float AMSL { get; set; }
        public string Country { get; set; }

        public Airport(ulong ID, string Name, string Code, float Longitude, float Latitude, float AMSL, string Country)
        {
            this.ID = ID;
            this.Name = Name;
            this.Code = Code;
            this.Latitude = Latitude;
            this.Longitude = Longitude;
            this.AMSL = AMSL;
            this.Country = Country;
        }

        // Accept function to create a suitable message
        public string Accept(IMediaVisitor visitor)
        {
            return visitor.VisitAirport(this);
        }

    }
}
