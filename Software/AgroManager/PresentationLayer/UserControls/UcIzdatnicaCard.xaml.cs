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
    /// Interaction logic for UcIzdatnicaCard.xaml
    /// </summary>
    public partial class UcIzdatnicaCard : UserControl
    {
        public UcIzdatnicaCard()
        {
            InitializeComponent();
            var cardTemplate = (UcCardTemplate)FindName("UcCardPredlozak");
            if (cardTemplate != null)
            {
                // Pronađi UcEditButton unutar UcCardTemplate i pretplati se na ButtonClick događaj
                var urediBtn = (UcEditButton)cardTemplate.FindName("UcBtnUredi"); // Ako ste dodali x:Name za UcEditButton
                if (urediBtn != null)
                {
                    urediBtn.ButtonClick += UrediButton_Click;
                }
                var izbrisiBtn = (UcDeleteButton)cardTemplate.FindName("UcBtnIzbrisi"); // Ako ste dodali x:Name za UcEditButton
                if (izbrisiBtn != null)
                {
                    izbrisiBtn.ButtonClick += IzbrisiButton_Click;
                }
            }
        }

        private void UrediButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Uredivanje podataka dokumenta nije moguce", "Potvrda", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.Cancel);
        }

        private void IzbrisiButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Jeste li sigurni da zelite obrisati", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.Cancel);
        }
        private void IzdatnicaImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Otvorili ste biljeske za izdatnicu");
        }

    }
}
