using EntitiesLayer.Entiteti;
using EntitiesLayer;
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
    /// Interaction logic for UcPrikljucak.xaml
    /// </summary>
    public partial class UcPrikljucak : UserControl
    {
        private RestClient Client = new RestClient();

        public ObservableCollection<Prikljucak> ListaPrikljucaka { get; set; } = new ObservableCollection<Prikljucak>();
        public UcPrikljucak()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void BtnDodaj_ButtonClick(object sender, RoutedEventArgs e)
        {
            var dodajNoviPrikljucak = new AddPrikljucakView();
            this.Opacity = 0.4;
            dodajNoviPrikljucak.ShowDialog();
            UserControl_Loaded(null, null);
            this.Opacity = 1;
        }
        public async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            StartState();
            var prikljucci = await DohvatiSvePrikljucke();
            if (AppKontekst.vozilo != null)
            {
                prikljucci = prikljucci.Where(p => p.Dostupnost == "Dostupno").ToList();
                prikljucci = KopmatibilnostVozilaPrikljcka(prikljucci);
            }
            ListaPrikljucaka.Clear();
            if (prikljucci.Count == 0 && AppKontekst.vozilo != null)
            {
                EndState();
                GreskaView greska = new GreskaView("Nema raspoloživih priključaka za " + AppKontekst.vozilo.Vrsta + "!");
                greska.ShowDialog();
                return;
            }
            foreach (var prikljucak in prikljucci)
            {
                ListaPrikljucaka.Add(prikljucak);
            }
            EndState();
        }

        private List<Prikljucak> KopmatibilnostVozilaPrikljcka(List<Prikljucak> prikljucci)
        {
            List<Prikljucak> kompatibilniPrikljucci = prikljucci;
            switch (AppKontekst.vozilo.Vrsta) 
            {
                case "Traktor":
                    List<int> PrikljuciZaTraktor = new List<int> { 1, 2, 3, 4, 5, 7, 8 };
                    kompatibilniPrikljucci = prikljucci.Where(p => PrikljuciZaTraktor.Contains(p.Vrsta_prikljucka_id)).ToList();
                    break;

                case "Kombajn":
                    List<int> PrikljuciZaKombajn = new List<int> { 13, 14 };
                    kompatibilniPrikljucci = prikljucci.Where(p => PrikljuciZaKombajn.Contains(p.Vrsta_prikljucka_id)).ToList();
                    break;

                case "Kamion":
                    List<int> PrikljuciZaKamion = new List<int> { 10, 12 };
                    kompatibilniPrikljucci = prikljucci.Where(p => PrikljuciZaKamion.Contains(p.Vrsta_prikljucka_id)).ToList();
                    break;

                case "Kombi":
                    List<int> PrikljuciZaKombi = new List<int> { 9 };
                    kompatibilniPrikljucci = prikljucci.Where(p => PrikljuciZaKombi.Contains(p.Vrsta_prikljucka_id)).ToList();
                    break;

                case "Bager":
                    List<int> PrikljuciZaBager = new List<int> { 6, 11 };
                    kompatibilniPrikljucci = prikljucci.Where(p => PrikljuciZaBager.Contains(p.Vrsta_prikljucka_id)).ToList();
                    break;

                case "Vilicar":
                    List<int> PrikljuciZaVilicar = new List<int> { 11 };
                    kompatibilniPrikljucci = prikljucci.Where(p => PrikljuciZaVilicar.Contains(p.Vrsta_prikljucka_id)).ToList();
                    break;

                case "Automobil":
                    List<int> PrikljuciZaAutomobil = new List<int> { 9 };
                    kompatibilniPrikljucci = prikljucci.Where(p => PrikljuciZaAutomobil.Contains(p.Vrsta_prikljucka_id)).ToList();
                    break;
            }

            return kompatibilniPrikljucci;
        }

        public void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            AppKontekst.vozilo = null;
        }

        private async Task<List<Prikljucak>> DohvatiSvePrikljucke()
        {
            try
            {
                int idPoduzece = AppKontekst.prijavljeniKorisnik.Poduzece_id;
                var prikljucci = await Client.GetAsync<List<Prikljucak>>($"/api/Prikljucak/Poduzece/{idPoduzece}");
                if (prikljucci == null) return new List<Prikljucak>();
                return prikljucci;
            }
            catch (Exception ex)
            {
                GreskaView greska = new GreskaView($"Greška: {ex.Message}");
                greska.ShowDialog();
                return new List<Prikljucak>();
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
