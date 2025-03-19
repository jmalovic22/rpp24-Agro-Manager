using EntitiesLayer;
using EntitiesLayer.Entiteti;
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
using System.Windows.Shapes;

namespace PresentationLayer.View
{

    public partial class AddVrstaStokeFarmaView : Window
    {
        public int IdFarma { get; set; }
        private RestClient Client { get; set; } = new RestClient();


        public AddVrstaStokeFarmaView(int idFarme)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            WindowState = WindowState.Normal;
            IdFarma = idFarme;
            
        }

        private void BtnOdustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnSpremi_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CmbVrstaStoke.ItemsSource = await DohvatiPodatke();
            CmbVrstaStoke.DisplayMemberPath = "Naziv";
        }

        private async Task<List<Vrsta_stoke>> DohvatiPodatke()
        {
            try
            {
                var vrsteStokeKojeNisuNaFarmi = await Client.GetAsync<List<Vrsta_stoke>>($"/api/VrstaStokeFarma/Farma/{IdFarma}/Nema");
                if (vrsteStokeKojeNisuNaFarmi == null) return new List<Vrsta_stoke>();
                return vrsteStokeKojeNisuNaFarmi;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška");
                return new List<Vrsta_stoke>();
            }
        }
    }
}
