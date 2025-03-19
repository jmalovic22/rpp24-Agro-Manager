using EntitiesLayer;
using EntitiesLayer.Entiteti;
using EntitiesLayer.Models;
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
    /// Interaction logic for AddEvidencijeStokeFarmaView.xaml
    /// </summary>
    public partial class AddEvidencijeStokeFarmaView : Window
    {
        RestClient Client = new RestClient();

        public AddEvidencijeStokeFarmaView()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PopuniComboBoxove();
        }

        private void BtnOdustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void BtnSpremi_Click(object sender, RoutedEventArgs e)
        {
            StartState();
            var uspjesnoDodavanje = await Dodaj();
            if (uspjesnoDodavanje)
            {
                Close();
            }
            EndState();
        }

        private async Task<bool> Dodaj()
        {
            if (string.IsNullOrEmpty(CmbVrstaStokeFarma.Text)
                || string.IsNullOrEmpty(CmbFarma.Text)
                || string.IsNullOrEmpty(TxtBiljeskeInput.txtInput.Text)
                || string.IsNullOrEmpty(TxtKolicinaInput.txtInput.Text)
                || string.IsNullOrEmpty(DpDatumIzmjeneInput.Text))
            {
                GreskaView greska = new GreskaView("Molimo ispunite sva polja.");
                greska.ShowDialog();
                return false;
            }

            try
            {
                //var vrstaStokeFarma = CmbVrstaStokeFarma.SelectedItem as Vrsta_stoke_farma;

                if (!DateTime.TryParse(DpDatumIzmjeneInput.Text, out DateTime datumPromjene))
                {
                    GreskaView greska = new GreskaView("Neispravan format datuma.");
                    greska.ShowDialog();
                    return false;
                }

                var evidencija_stoke_farma = new Evidencija_stoke_farma
                {
                    Datum_promjene = datumPromjene,
                    Napomena = TxtBiljeskeInput.txtInput.Text,
                    Kolicina_promjene = int.Parse(TxtKolicinaInput.txtInput.Text),
                    Vrsta_stoke_farma_id = (CmbVrstaStokeFarma.SelectedItem as Vrsta_stoke_farma)?.Vrsta_stoke_farma_id ?? 0,
                };

                string podaci = JsonConvert.SerializeObject(evidencija_stoke_farma);
                var odgovor = await Client.PostAsync<bool>("/api/EvidencijaStokeFarma", podaci);
                return odgovor;
            }
            catch (Exception ex)
            {
                GreskaView greska = new GreskaView($"Greška pri spremanju: {ex.Message}");
                greska.ShowDialog();
                return false;
            }
        }

        private async void PopuniComboBoxove()
        {
            try
            {
                var farme = await Client.GetAsync<List<Farma>>($"/api/Farma/Poduzece/" + AppKontekst.prijavljeniKorisnik.Poduzece_id);

                CmbFarma.ItemsSource = farme;
                CmbFarma.DisplayMemberPath = "Lokacija";
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

        private void CmbFarma_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
        }

        private async void PopuniComboStokaFarme(int farmaId)
        {
            try
            {
                List<Vrsta_stoke_farma> vrsteStokeFarma = await Client.GetAsync<List<Vrsta_stoke_farma>>($"/api/VrstaStokeFarma/Farma/{farmaId}");
                if (vrsteStokeFarma != null)
                {
                    CmbVrstaStokeFarma.ItemsSource = vrsteStokeFarma;
                }
                else
                {
                    GreskaView greska = new GreskaView("Greška prilikom učitavanja podataka.");
                    greska.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                GreskaView greska = new GreskaView($"Greška prilikom učitavanja podataka: {ex.Message}");
                greska.ShowDialog();

            }
        }

        private void CmbFarma_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var farma = CmbFarma.SelectedItem as Farma;
            if (farma != null)
            {
                PopuniComboStokaFarme(farma.Farma_id);
            }
            else
            {
                CmbVrstaStokeFarma.ItemsSource = null;
            }
        }
    }
}
