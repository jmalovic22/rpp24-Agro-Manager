using EntitiesLayer;
using EntitiesLayer.Entiteti;
using EntitiesLayer.Models;
using PresentationLayer.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PresentationLayer.UserControls
{
    /// <summary>
    /// Interaction logic for UcHome.xaml
    /// </summary>
    public partial class UcHome : UserControl
    {
        private RestClient Client = new RestClient();

        public UcHome()
        {
            InitializeComponent();
        }
        
        private async Task<ResursiPoduzeca> DohvatiPodatke()
        {
            try
            {
                int idPoduzece = AppKontekst.prijavljeniKorisnik.Poduzece_id;
                var resursi = await Client.GetAsync<ResursiPoduzeca>($"/api/Resursi/Poduzece/{idPoduzece}");
                if (resursi == null) return new ResursiPoduzeca();
                return resursi;
            }
            catch (Exception ex)
            {
                GreskaView greska = new GreskaView($"Greška: {ex.Message}");
                greska.ShowDialog();
                return new ResursiPoduzeca();
            }
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var resursi = await DohvatiPodatke();

            LblImePrezime.Content = AppKontekst.prijavljeniKorisnik.Ime + " " + AppKontekst.prijavljeniKorisnik.Prezime;
            LblOIB.Content += AppKontekst.prijavljeniKorisnik.Korisnik_OIB;
            LblEmail.Content += AppKontekst.prijavljeniKorisnik.Email;
            LblKategorije.Content += AppKontekst.prijavljeniKorisnik.Polozene_kategorije;

            LblNazivPoduzeca.Content = AppKontekst.prijavljeniKorisnik.Poduzece.Naziv;
            LblVlasnik.Content += AppKontekst.prijavljeniKorisnik.Poduzece.Vlasnik;
            LblTelefon.Content += AppKontekst.prijavljeniKorisnik.Poduzece.Telefon;
            LblEmailPoduzeca.Content += AppKontekst.prijavljeniKorisnik.Poduzece.Email;

            LblFarme.Content = resursi.brojFarmi.ToString();
            LblZaposlenici.Content = resursi.brojZaposlenih.ToString();
            LblSilos.Content = resursi.brojSilosa.ToString();
            LblPolje.Content = resursi.brojPolja.ToString();
            LblVozila.Content = resursi.brojVozila.ToString();
            LblPrikljucak.Content = resursi.brojPrikljucaka.ToString();
        }
    }
}
