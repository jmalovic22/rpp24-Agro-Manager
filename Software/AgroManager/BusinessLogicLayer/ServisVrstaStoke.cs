using DataAccessLayer.Repozitoriji;
using EntitiesLayer.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class ServisVrstaStoke
    {
        private RepoVrstaStoke repo;

        public ServisVrstaStoke()
        {
            repo = new RepoVrstaStoke();
        }

        public List<Vrsta_stoke> DohvatiVrsteStoke()
        {
            return repo.DohvatiVrsteStoke().ToList();
        }

        public Vrsta_stoke DohvatiVrsteStokePoId(int idVrsteStoke)
        {
            return repo.DohvatiVrsteStokePoId(idVrsteStoke);
        }
    }
}
