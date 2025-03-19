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
    /// Interaction logic for EditFarmaView.xaml
    /// </summary>
    public partial class EditFarmaView : Window
    {
        public Farma Farma { get; set; }
        public List<Vrsta_stoke_farma> VrsteStokeFarme { get; set; }
        public RestClient Client = new RestClient();

        public EditFarmaView(Farma farma)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            WindowState = WindowState.Normal;
            Farma = farma;
            TxtPovrsinaInput.txtInput.Text = farma.Povrsina.ToString();
            TxtBrojZaposlenihInput.txtInput.Text = farma.Broj_zaposlenih.ToString();
            TxtLokacijaInput.txtInput.Text = farma.Lokacija;
            if (farma.Status == "Aktivna")
            {
                RbAktivna.IsChecked = true;
            }
            else
            {
                RbNeaktivna.IsChecked = true;
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
        private void btnDodajStokuFarma_Click(object sender, RoutedEventArgs e)
        {
            var dodajVrstuStokeFarmi = new AddVrstaStokeFarmaView(Farma.Farma_id);
            this.Opacity = 0.4;
            dodajVrstuStokeFarmi.ShowDialog();
            this.Opacity = 1;
        }

        private void BtnOdustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnSpremi_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void Window_Loaded_1(object sender, RoutedEventArgs e)
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
    }
}
