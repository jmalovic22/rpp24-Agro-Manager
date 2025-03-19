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
    /// Interaction logic for UcEvidencijeStokeFarma.xaml
    /// </summary>
    public partial class UcEvidencijeStokeFarma : UserControl
    {
        private RestClient Client = new RestClient();
        public ObservableCollection<Evidencija_stoke_farma> ListaEvidencijeStoke { get; set; } = new ObservableCollection<Evidencija_stoke_farma>();

        public UcEvidencijeStokeFarma()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void BtnDodaj_ButtonClick(object sender, RoutedEventArgs e)
        {
            var dodajEvidencijeStokeFarmaView = new AddEvidencijeStokeFarmaView();
            this.Opacity = 0.4;
            dodajEvidencijeStokeFarmaView.ShowDialog();
            UserControl_Loaded(null, null);
            this.Opacity = 1;
        }

        public async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            StartState();
            var evidencijeStoke = await DohvatiPodatke();
            ListaEvidencijeStoke.Clear();
            foreach (var evidencija in evidencijeStoke)
            {
                ListaEvidencijeStoke.Add(evidencija);
            }
            EndState();
        }

        private async Task<List<Evidencija_stoke_farma>> DohvatiPodatke()
        {
            try
            {
                int idPoduzece = AppKontekst.prijavljeniKorisnik.Poduzece_id;
                var evidencijaStokeFarma = await Client.GetAsync<List<Evidencija_stoke_farma>>($"/api/EvidencijaStokeFarma/Poduzece/{idPoduzece}");
                if (evidencijaStokeFarma == null) return new List<Evidencija_stoke_farma>();
                return evidencijaStokeFarma;
            }
            catch (Exception ex)
            {
                GreskaView greska = new GreskaView($"Greška: {ex.Message}");
                greska.ShowDialog();
                return new List<Evidencija_stoke_farma>();
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
