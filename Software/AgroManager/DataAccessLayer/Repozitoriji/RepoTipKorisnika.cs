using EntitiesLayer.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repozitoriji
{
    public class RepoTipKorisnika : Repo<Tip_korisnika>
    {
        public RepoTipKorisnika() : base(new AgroManagerModel())
        { }

        public IQueryable<Tip_korisnika> DohvatiTipKorisnika()
        {
            var upit = from tipKorisnika in Entiteti
                       select tipKorisnika;
            return upit;
        }

        public Tip_korisnika DohvatitipKorisnikaPoId(int idTipKorisnika)
        {
            var upit = Entiteti.SingleOrDefault(tipKorisnika => tipKorisnika.Tip_korisnika_id == idTipKorisnika);
            return upit;
        }

        public override int Azuriraj(Tip_korisnika tipKorisnika, bool spremiPromjene = true)
        {
            throw new NotImplementedException();
        }
    }
}
