using EntitiesLayer;
using EntitiesLayer.Entiteti;
using Newtonsoft.Json;
using PresentationLayer.UserControls;
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
    /// Interaction logic for EditPosaoView.xaml
    /// </summary>
    public partial class EditPosaoView : Window
    {
        public Posao Posao { get; set; }
        private RestClient Client = new RestClient();

        public EditPosaoView(Posao posao)
        {
            InitializeComponent();
            Posao = posao;
            TxtNazivInput.txtInput.Text = posao.Naziv;
            TxtOpisInput.txtInput.Text = posao.Opis;
        }

        private void BtnOdustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void BtnSpremi_Click(object sender, RoutedEventArgs e)
        {
            StartState();
            var uspjesno = await Azuriraj();
            if (uspjesno)
            {
                Close();
                EndState();
                new UcPosao().UserControl_Loaded(sender, e);
            }
            EndState();
        }

        private async Task<bool> Azuriraj()
        {

            if (string.IsNullOrEmpty(TxtNazivInput.txtInput.Text)
                || string.IsNullOrEmpty(TxtOpisInput.txtInput.Text))
            {
                GreskaView greska = new GreskaView("Molimo ispunite sva polja.");
                greska.ShowDialog();
                return false;
            }

            try
            {
                var posao = new Posao
                {
                    Posao_id = Posao.Posao_id,
                    Naziv = TxtNazivInput.txtInput.Text,
                    Opis = TxtOpisInput.txtInput.Text,
                };

                string podaci = JsonConvert.SerializeObject(posao);
                var odgovor = await Client.PutAsync<bool>("/api/Posao", podaci);

                if (odgovor == true)
                {
                    Posao = posao;
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                GreskaView greska = new GreskaView($"Greška pri ažuriranju: {ex.Message}");
                greska.ShowDialog();
                return false;
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
