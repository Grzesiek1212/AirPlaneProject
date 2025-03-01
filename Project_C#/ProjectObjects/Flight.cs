﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_PO.ProjectObjects
{
    public class Flight : Myobject
    {
        public Airport Origin { get; set; }
        public Airport Target { get; set; }
        public string TakeoffTime { get; set; }
        public string LandingTime { get; set; }
        public float LongitudeStart { get; set; }
        public float LatitudeStart { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public float AMSL { get; set; }
        public Plane Plane { get; set; }
        public List<ulong> Crew_ids { get; set; }
        public List<ulong> Load_ids { get; set; }

        public Flight(ulong ID, Airport Origin, Airport Target, string TakeoffTime, string LandingTime, float Longitude, float Latitude, float Longitude1, float Latitude1, float AMSL, Plane Plane, List<ulong> Crew_ids, List<ulong> Load_ids)
        {
            this.ID = ID;
            this.Origin = Origin;
            this.Target = Target;
            this.TakeoffTime = TakeoffTime;
            this.LandingTime = LandingTime;
            this.Longitude = Longitude;
            this.Latitude = Latitude;
            LongitudeStart = Longitude1;
            LatitudeStart = Latitude1;
            this.AMSL = AMSL;
            this.Plane = Plane;
            this.Crew_ids = Crew_ids;
            this.Load_ids = Load_ids;
        }

    }
}