using DataAccessLayer.Repozitoriji;
using EntitiesLayer.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class ServisVrstaPrikljucka
    {
        private RepoVrstaPrikljucka repo;

        public ServisVrstaPrikljucka()
        {
            repo = new RepoVrstaPrikljucka();
        }

        public List<Vrsta_prikljucka> DohvatiVrstePrikljucka()
        {
            return repo.DohvatiVrstePrikljucka().ToList();
        }

        public Vrsta_prikljucka DohvatiVrstuPrikljuckaPoId(int idVrstePrikljucka)
        {
            return repo.DohvatiVrstuPrikljuckaPoId(idVrstePrikljucka);
        }
    }
}
