using EntitiesLayer;
using EntitiesLayer.Entiteti;
using EntitiesLayer.Models;
using Newtonsoft.Json;
using PresentationLayer.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for EditVoziloView.xaml
    /// </summary>
    public partial class EditVoziloView : Window
    {
        public Vozilo Vozilo { get; set; }
        private RestClient Client = new RestClient();

        public EditVoziloView(Vozilo vozilo)
        {
            InitializeComponent();
            Vozilo = vozilo;
            CmbVrsta.ItemsSource = VrstaVozila.ListaVrstaVozila;
            int index = VrstaVozila.ListaVrstaVozila.FindIndex(v => v == vozilo.Vrsta);
            CmbVrsta.SelectedIndex = index;
            TxtVoziloMarka.txtInput.Text = vozilo.Marka;
            TxtVoziloJacinaMotora.txtInput.Text = vozilo.Jacina_motora_u_KS.ToString();
            TxtVoziloOpis.txtInput.Text = vozilo.Opis;
        }

        private void BtnOdustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void BtnSpremi_Click(object sender, RoutedEventArgs e)
        {
            StartState();
            var uspjesno = await AzurirajVozilo();

            if (uspjesno) {
                Close();
                EndState();
                new UcVozilo().UserControl_Loaded(sender, e);
            }
            EndState();
        }

        private async Task<bool> AzurirajVozilo()
        {

            if (string.IsNullOrEmpty(CmbVrsta.Text)
                || string.IsNullOrEmpty(TxtVoziloMarka.txtInput.Text)
                || string.IsNullOrEmpty(TxtVoziloJacinaMotora.txtInput.Text)
                || string.IsNullOrEmpty(TxtVoziloOpis.txtInput.Text))
            {
                GreskaView greska = new GreskaView("Molimo ispunite sva polja.");
                greska.ShowDialog();
                return false;
            }

            if (TxtVoziloOpis.txtInput.Text == Vozilo.Opis
                && CmbVrsta.Text == Vozilo.Vrsta
                && TxtVoziloJacinaMotora.txtInput.Text == Vozilo.Jacina_motora_u_KS.ToString() 
                && TxtVoziloMarka.txtInput.Text == Vozilo.Marka)
            {
                GreskaView greska = new GreskaView("Niste unijeli nikakve promjene!");
                greska.ShowDialog();
                return false;
            }

            if (!Regex.IsMatch(TxtVoziloJacinaMotora.txtInput.Text, @"^\d+$"))
            {
                GreskaView greska = new GreskaView("Greška:\r\nJačina motora mora biti broj!");
                greska.ShowDialog();
                return false;
            }

            try
            {
                var vozilo = new Vozilo
                {
                    Registracija = Vozilo.Registracija,
                    Vrsta = CmbVrsta.Text,
                    Marka = TxtVoziloMarka.txtInput.Text,
                    Jacina_motora_u_KS = int.Parse(TxtVoziloJacinaMotora.txtInput.Text),
                    Opis = TxtVoziloOpis.txtInput.Text,
                    Dostupnost = Vozilo.Dostupnost,
                    Poduzece_id = AppKontekst.prijavljeniKorisnik.Poduzece_id,
                    Prikljucak = Vozilo.Prikljucak
                };
                
                string podaci = JsonConvert.SerializeObject(vozilo);
                var odgovor = await Client.PutAsync<bool>("/api/Vozilo", podaci);

                if (odgovor == true)
                {
                    Vozilo = vozilo;
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
