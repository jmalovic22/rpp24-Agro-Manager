using DataAccessLayer.Repozitoriji;
using EntitiesLayer.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class ServisRadniNalog
    {
        private RepoRadniNalog repo;

        public ServisRadniNalog()
        {
            repo = new RepoRadniNalog();
        }

        public List<Radni_nalog> DohvatiRadneNalogePolja(int idPolja)
        {
            return repo.DohvatiRadneNalogePolja(idPolja).ToList();
        }

        public List<Radni_nalog> DohvatiRadneNalogePoduzeca(int idPoduzecaa)
        {
            return repo.DohvatiRadneNalogePoduzeca(idPoduzecaa).ToList();
        }

        public Radni_nalog DohvatiRadniNalogPremaId(int idRadniNalog)
        {
            return repo.DohvatiRadniNalogPoId(idRadniNalog);
        }

        public bool DodajRadniNalog(Radni_nalog radniNalogZaDodavnaje)
        {
            bool uspjesnoDodavanjeRadnogNaloga = false;

            if (radniNalogZaDodavnaje == null)
            {
                throw new ArgumentException("Radni nalog za dodavanje nije unesen");
            }

            ProvjeraUpisanihVrijednosti(radniNalogZaDodavnaje);

            int radniNalogDodan = repo.Dodaj(radniNalogZaDodavnaje);
            uspjesnoDodavanjeRadnogNaloga = radniNalogDodan > 0;

            if (uspjesnoDodavanjeRadnogNaloga == false)
            {
                throw new ArgumentException("Dodavanje radnog naloga nije uspjelo");
            }

            return uspjesnoDodavanjeRadnogNaloga;
        }

        private void ProvjeraUpisanihVrijednosti(Radni_nalog radniNalogZaProvjeru, string porukaOGresci = "")
        {
            if (radniNalogZaProvjeru.Datum_kreiranja == null)
            {
                porukaOGresci += "Datum kreiranja nije unesen" + Environment.NewLine;
            }

            if (radniNalogZaProvjeru.Zavrsni_rok == null)

            {
                porukaOGresci += "Završni rok nije unesen" + Environment.NewLine;
            }

            if (radniNalogZaProvjeru.Status == null)
            {
                porukaOGresci += "Status nije unesen" + Environment.NewLine;
            }

            if (radniNalogZaProvjeru.Datum_kreiranja.Date != DateTime.Today)
            {
                porukaOGresci += "Datum kreiranja nije ispravan" + Environment.NewLine;
            }

            if (radniNalogZaProvjeru.Zavrsni_rok < DateTime.Today || radniNalogZaProvjeru.Zavrsni_rok < radniNalogZaProvjeru.Datum_kreiranja)
            {
                porukaOGresci += "Završni rok nije ispravan" + Environment.NewLine;
            }

            if (porukaOGresci != "")
            {
                throw new ArgumentException(porukaOGresci);
            }
        }

        public bool AzurirajRadniNalog(Radni_nalog radniNalogZaAzuriranje)
        {
            bool uspjesnoAzuriranjeRadnogNaloga = false;

            if (radniNalogZaAzuriranje == null)
            {
                throw new ArgumentException("Radni nalog za ažuriranje nije unesen");
            }

            //ProvjeraUpisanihVrijednosti(radniNalogZaDodavnaje);
            // samo provjera da je datum kreiranja danasnji datum
            if (radniNalogZaAzuriranje.Zavrsni_rok < radniNalogZaAzuriranje.Datum_kreiranja)
            {
                throw new ArgumentException("Završni rok nije ispravan");
            }

            int radniNalogAzuriran = repo.Azuriraj(radniNalogZaAzuriranje);
            uspjesnoAzuriranjeRadnogNaloga = radniNalogAzuriran > 0;

            if (uspjesnoAzuriranjeRadnogNaloga == false)
            {
                throw new ArgumentException("Ažuriranje radnog naloga nije uspjelo");
            }

            return uspjesnoAzuriranjeRadnogNaloga;
        }

        public bool ObrisiRadniNalog(int idRadniNalog)
        {
            bool uspjesnoBrisanjeRadnogNaloga = false;

            var radniNalogZaBrisanje = repo.DohvatiRadniNalogPoId(idRadniNalog);

            if (radniNalogZaBrisanje == null)
            {
                throw new ArgumentException("Radni nalog ne postoji");
            }

            int radniNalogIzbrisan = repo.Izbrisi(radniNalogZaBrisanje);
            uspjesnoBrisanjeRadnogNaloga = radniNalogIzbrisan > 0;

            if (uspjesnoBrisanjeRadnogNaloga == false)
            {
                throw new ArgumentException("Brisanje radnog naloga nije uspjelo");
            }

            return uspjesnoBrisanjeRadnogNaloga;
        }
    }
}
