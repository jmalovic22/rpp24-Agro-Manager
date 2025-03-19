using EntitiesLayer;
using EntitiesLayer.Entiteti;
using EntitiesLayer.Models;
using PresentationLayer.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for UcVozilo.xaml
    /// </summary>
    public partial class UcVozilo : UserControl
    {
        private RestClient Client = new RestClient();

        public ObservableCollection<Vozilo> ListaVozila { get; set; } = new ObservableCollection<Vozilo>();
        public UcVozilo()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void BtnDodaj_ButtonClick(object sender, RoutedEventArgs e)
        {
            var dodajNovoVozilo = new AddVoziloView();
            this.Opacity = 0.4;
            dodajNovoVozilo.ShowDialog();
            UserControl_Loaded(null, null);
            this.Opacity = 1;
        }

        public async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            StartState();
            var vozila = await DohvatiPodatke();
            ListaVozila.Clear();
            foreach (var vozilo in vozila)
            {
                ListaVozila.Add(vozilo);
            }
            EndState();
        }

        private async Task<List<Vozilo>> DohvatiPodatke()
        {
            try
            {
                int idPoduzece = AppKontekst.prijavljeniKorisnik.Poduzece_id;
                var vozila = await Client.GetAsync<List<Vozilo>>($"/api/Vozilo/Poduzece/{idPoduzece}");
                if (vozila == null) return new List<Vozilo>();
                return vozila;
            }
            catch (Exception ex)
            {
                GreskaView greska = new GreskaView($"Greška: {ex.Message}");
                greska.ShowDialog();
                return new List<Vozilo>();
            }
        }

        private void StartState()
        {
            GifLoading.Visibility = Visibility.Visible;
        }
        private void EndState()
        {
            GifLoading.Visibility = Visibility.Hidden;
        }
    }
}
