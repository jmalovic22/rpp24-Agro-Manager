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
    /// Interaction logic for UcFarma.xaml
    /// </summary>
    public partial class UcFarma : UserControl
    {
        private RestClient Client = new RestClient();

        public ObservableCollection<Farma> ListaFarmi { get; set; } = new ObservableCollection<Farma>();
        public UcFarma()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void BtnDodaj_ButtonClick(object sender, RoutedEventArgs e)
        {
            var dodajNovuFarmu = new AddFarmaView();
            this.Opacity = 0.4;
            dodajNovuFarmu.ShowDialog();
            UserControl_Loaded(null, null);
            this.Opacity = 1;
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            StartState();
            var farme = await DohvatiPodatke();
            ListaFarmi.Clear();
            foreach (var farma in farme)
            {
                ListaFarmi.Add(farma);
            }
            EndState();
        }

        private async Task<List<Farma>> DohvatiPodatke()
        {
            try
            {
                int idPoduzece = AppKontekst.prijavljeniKorisnik.Poduzece_id;
                var farme = await Client.GetAsync<List<Farma>>($"/api/Farma/Poduzece/{idPoduzece}");
                if (farme == null) return new List<Farma>();
                return farme;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška");
                return new List<Farma>();
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
