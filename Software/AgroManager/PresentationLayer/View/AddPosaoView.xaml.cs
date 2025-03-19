using EntitiesLayer.Entiteti;
using EntitiesLayer;
using Newtonsoft.Json;
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
    /// Interaction logic for AddPosaoView.xaml
    /// </summary>
    public partial class AddPosaoView : Window
    {
        RestClient Client = new RestClient();

        public AddPosaoView()
        {
            InitializeComponent();
        }

        private void BtnOdustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void BtnSpremi_Click(object sender, RoutedEventArgs e)
        {
            StartState();
            var uspjesnoDodavanje = await Dodaj();
            EndState();
            Close();
        }

        private async Task<Posao> Dodaj()
        {

            if (string.IsNullOrEmpty(TxtNazivInput.txtInput.Text)
                || string.IsNullOrEmpty(TxtOpisInput.txtInput.Text))
            {
                GreskaView greska = new GreskaView("Molimo ispunite sva polja.");
                greska.ShowDialog();
                return null;
            }

            try
            {
                var posao = new Posao
                {
                    Naziv = TxtNazivInput.txtInput.Text,
                    Opis = TxtOpisInput.txtInput.Text,
                };

                string podaci = JsonConvert.SerializeObject(posao);
                var odgovor = await Client.PostAsync<bool>("/api/Posao", podaci);

                if (odgovor == true)
                {
                    return posao;
                }

                return null;
            }
            catch (Exception ex)
            {
                GreskaView greska = new GreskaView($"Greška pri spremanju: {ex.Message}");
                greska.ShowDialog();
                return null;
            }
        }

        private void StartState()
        {
            BtnOdustani.IsEnabled = false;
            BtnOdustani.Opacity = 0.5;
            BtnSpremi.IsEnabled = false;
            BtnSpremi.Opacity = 0.5;

        }
        private void EndState()
        {
            BtnOdustani.IsEnabled = true;
            BtnOdustani.Opacity = 1;
            BtnSpremi.IsEnabled = true;
            BtnSpremi.Opacity = 1;
        }
    }
}
