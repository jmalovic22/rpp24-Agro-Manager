using EntitiesLayer.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repozitoriji
{
    public class RepoVrstaPrikljucka : Repo<Vrsta_prikljucka>
    {
        public RepoVrstaPrikljucka() : base(new AgroManagerModel())
        { }

        public IQueryable<Vrsta_prikljucka> DohvatiVrstePrikljucka()
        {
            var upit = from vrstaPrikljucka in Entiteti
                       select vrstaPrikljucka;
            return upit;
        }

        public Vrsta_prikljucka DohvatiVrstuPrikljuckaPoId(int idVrstePrikljucka)
        {
            var upit = Entiteti.SingleOrDefault(vrstaPrikljucka => vrstaPrikljucka.Vrsta_prikljucka_id == idVrstePrikljucka);
            return upit;
        }

        public override int Azuriraj(Vrsta_prikljucka vrstaPrikljucka, bool spremiPromjene = true)
        {
            throw new NotImplementedException();
        }
    }
}
