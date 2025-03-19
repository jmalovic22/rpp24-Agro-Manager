using DataAccessLayer.Repozitoriji;
using EntitiesLayer.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class ServisVrstaStokeFarma
    {
        private RepoVrstaStokeFarma repo;

        public ServisVrstaStokeFarma()
        {
            repo = new RepoVrstaStokeFarma();
        }

        public async Task<List<Vrsta_stoke_farma>> DohvatiVrsteStoke()
        {
            return await repo.DohvatiSveAsync();
        }

        public bool DodajVrstuStokeFarmi(Vrsta_stoke_farma vrstaStokeZaDodavanjeFarmi)
        {
            bool uspjesnoDodavanjeVrsteStokeFarma = false;

            if (vrstaStokeZaDodavanjeFarmi == null)
            {
                throw new ArgumentException("Vrsta stoke farme za dodavanje nije unesena");
            }

            ProvjeraUnesenihPodatakaDodavanje(vrstaStokeZaDodavanjeFarmi);

            int vrstaStokeFarmaDodana = repo.Dodaj(vrstaStokeZaDodavanjeFarmi);
            uspjesnoDodavanjeVrsteStokeFarma = vrstaStokeFarmaDodana > 0;

            if (uspjesnoDodavanjeVrsteStokeFarma == false)
            {
                throw new ArgumentException("Neuspjesno dodavanje vrste stoke farmi");
            }

            return uspjesnoDodavanjeVrsteStokeFarma;
        }

        //potencijalno nepotrebna metoda s obzirom da se na frontendu ne moze brisati vrsta stoke koja pripada nekoj farmi
        public bool IzbrisiVrstuStokeNaFarmi(int idVrstaStokeFarma)
        {
            bool uspjesnoBrisanjeVrsteStokeFarma = false;

            var vrstaStokeNaFarmiZaBrisanje = repo.DohvatiSve().SingleOrDefault(v => v.Vrsta_stoke_farma_id == idVrstaStokeFarma);

            if (vrstaStokeNaFarmiZaBrisanje == null)
            {
                throw new ArgumentException("Vrsta stoke na farmi nije pronađena");
            }

            int vrstaStokeFarmaIzbrisana = repo.Izbrisi(vrstaStokeNaFarmiZaBrisanje);
            uspjesnoBrisanjeVrsteStokeFarma = vrstaStokeFarmaIzbrisana > 0;

            if (uspjesnoBrisanjeVrsteStokeFarma == false)
            {
                throw new ArgumentException("Brisanje vrste stoke na farmi nije uspjelo");
            }

            return uspjesnoBrisanjeVrsteStokeFarma;
        }

        public async Task<List<Vrsta_stoke_farma>> DohvatiSveVrsteStokePremaIdFarmeAsync(int idFarme)
        {
            var sveVrsteStokeNaFarmi = await repo.DohvatiSveVrsteStokeFarmeAsync(idFarme);
            return sveVrsteStokeNaFarmi;
        }

        public async Task<List<Vrsta_stoke>> DohvatiSveVrsteStokeKojeNisuNaFarmi(int idFarme)
        {
            var sveVrsteStokeNaFarmi = await repo.DohvatiSveVrsteStokeFarmeAsync(idFarme);
            List<int> listaStokeId = sveVrsteStokeNaFarmi.Select(v => v.Vrsta_stoke_id).ToList();
            var sveVrsteStoke = new RepoVrstaStoke().DohvatiSve().Where(v => 
                                !listaStokeId.Contains(v.Vrsta_stoke_id)).ToList();
            return sveVrsteStoke;
        }

        private void ProvjeraUnesenihPodatakaDodavanje(Vrsta_stoke_farma vrstaStokeFarmaZaProvjeru, string porukaOGresci = "")
        {
            var vrstaStokeFarmaId = repo.DohvatiSve().Where(vrsta => 
                                    vrsta.Vrsta_stoke_id == vrstaStokeFarmaZaProvjeru.Vrsta_stoke_id
                                    && vrsta.Farma_id == vrstaStokeFarmaZaProvjeru.Farma_id);

            Vrsta_stoke_farma vrstaStokeFarma = null;

            if (vrstaStokeFarmaId.Count() > 0)
            {
                vrstaStokeFarma = vrstaStokeFarmaId.First();
            }

            if (vrstaStokeFarma != null)
            {
                porukaOGresci += $"U bazi već postoji dodana vrsta stoke {vrstaStokeFarma.Vrsta_stoke.Naziv} " +
                    $"na farmu. Promjenu morate odraditi kroz evidenciju stoke na farmi. "
                    + Environment.NewLine;
            };

            ProvjeraUnesenihPodataka(vrstaStokeFarmaZaProvjeru, porukaOGresci);
        }
        private void ProvjeraUnesenihPodataka(Vrsta_stoke_farma vrstaStokeFarmaZaProvjeru, string porukaOGresci = "")
        {

            if (vrstaStokeFarmaZaProvjeru.Kolicina_stoke < 0)
            {
                porukaOGresci += "Kolicina stoke ne smje biti manja od 0" + Environment.NewLine;
            }

            if (porukaOGresci != "")
            {
                throw new ArgumentException(porukaOGresci);
            }

        }

    }
}
