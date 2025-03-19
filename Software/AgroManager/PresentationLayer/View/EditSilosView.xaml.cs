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
using Newtonsoft.Json;
using PresentationLayer.UserControls;

namespace PresentationLayer.View
{
    /// <summary>
    /// Interaction logic for EditSilosView.xaml
    /// </summary>
    public partial class EditSilosView : Window
    {
        private RestClient client = new RestClient();
        public Silos Silos { get; set; }


        public EditSilosView(Silos silos)
        {
            InitializeComponent();
            Silos = silos;
            TxtKapacitet.txtInput.Text = silos.Kapacitet.ToString();
            RbDostupno.IsChecked = silos.Dostupnost == 1 ? true : false;
            RbNedostupno.IsChecked = silos.Dostupnost == 0 ? true : false;
        }

        private async void BtnSpremi_Click(object sender, RoutedEventArgs e)
        {
            StartState();
            var uspjesno = await Azuriraj();
            if (uspjesno)
            {
                Close();
                EndState();
                new UcSilos().UserControl_Loaded(sender, e);
            }
            EndState();
        }

        private void BtnOdustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await PopuniComboBoxe();
        }

        private async Task PopuniComboBoxe()
        {
            try
            {
                int idPoduzece = AppKontekst.prijavljeniKorisnik.Poduzece_id;
                var farme = await client.GetAsync<List<Farma>>($"/api/Farma/Poduzece/{idPoduzece}");
                var uzgojneKulture = await client.GetAsync<List<Uzgojna_kultura>>("/api/UzgojnaKultura");

                if (farme != null && uzgojneKulture != null)
                {
                    CmbFarma.ItemsSource = farme;
                    CmbUzgojnaKultura.ItemsSource = uzgojneKulture;
                    CmbFarma.DisplayMemberPath = "Lokacija";
                    CmbUzgojnaKultura.DisplayMemberPath = "Naziv";
                    CmbFarma.SelectedItem = Silos.Farma;
                    CmbFarma.Text = Silos.Farma.Lokacija;
                    CmbUzgojnaKultura.SelectedItem = Silos.Uzgojna_kultura;
                    CmbUzgojnaKultura.Text = Silos.Uzgojna_kultura.Naziv;
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

        private async Task<bool> Azuriraj()
        {
            var kapacitet = TxtKapacitet.txtInput.Text;
            var farma = CmbFarma.SelectedItem as Farma;
            var kultura = CmbUzgojnaKultura.SelectedItem as Uzgojna_kultura;

            if (string.IsNullOrEmpty(kapacitet) || farma == null || kultura == null)
            {
                GreskaView greska = new GreskaView($"Molimo ispunite sva polja.");
                greska.ShowDialog();
                return false;
            }

            try
            {
                var silos = new Silos
                {
                    Silos_id = Silos.Silos_id,
                    Popunjenost = 0,
                    Kapacitet = int.Parse(kapacitet),
                    Dostupnost = RbDostupno.IsChecked == true ? 1 : 0,
                    Farma_id = farma.Farma_id,
                    Uzgojna_kultura_id = kultura.Uzgojna_kultura_id
                };

                string podaci = JsonConvert.SerializeObject(silos);
                var odgovor = await client.PutAsync<bool>("/api/Silos", podaci);

                if (odgovor == true)
                {
                    Silos = silos;
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                GreskaView greska = new GreskaView($"Greška prilikom ažuriranja podataka: {ex.Message}");
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
    }
}