using EntitiesLayer.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repozitoriji
{
    public class RepoVrstaStoke : Repo<Vrsta_stoke>
    {
        public RepoVrstaStoke() : base(new AgroManagerModel())
        {}

        public IQueryable<Vrsta_stoke> DohvatiVrsteStoke()
        {
            var upit = from vrstaStoke in Entiteti
                       select vrstaStoke;
            return upit;
        }

        public Vrsta_stoke DohvatiVrsteStokePoId(int idVrstaStoke)
        {
            var upit = Entiteti.SingleOrDefault(vrstaStoke => vrstaStoke.Vrsta_stoke_id == idVrstaStoke);
            return upit;
        }

        public override int Azuriraj(Vrsta_stoke entitet, bool spremiPromjene = true)
        {
            throw new NotImplementedException();
        }

    }
}
