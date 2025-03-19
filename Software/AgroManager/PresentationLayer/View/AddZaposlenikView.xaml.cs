using EntitiesLayer;
using EntitiesLayer.Entiteti;
using EntitiesLayer.Enumeracije;
using EntitiesLayer.Utilities;
using Newtonsoft.Json;
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
using System.Windows.Shapes;

namespace PresentationLayer.View
{
    /// <summary>
    /// Interaction logic for AddZaposlenikView.xaml
    /// </summary>
    public partial class AddZaposlenikView : Window
    {
        RestClient Client = new RestClient();

        public AddZaposlenikView()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await PopuniComboBoxove();
        }

        private void BtnOdustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void BtnSpremi_Click(object sender, RoutedEventArgs e)
        {
            StartState();
            var uspjesnoDodavanje = await Dodaj();
            EndState();
            Close();
        }

        private void BtnDodajPolozenuKategoriju_Click(object sender, RoutedEventArgs e)
        {
            ZaposlenikDodajPolozenuKategorijuView zaposlenikDodajPolozenuKategorijuView = new ZaposlenikDodajPolozenuKategorijuView();

            if (zaposlenikDodajPolozenuKategorijuView.ShowDialog() == true)
            {
                EnumPolozeneKategorije? novaKategorija = zaposlenikDodajPolozenuKategorijuView.OdabranaKategorija;

                if (novaKategorija.HasValue)
                {
                    string trenutnoDodane = TxtPolozeneKategorije.Text;

                    if (!trenutnoDodane.Split(',').Select(x => x.Trim()).Contains(novaKategorija.ToString()))
                    {
                        TxtPolozeneKategorije.Text += (trenutnoDodane.Length > 0 ? ", " : "") + novaKategorija.ToString();
                    }
                    else
                    {
                        GreskaView greska = new GreskaView("Kategorija je već dodana");
                        greska.ShowDialog();
                    }
                }
            }
        }

        private async Task<Korisnik> Dodaj()
        {
            var tipKorisnika = CmbTipKorisnika.SelectedItem as Tip_korisnika;

            if (string.IsNullOrEmpty(TxtOibInput.txtInput.Text)
                || string.IsNullOrEmpty(TxtImeInput.txtInput.Text)
                || string.IsNullOrEmpty(TxtPrezimeInput.txtInput.Text)
                || string.IsNullOrEmpty(TxtKorisnickoImeInput.txtInput.Text)
                || string.IsNullOrEmpty(TxtEmailInput.txtInput.Text)
                || string.IsNullOrEmpty(TxtLozinkaInput.txtInput.Text)
                || string.IsNullOrEmpty(CmbTipKorisnika.Text)
                || tipKorisnika == null)
            {
                GreskaView greska = new GreskaView("Molimo ispunite sva polja.");
                greska.ShowDialog();
                return null;
            }

            try
            {

                var korisnik = new Korisnik
                {
                    Korisnik_OIB = TxtOibInput.txtInput.Text,
                    Ime = TxtImeInput.txtInput.Text,
                    Prezime = TxtPrezimeInput.txtInput.Text,
                    Korisnicko_ime = TxtKorisnickoImeInput.txtInput.Text,
                    Lozinka = TxtLozinkaInput.txtInput.Text,
                    Email = TxtEmailInput.txtInput.Text,
                    Polozene_kategorije = TxtPolozeneKategorije.Text,
                    Poduzece_id = AppKontekst.prijavljeniKorisnik.Poduzece_id,
                    Tip_korisnika_id = tipKorisnika.Tip_korisnika_id,
                    Secret = TOTP.EncodeBase32(TOTP.GenerateKey(32))
                };

                string podaci = JsonConvert.SerializeObject(korisnik);
                var odgovor = await Client.PostAsync<bool>("/api/Korisnik", podaci);

                if (odgovor == true)
                {
                    return korisnik;
                }

                return null;
            }
            catch (Exception ex)
            {
                GreskaView greska = new GreskaView($"Greška pri spremanju: {ex.Message}");
                greska.ShowDialog();
                return null;
            }
        }

        private async Task PopuniComboBoxove()
        {
            try
            {
                var tipKorisnika = await Client.GetAsync<List<Tip_korisnika>>($"/api/TipKorisnika");

                if (tipKorisnika != null)
                {
                    CmbTipKorisnika.ItemsSource = tipKorisnika;
                    CmbTipKorisnika.DisplayMemberPath = "Pozicija";
                }
                else
                {
                    GreskaView greska = new GreskaView($"Greška prilikom učitavanja podataka.");
                    greska.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                GreskaView greska = new GreskaView($"Greška prilikom učitavanja podataka: {ex.Message}");
                greska.ShowDialog();
            }
        }

        private void StartState()
        {
            BtnOdustani.IsEnabled = false;
            BtnOdustani.Opacity = 0.5;
            BtnSpremi.IsEnabled = false;
            BtnSpremi.Opacity = 0.5;

        }
        private void EndState()
        {
            BtnOdustani.IsEnabled = true;
            BtnOdustani.Opacity = 1;
            BtnSpremi.IsEnabled = true;
            BtnSpremi.Opacity = 1;
        }
    }
}
