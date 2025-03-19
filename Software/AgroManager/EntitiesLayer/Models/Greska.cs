using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntitiesLayer.Models
{
    public class Greska
    {
        public string Status { get; set; }
        public string Poruka { get; set; }

        public string Message { get; set; }
        public string MessageDetail { get; set; }
        public UnutarnjaGreska InnerException { get; set; }
    }
}