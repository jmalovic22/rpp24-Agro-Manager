using EntitiesLayer;
using EntitiesLayer.Entiteti;
using EntitiesLayer.Enumeracije;
using Newtonsoft.Json;
using PresentationLayer.UserControls;
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
    /// Interaction logic for EditZaposlenikView.xaml
    /// </summary>
    public partial class EditZaposlenikView : Window
    {
        public Korisnik Korisnik { get; set; }
        private RestClient Client = new RestClient();


        public EditZaposlenikView(Korisnik korisnik)
        {
            InitializeComponent();
            Korisnik = korisnik;
            TxtImeInput.txtInput.Text = korisnik.Ime;
            TxtPrezimeInput.txtInput.Text = korisnik.Prezime;
            TxtEmailInput.txtInput.Text = korisnik.Email;
            TxtPolozeneKategorije.Text = korisnik.Polozene_kategorije;
        }

        private void BtnOdustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void BtnSpremi_Click(object sender, RoutedEventArgs e)
        {
            StartState();
            var uspjesno = await Azuriraj();
            if (uspjesno)
            {
                Close();
                EndState();
                new UcZaposlenik().UserControl_Loaded(sender, e);
            }
            EndState();
        }

        private async Task<bool> Azuriraj()
        {
            var tipKorisnika = CmbTipKorisnika.SelectedItem as Tip_korisnika;

            if (string.IsNullOrEmpty(TxtImeInput.txtInput.Text)
                || string.IsNullOrEmpty(TxtPrezimeInput.txtInput.Text)            
                || string.IsNullOrEmpty(TxtEmailInput.txtInput.Text)                
                || string.IsNullOrEmpty(CmbTipKorisnika.Text)
                || tipKorisnika == null)
            {
                GreskaView greska = new GreskaView("Molimo ispunite sva polja.");
                greska.ShowDialog();
                return false;
            }

            try
            {
                var korisnik = new Korisnik
                {
                    Korisnik_OIB = Korisnik.Korisnik_OIB,
                    Ime = TxtImeInput.txtInput.Text,
                    Prezime = TxtPrezimeInput.txtInput.Text,
                    Korisnicko_ime = Korisnik.Korisnicko_ime,
                    Lozinka = Korisnik.Lozinka,
                    Email = TxtEmailInput.txtInput.Text,
                    Polozene_kategorije = TxtPolozeneKategorije.Text,
                    Poduzece_id = Korisnik.Poduzece_id,
                    Tip_korisnika_id = tipKorisnika.Tip_korisnika_id
                };

                string podaci = JsonConvert.SerializeObject(korisnik);
                var odgovor = await Client.PutAsync<bool>("/api/Korisnik", podaci);

                if (odgovor == true)
                {
                    Korisnik = korisnik;
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                GreskaView greska = new GreskaView($"Greška pri ažuriranju: {ex.Message}");
                greska.ShowDialog();
                return false;
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

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await PopuniComboBoxove();
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
                    CmbTipKorisnika.SelectedItem = Korisnik.Tip_korisnika_id;
                    CmbTipKorisnika.Text = Korisnik.Tip_korisnika.Pozicija;
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
    }
}
