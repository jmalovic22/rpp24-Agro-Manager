using EntitiesLayer.Entiteti;
using EntitiesLayer;
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
using Newtonsoft.Json;

namespace PresentationLayer.View
{
    /// <summary>
    /// Interaction logic for RadniNalogDodajStavkuView.xaml
    /// </summary>
    public partial class RadniNalogDodajStavkuView : Window
    {
        public RestClient Client = new RestClient();

        public RadniNalogDodajStavkuView()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RbAktivan.IsChecked = true;
            RbZavrsen.IsChecked = false;
            RbZavrsen.IsEnabled = false;

            RbZavrsen.Foreground = Brushes.Gray;
            RbAktivan.Foreground = Brushes.Black;

            await DohvatiPoslove();
        }

        private void BtnSpremi_Click(object sender, RoutedEventArgs e)
        {
            if (CmbPosao.SelectedItem is Posao posao
                && CmbRazinaPrioriteta.SelectedItem != null)
            {
                StartState();

                if (!AppKontekst.privremenaListaZaStavkeRadnogNaloga.Any(k => k.Posao_id == posao.Posao_id))
                {
                    var stavka = new Stavka_radnog_naloga
                    {
                        Status = RbAktivan.IsChecked == true ? "Aktivan" : "Završen",
                        Razina_prioriteta = int.TryParse(CmbRazinaPrioriteta.SelectedItem?.ToString(), out int prioritet) ? prioritet : 1,
                        Napomena = TxtNapomenaInput.txtInput.Text,
                        Radni_nalog_id = 0,
                        Radni_nalog = null,
                        Posao_id = posao.Posao_id,
                        Posao = posao,
                    };
                    AppKontekst.privremenaListaZaStavkeRadnogNaloga.Add(stavka);
                    DialogResult = true;
                }

                EndState();
                Close();
            }
            else
            {
                GreskaView greska = new GreskaView("Molimo odaberite zaposlenika.");
                greska.ShowDialog();
            }
        }

        private void BtnOdustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async Task DohvatiPoslove()
        {
            try
            {
                var poslovi = await Client.GetAsync<List<Posao>>($"/api/Posao");

                if (poslovi != null)
                {
                    CmbPosao.ItemsSource = poslovi;
                    CmbPosao.DisplayMemberPath = "Naziv";
                }
                else
                {
                    GreskaView greska = new GreskaView($"Greška prilikom učitavanja podataka.");
                    greska.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                GreskaView greska = new GreskaView($"Greška prilikom učitavanja podataka: {ex.Message}");
                greska.ShowDialog();
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
