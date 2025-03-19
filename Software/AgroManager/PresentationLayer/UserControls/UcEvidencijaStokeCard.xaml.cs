using EntitiesLayer.Entiteti;
using PresentationLayer.View;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PresentationLayer.UserControls
{
    /// <summary>
    /// Interaction logic for UcEvidencijaStokeCard.xaml
    /// </summary>
    public partial class UcEvidencijaStokeCard : UserControl
    {
        private RestClient Client = new RestClient();
        public Evidencija_stoke_farma EvidencijaStokeFarma { get; set; }

        public UcEvidencijaStokeCard()
        {
            InitializeComponent();
            this.DataContextChanged += UcEvidencijaStokeCard_DataContextChanged;

            var cardTemplate = (UcCardTemplateBezEditButton)FindName("UcCardPredlozak");
            if (cardTemplate != null)
            {
                var urediBtn = (UcEditButton)cardTemplate.FindName("UcBtnUredi");
                if (urediBtn != null)
                {
                    urediBtn.ButtonClick += UrediButton_Click;
                }
                var izbrisiBtn = (UcDeleteButton)cardTemplate.FindName("UcBtnIzbrisi");
                if (izbrisiBtn != null)
                {
                    izbrisiBtn.ButtonClick += IzbrisiButton_Click;
                }
            }
        }

        public UcEvidencijaStokeCard(Evidencija_stoke_farma evidencijaStokeFarma)
        {
            InitializeComponent();
            EvidencijaStokeFarma = evidencijaStokeFarma;
        }

        private void UcEvidencijaStokeCard_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is Evidencija_stoke_farma evidencijaStokeFarma)
            {
                EvidencijaStokeFarma = evidencijaStokeFarma;
            }
        }

        private async void IzbrisiButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteView deleteView = new DeleteView();
            this.Opacity = 0.4;
            bool? rezultat = deleteView.ShowDialog();
            this.Opacity = 1;

            if (rezultat == true)
            {
                var uspjesnoBrisanje = await Obrisi(EvidencijaStokeFarma.Evidencija_stoke_farma_id);
                if (uspjesnoBrisanje == true)
                {
                    Visibility = Visibility.Collapsed;
                }
            }
        }

        private void UrediButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EvidencijaStokeFarmaImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NapomenaView napomenaDokumenta = new NapomenaView(EvidencijaStokeFarma.Napomena);
            napomenaDokumenta.ShowDialog();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            TxtDatum.Content = EvidencijaStokeFarma.Datum_promjene;
            TxtKolicina.Content = EvidencijaStokeFarma.Kolicina_promjene;
            TxtVrstaStokeFarma.Content = EvidencijaStokeFarma.Vrsta_stoke_farma;
        }

        private async Task<bool> Obrisi(int id)
        {
            try
            {
                var odgovor = await Client.DeleteAsync<bool>("/api/EvidencijaStokeFarma/" + id);
                return odgovor;
            }
            catch (Exception ex)
            {
                GreskaView greska = new GreskaView($"Greška prilikom brisanja podataka: {ex.Message}");
                greska.ShowDialog();
                return false;
            }
        }
    }
}
