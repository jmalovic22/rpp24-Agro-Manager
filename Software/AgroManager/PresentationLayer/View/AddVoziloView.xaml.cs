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
    /// Interaction logic for AddVoziloView.xaml
    /// </summary>
    public partial class AddVoziloView : Window
    {

        RestClient Client = new RestClient();
        public AddVoziloView()
        {
            InitializeComponent();
            CmbVrsta.ItemsSource = VrstaVozila.ListaVrstaVozila;
        }

        private void BtnOdustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void BtnSpremi_Click(object sender, RoutedEventArgs e)
        {
            StartState();
            var uspjesnoDodavanje = await Dodaj();
            if (uspjesnoDodavanje)
            {
                Close();
                EndState();
            }
            EndState();
        }

        private async Task<bool> Dodaj()
        {

            if (string.IsNullOrEmpty(TxtRegistracijaInput.txtInput.Text) 
                || string.IsNullOrEmpty(CmbVrsta.Text) 
                || string.IsNullOrEmpty(TxtMarkaVozilaInput.txtInput.Text)
                || string.IsNullOrEmpty(TxtJacinaMotoraInput.txtInput.Text)
                || string.IsNullOrEmpty(TxtOpisVozilaInput.txtInput.Text))
            {
                GreskaView greska = new GreskaView("Molimo ispunite sva polja.");
                greska.ShowDialog();
                return false;
            }

            if (!Regex.IsMatch(TxtJacinaMotoraInput.txtInput.Text, @"^\d+$"))
            {
                GreskaView greska = new GreskaView("Greška:\r\nJačina motora mora biti broj!");
                greska.ShowDialog();
                return false;
            }

            try
            {
                var vozilo = new Vozilo
                {
                    Registracija = TxtRegistracijaInput.txtInput.Text,
                    Vrsta = CmbVrsta.Text,
                    Marka = TxtMarkaVozilaInput.txtInput.Text,
                    Jacina_motora_u_KS = int.Parse(TxtJacinaMotoraInput.txtInput.Text),
                    Opis = TxtOpisVozilaInput.txtInput.Text,
                    Poduzece_id = AppKontekst.prijavljeniKorisnik.Poduzece_id,
                };

                string podaci = JsonConvert.SerializeObject(vozilo);
                var odgovor = await Client.PostAsync<bool>("/api/Vozilo", podaci);

                return odgovor;
            }
            catch (Exception ex)
            {
                GreskaView greska = new GreskaView($"Greška pri spremanju: {ex.Message}");
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
