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
    /// Interaction logic for UcPosao.xaml
    /// </summary>
    public partial class UcPosao : UserControl
    {
        private RestClient Client = new RestClient();
        public ObservableCollection<Posao> ListaPoslova { get; set; } = new ObservableCollection<Posao>();

        public UcPosao()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void BtnDodaj_ButtonClick(object sender, RoutedEventArgs e)
        {
            var dodajPosaoView = new AddPosaoView();
            this.Opacity = 0.4;
            dodajPosaoView.ShowDialog();
            UserControl_Loaded(null, null);
            this.Opacity = 1;
        }

        public async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            StartState();
            var poslovi = await DohvatiPodatke();
            ListaPoslova.Clear();
            foreach (var posao in poslovi)
            {
                ListaPoslova.Add(posao);
            }
            EndState();
        }

        private async Task<List<Posao>> DohvatiPodatke()
        {
            try
            {
                var poslovi = await Client.GetAsync<List<Posao>>($"/api/Posao");
                if (poslovi == null) return new List<Posao>();
                return poslovi;
            }
            catch (Exception ex)
            {
                GreskaView greska = new GreskaView($"Greška: {ex.Message}");
                greska.ShowDialog();
                return new List<Posao>();
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
