using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Entiteti
{
    public partial class Prikljucak
    {
        public override string ToString()
        {
            return Vrsta_prikljucka.Naziv + " " + Marka;
        } 
        //public string FullName => {$"{Vrsta_prikljucka.Naziv}  {Marka}"};
    }
}
