using EntitiesLayer.Entiteti;
using EntitiesLayer.Models;
using EntitiesLayer;
using Newtonsoft.Json;
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
using System.Collections.ObjectModel;

namespace PresentationLayer.UserControls
{
    /// <summary>
    /// Interaction logic for UcSilos.xaml
    /// </summary>
    public partial class UcSilos : UserControl
    {
        private RestClient Client = new RestClient();
        public ObservableCollection<Silos> ListaSilosa { get; set; } = new ObservableCollection<Silos>();

        public UcSilos()
        {
            InitializeComponent();
            DataContext = this;
        }

        public async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            StartState();
            var silosi = await DohvatiPodatke();
            ListaSilosa.Clear();
            foreach (var silos in silosi)
            {
                ListaSilosa.Add(silos);
            }
            EndState();
        }

        private void BtnDodaj_ButtonClick(object sender, RoutedEventArgs e)
        {
            var dodajSilosView = new AddSilosView();
            this.Opacity = 0.4;
            dodajSilosView.ShowDialog();
            UserControl_Loaded(null, null);
            this.Opacity = 1;
        }

        private async Task<List<Silos>> DohvatiPodatke()
        {
            try
            {
                var idPoduzece = AppKontekst.prijavljeniKorisnik.Poduzece_id;
                var silosi = await Client.GetAsync<List<Silos>>($"/api/Silos/Poduzeca/{idPoduzece}");
                if (silosi == null) return new List<Silos>();
                return silosi;
            }
            catch (Exception ex)
            {
                GreskaView greska = new GreskaView($"Greška: {ex.Message}");
                greska.ShowDialog();
                return new List<Silos>();
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