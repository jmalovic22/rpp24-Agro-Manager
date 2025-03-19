using EntitiesLayer.Entiteti;
using EntitiesLayer;
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
using EntitiesLayer.Enumeracije;
using PresentationLayer.UserControls;

namespace PresentationLayer.View
{
    /// <summary>
    /// Interaction logic for RadniNalogDodajZaposlenikaView.xaml
    /// </summary>
    public partial class RadniNalogDodajZaposlenikaView : Window
    {
        public RestClient Client = new RestClient();

        public RadniNalogDodajZaposlenikaView()
        {
            InitializeComponent();
        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await DohvatiZaposlenike();
        }

        private void BtnOdustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void BtnSpremi_Click(object sender, RoutedEventArgs e)
        {
            if (CmbZaposlenici.SelectedItem != null)
            {
                StartState();
                var selectedObject = (dynamic)CmbZaposlenici.SelectedItem;
                Korisnik zaposlenik = selectedObject.Original;

                if (!AppKontekst.privremenaListaZaKorisnikeRadnogNaloga.Any(k => k.Korisnik_OIB == zaposlenik.Korisnik_OIB))
                {
                    AppKontekst.privremenaListaZaKorisnikeRadnogNaloga.Add(zaposlenik);
                    DialogResult = true;
                }

                EndState();
                Close();
            }
            else
            {
                GreskaView greska = new GreskaView("Molimo odaberite zaposlenika.");
                greska.ShowDialog();
            }
        }

        private async Task DohvatiZaposlenike()
        {
            try
            {
                var zaposlenici = await Client.GetAsync<List<Korisnik>>($"/api/Korisnik/Poduzece/{AppKontekst.prijavljeniKorisnik.Poduzece_id}");

                if (zaposlenici != null)
                {
                    CmbZaposlenici.ItemsSource = zaposlenici.Select(z => new { Prikaz = $"{z.Ime} {z.Prezime}, {z.Korisnik_OIB}", Original = z }).ToList();
                    CmbZaposlenici.DisplayMemberPath = "Prikaz";
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
