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
    /// Interaction logic for EditPrikljucakView.xaml
    /// </summary>
    public partial class EditPrikljucakView : Window
    {
        public Prikljucak Prikljucak { get; set; }
        private RestClient Client = new RestClient();

        public EditPrikljucakView(Prikljucak prikljucak)
        {
            InitializeComponent();
            Prikljucak = prikljucak;
            CmbVrsta.ItemsSource = AppKontekst.vrstePrikljucaka;
            CmbVrsta.DisplayMemberPath = "Naziv";
            int index = AppKontekst.vrstePrikljucaka.FindIndex(p => p.Vrsta_prikljucka_id == prikljucak.Vrsta_prikljucka_id);
            CmbVrsta.SelectedIndex = index;
            TxtRegistracijaInput.txtInput.Text = prikljucak.Registracija;
            TxtOpisInput.txtInput.Text = prikljucak.Vrsta_prikljucka.Opis;
            TxtMarkaInput.txtInput.Text = prikljucak.Marka;
            TxtOpisInput.IsEnabled = false;
        }

        private void BtnOdustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void BtnSpremi_Click(object sender, RoutedEventArgs e)
        {
            StartState();

            var uspjesno = await AzurirajPrikljucak();

            if (uspjesno)
            {
                Close();
                EndState();
                new UcVozilo().UserControl_Loaded(sender, e);
            }
            EndState();
        }

        private async Task<bool> AzurirajPrikljucak()
        {

            if (string.IsNullOrEmpty(CmbVrsta.Text)
                || string.IsNullOrEmpty(TxtMarkaInput.txtInput.Text)
                || string.IsNullOrEmpty(TxtOpisInput.txtInput.Text))
            {
                GreskaView greska = new GreskaView("Molimo ispunite sva potrebna polja.");
                greska.ShowDialog();
                return false;
            }

            if (((Vrsta_prikljucka)CmbVrsta.SelectedItem).Vrsta_prikljucka_id == Prikljucak.Vrsta_prikljucka_id 
                && TxtMarkaInput.txtInput.Text == Prikljucak.Marka 
                && TxtRegistracijaInput.txtInput.Text == Prikljucak.Registracija)
            {
                GreskaView greska = new GreskaView("Niste unijeli nikakve promjene!");
                greska.ShowDialog();
                return false;
            }

            try
            {
                string registracija = "";
                if (TxtRegistracijaInput.txtInput.Text == "")
                {
                    registracija = "Nema";
                }
                else
                {
                    registracija = TxtRegistracijaInput.txtInput.Text;
                }
                var dostpunost = Prikljucak.Dostupnost;
                var prikljucak = new Prikljucak()
                {
                    Prikljucak_id = Prikljucak.Prikljucak_id,
                    Registracija = registracija,
                    Vrsta_prikljucka_id = ((Vrsta_prikljucka)CmbVrsta.SelectedItem).Vrsta_prikljucka_id,
                    Marka = TxtMarkaInput.txtInput.Text,
                    Poduzece_id = AppKontekst.prijavljeniKorisnik.Poduzece_id,
                    Dostupnost = dostpunost,
                    Vrsta_prikljucka = (Vrsta_prikljucka)CmbVrsta.SelectedItem
                };

                string podaci = JsonConvert.SerializeObject(prikljucak);
                var odgovor = await Client.PutAsync<bool>("/api/Prikljucak", podaci);

                if (odgovor == true)
                {
                    Prikljucak = prikljucak;
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
