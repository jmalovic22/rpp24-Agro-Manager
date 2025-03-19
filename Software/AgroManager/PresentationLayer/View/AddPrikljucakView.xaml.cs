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
    /// Interaction logic for AddPrikljucakView.xaml
    /// </summary>
    public partial class AddPrikljucakView : Window
    {
        RestClient Client = new RestClient();
        public AddPrikljucakView()
        {
            InitializeComponent();
            CmbVrsta.ItemsSource = AppKontekst.vrstePrikljucaka;
            CmbVrsta.DisplayMemberPath = "Naziv";
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

        private void BtnOdustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private async Task<bool> Dodaj()
        {
            if (RbDa.IsChecked == true && string.IsNullOrEmpty(TxtRegistracijaInput.txtInput.Text))
            {
                GreskaView greska = new GreskaView("Molimo ispunite polje za registraciju");
                greska.ShowDialog();
                return false;
            }

            if (string.IsNullOrEmpty(CmbVrsta.Text)
                || string.IsNullOrEmpty(TxtMarkaInput.txtInput.Text))
            {
                GreskaView greska = new GreskaView("Molimo ispunite sva polja.");
                greska.ShowDialog();
                return false;
            }

            try
            {
                var prikljucak = new Prikljucak()
                {
                    Registracija = TxtRegistracijaInput.txtInput.Text,
                    Vrsta_prikljucka_id = ((Vrsta_prikljucka)CmbVrsta.SelectedItem).Vrsta_prikljucka_id,
                    Marka = TxtMarkaInput.txtInput.Text,
                    Poduzece_id = AppKontekst.prijavljeniKorisnik.Poduzece_id
                };

                string podaci = JsonConvert.SerializeObject(prikljucak);
                var odgovor = await Client.PostAsync<bool>("/api/Prikljucak", podaci);

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

        private void RbNeChecked(object sender, RoutedEventArgs e)
        {
            TxtRegistracijaInput.IsEnabled = false;
            TxtRegistracijaInput.txtInput.Text = "Nema";
        }

        private void RbDa_Checked(object sender, RoutedEventArgs e)
        {
            TxtRegistracijaInput.IsEnabled = true;
            TxtRegistracijaInput.txtInput.Text = "";
        }
    }
}
