using DataAccessLayer.Repozitoriji;
using EntitiesLayer.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BusinessLogicLayer
{
    public class ServisTipKorisnika
    {
        private RepoTipKorisnika repo;

        public ServisTipKorisnika()
        {
            repo = new RepoTipKorisnika();
        }

        public IEnumerable<Tip_korisnika> DohvatiTipKorisnika()
        {
            return repo.DohvatiTipKorisnika();
        }

        public Tip_korisnika DohvatiTipKorisnikaPoId(int idTipKorisnika)
        {
            return repo.DohvatitipKorisnikaPoId(idTipKorisnika);
        }
    }
}
