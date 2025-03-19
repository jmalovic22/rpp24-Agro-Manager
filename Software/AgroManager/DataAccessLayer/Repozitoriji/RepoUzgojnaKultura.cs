using EntitiesLayer.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repozitoriji
{
    public class RepoUzgojnaKultura : Repo<Uzgojna_kultura>
    {
        public RepoUzgojnaKultura() : base(new AgroManagerModel())
        { }

        public IQueryable<Uzgojna_kultura> DohvatiUzgojneKulture()
        {
            var upit = from uzgojnaKultura in Entiteti
                       select uzgojnaKultura;
            return upit;
        }

        public Uzgojna_kultura DohvatiUzgojnuKulturuPoId(int idUzgojneKulture)
        {
            var upit = Entiteti.SingleOrDefault(uzgojnaKultura => uzgojnaKultura.Uzgojna_kultura_id == idUzgojneKulture);
            return upit;
        }

        public override int Azuriraj(Uzgojna_kultura uzgojnaKultura, bool spremiPromjene = true)
        {
            throw new NotImplementedException();
        }
    }
}
