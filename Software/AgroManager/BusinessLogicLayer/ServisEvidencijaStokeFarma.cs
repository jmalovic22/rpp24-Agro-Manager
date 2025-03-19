using DataAccessLayer.Repozitoriji;
using EntitiesLayer.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class ServisEvidencijaStokeFarma
    {
        private RepoEvidencijaStokeFarma repo;

        public ServisEvidencijaStokeFarma()
        {
            repo = new RepoEvidencijaStokeFarma();
        }

        public async Task<bool> DodajEvidencijuStokeNaFarmi(Evidencija_stoke_farma evidencijaZaDodavanje)
        {
            bool uspjesnoDodavanjeEvidencije = false;

            if (evidencijaZaDodavanje == null)
            {
                throw new ArgumentException("Evidencija stoke za dodavanje nije unesena");
            }

            await ProvjeraUnesenihPodatakaEvidencije(evidencijaZaDodavanje);

            int evidencijaDodana = repo.Dodaj(evidencijaZaDodavanje);
            uspjesnoDodavanjeEvidencije = evidencijaDodana > 0;

            if (uspjesnoDodavanjeEvidencije == false)
            {
                throw new ArgumentException("Dodavanje evidencije stoke nije uspjelo");
            }

            return uspjesnoDodavanjeEvidencije;
        }

        public async Task<List<Evidencija_stoke_farma>> DohvatiSveEvidencijeStokeNaFarmiPoduzeca(int idPoduzeca)
        {

            var sveEvidencijeStoke = await repo.DohvatiSveAsync();
            var evidencijeStokeNaFarmiPoduzeca = sveEvidencijeStoke
                .Where(evidencija => evidencija.Vrsta_stoke_farma.Farma.Poduzece_id == idPoduzeca);

            return evidencijeStokeNaFarmiPoduzeca.ToList();
        }

        public async Task<List<Evidencija_stoke_farma>> DohvatiSveEvidencijeStokeNaFarmi(int idFarme)
        {

            var sveEvidencijeStoke = await repo.DohvatiSveAsync();
            var evidencijeStokeNaFarmiPoduzeca = sveEvidencijeStoke
                .Where(evidencija => evidencija.Vrsta_stoke_farma.Farma_id == idFarme);

            return evidencijeStokeNaFarmiPoduzeca.ToList();
        }

        private async Task ProvjeraUnesenihPodatakaEvidencije(Evidencija_stoke_farma evidencijaZaProvjeru, string porukaOGresci = "")
        {
            using (var repo = new RepoVrstaStokeFarma())
            {
                var sveVrsteStoke = await repo.DohvatiSveAsync();
                var vrstaStokeFarma = sveVrsteStoke.SingleOrDefault(vrstaStokeNaFarmi =>
                    evidencijaZaProvjeru.Vrsta_stoke_farma_id == vrstaStokeNaFarmi.Vrsta_stoke_farma_id);

                if (vrstaStokeFarma == null)
                {
                    throw new ArgumentException("Vrsta stoke za evidenciju nije pronađena");
                }

                var trenutnaKolicinaVrsteStokeFarma = vrstaStokeFarma.Kolicina_stoke;

                if (evidencijaZaProvjeru.Kolicina_promjene < 0
                    && Math.Abs(evidencijaZaProvjeru.Kolicina_promjene) > trenutnaKolicinaVrsteStokeFarma)
                {
                    porukaOGresci += "Unesena je nedostupna kolicina za tu vrstu stoke na farmi, " +
                        $"trenutno stanje na farmi za tu vrstu stoke je {trenutnaKolicinaVrsteStokeFarma}" + Environment.NewLine;
                }

                if (evidencijaZaProvjeru.Kolicina_promjene == 0)
                {
                    porukaOGresci += "Unesena kolicina promjene ne moze biti 0" + Environment.NewLine;
                }

                if (evidencijaZaProvjeru.Datum_promjene == null)
                {
                    porukaOGresci += "Niste unijeli datum promjene" + Environment.NewLine;
                }

                if (evidencijaZaProvjeru.Datum_promjene != DateTime.Today)
                {
                    porukaOGresci += "Datum evidencije mora biti jednak danasnjem datumu" + Environment.NewLine;
                }

                if (evidencijaZaProvjeru.Napomena.Length > 1000)
                {
                    porukaOGresci += "Max. dužina napomene je 1000 znakova" + Environment.NewLine;
                }

                if (porukaOGresci != "")
                {
                    throw new ArgumentException(porukaOGresci);
                }
            }

        }

    }
}
