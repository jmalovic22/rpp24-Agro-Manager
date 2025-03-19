using DataAccessLayer.Repozitoriji;
using EntitiesLayer.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class ServisUzgojnaKultura
    {
        private RepoUzgojnaKultura repo;

        public ServisUzgojnaKultura()
        {
            repo = new RepoUzgojnaKultura();
        }

        public List<Uzgojna_kultura> DohvatiUzgojneKulture()
        {
            return repo.DohvatiUzgojneKulture().ToList();
        }

        public Uzgojna_kultura DohvatiUzgonjuKulturuPremaId(int idUzgojneKulture)
        {
            return repo.DohvatiUzgojnuKulturuPoId(idUzgojneKulture);
        }
    }
}
