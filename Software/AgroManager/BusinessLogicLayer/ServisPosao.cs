using DataAccessLayer.Repozitoriji;
using EntitiesLayer.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class ServisPosao
    {
        private RepoPosao repo;

        public ServisPosao()
        {
            repo = new RepoPosao();
        }

        public List<Posao> DohvatiPoslove()
        {
            return repo.DohvatiPoslove().ToList();
        }

        public Posao DohvatiPosao(int idPosao)
        {
            return repo.DohvatiPosaoPoId(idPosao);
        }

        public bool DodajPosao(Posao posaoZaDodavanje)
        {
            bool uspjesnoDodavanjePosla = false;

            if (posaoZaDodavanje == null)
            {
                throw new ArgumentException("Posao za dodavanje nije pronađeno");
            }

            PosaoIspravniPodaciDodavanja(posaoZaDodavanje);

            int voziloDodano = repo.Dodaj(posaoZaDodavanje);
            uspjesnoDodavanjePosla = voziloDodano > 0;

            if (uspjesnoDodavanjePosla == false)
            {
                throw new ArgumentException("Dodavanje posla nije uspjelo");
            }

            return uspjesnoDodavanjePosla;
        }

        public bool AzurirajPosao(Posao posaoZaAzuriranje)
        {
            bool uspjesnoAzuriranjePosla = false;

            if (posaoZaAzuriranje == null)
            {
                throw new ArgumentException("Posao za ažuriranje nije pronađeno");
            }

            PosaoIspravniPodaciAzuriraj(posaoZaAzuriranje);

            int voziloAzurirano = repo.Azuriraj(posaoZaAzuriranje);
            uspjesnoAzuriranjePosla = voziloAzurirano > 0;

            if (uspjesnoAzuriranjePosla == false)
            {
                throw new ArgumentException("Ažuriranje posla nije uspjelo");
            }

            return uspjesnoAzuriranjePosla;
        }

        public bool IzbrisiPosao(int idPosla)
        {
            bool uspjesnoBrisanjePosla = false;

            var posaoZaBrisanje = repo.DohvatiPosaoPoId(idPosla);

            if (posaoZaBrisanje == null)
            {
                throw new ArgumentException("Posao za brisanje nije pronađeno");
            }

            int posaoIzbrisano = repo.Izbrisi(posaoZaBrisanje);
            uspjesnoBrisanjePosla = posaoIzbrisano > 0;

            if (uspjesnoBrisanjePosla == false)
            {
                throw new ArgumentException("Brisanje posao nije uspjelo");
            }

            return uspjesnoBrisanjePosla;
        }

        private void PosaoIspravniPodaciDodavanja(Posao posaoZaProvjeru, string porukaOGresci = "")
        { 
            if (string.IsNullOrWhiteSpace(posaoZaProvjeru.Naziv))
            {
                porukaOGresci += "Naziv posla ne smije biti prazna" + Environment.NewLine;
            }

            var posaoNaziv = DohvatiPoslove().Exists(posao =>
                string.Equals(posao.Naziv, posaoZaProvjeru.Naziv, StringComparison.OrdinalIgnoreCase));

            if (posaoNaziv)
            {
                porukaOGresci += "Već postoji posao s tim nazivom" + Environment.NewLine;
            }

            if (porukaOGresci != "")
            {
                throw new ArgumentException(porukaOGresci);
            }
        }

        private void PosaoIspravniPodaciAzuriraj(Posao posaoZaProvjeru, string porukaOGresci = "")
        {

            if (string.IsNullOrWhiteSpace(posaoZaProvjeru.Naziv))
            {
                porukaOGresci += "Naziv posla ne smije biti prazna" + Environment.NewLine;
            }
            else
            {
                var pronadeniPosao = DohvatiPoslove().Find(posao =>
                string.Equals(posao.Naziv, posaoZaProvjeru.Naziv, StringComparison.OrdinalIgnoreCase));

                if (pronadeniPosao != null && pronadeniPosao.Posao_id != posaoZaProvjeru.Posao_id)
                {
                    porukaOGresci += "Već postoji posao s tim nazivom" + Environment.NewLine;
                }
            }

            if (porukaOGresci != "")
            {
                throw new ArgumentException(porukaOGresci);
            }
        }
    }
}
