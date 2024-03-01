﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt_PO.ProjectObjects;

namespace Projekt_PO.Factory
{
    public class CargoPlaneFactory : IObjectFactory
    {
        public Myobject CreateObject(params string[] data)
        {
            ulong id = ulong.Parse(data[1]);
            string Serial = data[2];
            string Country = data[3];
            string Model = data[4];
            float MaxLoad = float.Parse(data[5], System.Globalization.CultureInfo.InvariantCulture);

            return new CargoPlane(id, Serial, Country, Model, MaxLoad);
        }
    }
}