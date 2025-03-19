using EntitiesLayer;
using EntitiesLayer.Entiteti;
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

namespace PresentationLayer.View
{
    /// <summary>
    /// Interaction logic for FarmaDokumentView.xaml
    /// </summary>
    public partial class FarmaDokumentView : Window
    {
        public RestClient Client = new RestClient();
        public List<Vrsta_stoke_farma> VrsteStokeFarme { get; set; }
        public Farma Farma { get; set; }
        public FarmaDokumentView(Farma farma)
        {
            InitializeComponent();
            Farma = farma;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GifLoading.Visibility = Visibility.Visible;
            VrsteStokeFarme = await DohvatiPodatke();
            GifLoading.Visibility = Visibility.Collapsed;
            StackPanelVrsteStokeNaFarmi.Children.Clear();

            foreach (var vrsta in VrsteStokeFarme)
            {
                Expander expander = new Expander
                {
                    Width = 200,
                    Margin = new Thickness(0, 10, 0, 0),
                    Header = vrsta.Vrsta_stoke.Naziv,
                    Style = FindResource("CustomExpanderStyle") as Style
                };

                StackPanel stackPanel = new StackPanel
                {
                    Margin = new Thickness(5)
                };

                stackPanel.Children.Add(new TextBlock { Text = $"Količina stoke: {vrsta.Kolicina_stoke}" });

                expander.Content = stackPanel;

                StackPanelVrsteStokeNaFarmi.Children.Add(expander);
            }
        }

        private async Task<List<Vrsta_stoke_farma>> DohvatiPodatke()
        {
            try
            {
                var vozila = await Client.GetAsync<List<Vrsta_stoke_farma>>($"/api/VrstaStokeFarma/Farma/{Farma.Farma_id}");
                if (vozila == null) return new List<Vrsta_stoke_farma>();
                return vozila;
            }
            catch (Exception ex)
            {
                GreskaView greska = new GreskaView($"Greška: {ex.Message}");
                greska.ShowDialog();
                return new List<Vrsta_stoke_farma>();
            }
        }

        private void BtnIzadji_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
