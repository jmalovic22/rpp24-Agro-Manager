using EntitiesLayer;
using EntitiesLayer.Entiteti;
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
    /// Interaction logic for UcZaposlenik.xaml
    /// </summary>
    public partial class UcZaposlenik : UserControl
    {
        private RestClient Client = new RestClient();
        public ObservableCollection<Korisnik> ListaKorisnika { get; set; } = new ObservableCollection<Korisnik>();

        public UcZaposlenik()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void BtnDodaj_ButtonClick(object sender, RoutedEventArgs e)
        {
            var dodajNovuFarmu = new AddZaposlenikView();
            this.Opacity = 0.4;
            dodajNovuFarmu.ShowDialog();
            UserControl_Loaded(null, null);
            this.Opacity = 1;
        }

        public async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            StartState();
            var korisnici = await DohvatiPodatke();
            ListaKorisnika.Clear();
            foreach (var korisnik in korisnici)
            {
                ListaKorisnika.Add(korisnik);
            }
            EndState();
        }

        private async Task<List<Korisnik>> DohvatiPodatke()
        {
            try
            {
                int idPoduzece = AppKontekst.prijavljeniKorisnik.Poduzece_id;
                var korisnici = await Client.GetAsync<List<Korisnik>>($"/api/Korisnik/Poduzece/{idPoduzece}");
                if (korisnici == null) return new List<Korisnik>();
                return korisnici;
            }
            catch (Exception ex)
            {
                GreskaView greska = new GreskaView($"Greška: {ex.Message}");
                greska.ShowDialog();
                return new List<Korisnik>();
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
