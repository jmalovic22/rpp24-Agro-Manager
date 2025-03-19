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
    /// Interaction logic for UcPosaoCard.xaml
    /// </summary>
    public partial class UcPosaoCard : UserControl
    {
        private RestClient Client = new RestClient();
        public Posao Posao { get; set; }

        public UcPosaoCard()
        {
            InitializeComponent();
            this.DataContextChanged += UcPosaoCard_DataContextChanged;

            var cardTemplate = (UcCardTemplate)FindName("UcCardPredlozak");
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

        public UcPosaoCard(Posao posao)
        {
            InitializeComponent();
            Posao = posao;
        }

        private void UcPosaoCard_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is Posao posao)
            {
                Posao = posao;
            }
        }

        private void DocumentImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var napomenaDokumenta = new NapomenaView(Posao.Opis);
            napomenaDokumenta.ShowDialog();
        }

        private async void IzbrisiButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteView deleteView = new DeleteView();
            this.Opacity = 0.4;
            bool? rezultat = deleteView.ShowDialog();
            this.Opacity = 1;

            if (rezultat == true)
            {
                var uspjesnoBrisanje = await Obrisi(Posao.Posao_id);
                if (uspjesnoBrisanje == true)
                {
                    Visibility = Visibility.Collapsed;
                }
            }
        }

        private void UrediButton_Click(object sender, RoutedEventArgs e)
        {
            EditPosaoView editPosaoView = new EditPosaoView(Posao);
            editPosaoView.ShowDialog();
            Posao.Posao_id = editPosaoView.Posao.Posao_id;
            Posao.Naziv = editPosaoView.Posao.Naziv;
            Posao.Opis = editPosaoView.Posao.Opis;
            UserControl_Loaded(sender, e);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            TxtNaziv.Content = Posao.Naziv;
        }

        private async Task<bool> Obrisi(int id)
        {
            try
            {
                var odgovor = await Client.DeleteAsync<bool>("/api/Posao/" + id);
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
