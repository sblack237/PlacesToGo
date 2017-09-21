using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Spatial;

namespace PlacesToGo.API.Models
{
    
        public class Places
        {
            public int PlacesId { get; set; }
            public string Name { get; set; }
            public DbGeography Location { get; set; }

            public List<Information> Informations { get; set; }
        }

        public class Information
        {
            public int InformationId { get; set; }
            public string Author { get; set; }
            public string Comments { get; set; }

            public int PlacesId { get; set; }
            public Places Places { get; set; }
        }
    }