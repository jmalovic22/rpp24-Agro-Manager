using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntitiesLayer;
using RestAPI.Models;

namespace RestAPI.Utilities
{
    public class Zajednicko
    {

        public static Greska DohvatiOpisGreske(Exception ex)
        {
            var greska = new Greska() { Status = "Greška " + ex.Source, Poruka = ex.Message };
            // dodaj stvarni uzrok greške ako je nastla u EF ili LINQ
            if (ex.InnerException != null)
            {
                greska.Status = "Sistemska greška " + ex.Source;
                greska.Poruka = ex.InnerException.Message;
                if (ex.InnerException.InnerException != null)
                {
                    greska.Poruka = ex.InnerException.InnerException.Message;
                }
            }
            return greska;
        }
    }
}