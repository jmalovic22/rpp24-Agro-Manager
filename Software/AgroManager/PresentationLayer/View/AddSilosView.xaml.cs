using EntitiesLayer;
using EntitiesLayer.Entiteti;
using Newtonsoft.Json;
using PresentationLayer.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    /// Interaction logic for AddSilosView.xaml
    /// </summary>
    public partial class AddSilosView : Window
    {
        private RestClient Client = new RestClient();

        public AddSilosView()
        {
            InitializeComponent();
        }

        private async void BtnSpremi_Click(object sender, RoutedEventArgs e)
        {
            StartState();
            var uspjesnoDodavanje = await Dodaj();
            EndState();
            Close();
        }

        private void BtnOdustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await PopuniComboBoxove();
        }

        private async Task PopuniComboBoxove()
        {
            try
            {
                int idPoduzece = AppKontekst.prijavljeniKorisnik.Poduzece_id;
                var farme = await Client.GetAsync<List<Farma>>($"/api/Farma/Poduzece/{idPoduzece}");
                var uzgojneKulture = await Client.GetAsync<List<Uzgojna_kultura>>("/api/UzgojnaKultura");

                if (farme != null && uzgojneKulture != null)
                {
                    CmbFarma.ItemsSource = farme;
                    CmbUzgojnaKultura.ItemsSource = uzgojneKulture;
                    CmbFarma.DisplayMemberPath = "Lokacija";
                    CmbUzgojnaKultura.DisplayMemberPath = "Naziv";
                }
                else
                {
                    MessageBox.Show("Greška prilikom učitavanja podataka.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška prilikom učitavanja podataka: {ex.Message}");
            }
        }

        private async Task<Silos> Dodaj()
        {
            var kapacitet = TxtKapacitet.txtInput.Text;
            var farma = CmbFarma.SelectedItem as Farma;
            var kultura = CmbUzgojnaKultura.SelectedItem as Uzgojna_kultura;

            if (string.IsNullOrEmpty(kapacitet) || farma == null || kultura == null)
            {
                GreskaView greska = new GreskaView("Molimo ispunite sva polja.");
                greska.ShowDialog();
                return null;
            }

            try
            {
                var silos = new Silos
                {
                    Popunjenost = 0,
                    Kapacitet = int.Parse(kapacitet),
                    Dostupnost = 1,
                    Farma_id = farma.Farma_id,
                    Uzgojna_kultura_id = kultura.Uzgojna_kultura_id
                };

                string podaci = JsonConvert.SerializeObject(silos);
                var odgovor = await Client.PostAsync<bool>("/api/Silos", podaci);

                if (odgovor == true)
                {
                    return silos;
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