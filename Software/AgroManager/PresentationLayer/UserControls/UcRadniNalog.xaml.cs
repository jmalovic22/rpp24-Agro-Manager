using EntitiesLayer.Entiteti;
using EntitiesLayer.Models;
using EntitiesLayer;
using Newtonsoft.Json;
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
    /// Interaction logic for UcRadniNalog.xaml
    /// </summary>
    public partial class UcRadniNalog : UserControl
    {
        private RestClient client = new RestClient();
        public ObservableCollection<Radni_nalog> ListaNaloga { get; set; } = new ObservableCollection<Radni_nalog>();

        public UcRadniNalog()
        {
            InitializeComponent();
            DataContext = this;
        }

        private async Task<List<Radni_nalog>> DohvatiPodatke()
        {
            try
            {
                var radniNalozi = await client.GetAsync<List<Radni_nalog>>($"/api/RadniNalog/Poduzece/{AppKontekst.prijavljeniKorisnik.Poduzece_id}");
                return radniNalozi;
            }
            catch (Exception ex)
            {
                GreskaView greska = new GreskaView($"Greška: {ex.Message}");
                greska.ShowDialog();
                return new List<Radni_nalog>();
            }
        }
        private void BtnDodaj_ButtonClick(object sender, RoutedEventArgs e)
        {
            var dodajRadniNalogView = new AddRadniNalogView2();
            this.Opacity = 0.4;
            dodajRadniNalogView.ShowDialog();
            this.Opacity = 1;
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            StartState();
            var podaci = await DohvatiPodatke();
            ListaNaloga.Clear();
            foreach (var nalog in podaci)
            {
                ListaNaloga.Add(nalog);
            }
            EndState();
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
