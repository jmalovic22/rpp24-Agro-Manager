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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PresentationLayer.UserControls
{

    public partial class UcEditButton : UserControl
    {
        public event RoutedEventHandler ButtonClick;
        public event RoutedEventHandler ButtonMouseOver;
        public event RoutedEventHandler ButtonMouseLeave;


        public UcEditButton()
        {
            InitializeComponent();
            UrediButton.Click += OnUrediButtonClick; // Povezivanje lokalnog klika s metodom
            UrediButton.MouseEnter += OnUrediButtonMouseEnter;
            UrediButton.MouseLeave += OnUrediButtonMouseLeave;
        }

        private void OnUrediButtonClick(object sender, RoutedEventArgs e)
        {
            // Prosljeđivanje događaja vanjskom kontroleru
            ButtonClick?.Invoke(this, e);
        }

        private void OnUrediButtonMouseEnter(object sender, RoutedEventArgs e)
        {
            // Prosljeđivanje događaja vanjskom kontroleru
            ButtonMouseOver?.Invoke(this, e);
            Opacity = 0.85;

        }
        private void OnUrediButtonMouseLeave(object sender, RoutedEventArgs e)
        {
            // Prosljeđivanje događaja vanjskom kontroleru
            ButtonMouseLeave?.Invoke(this, e);
            Opacity = 1;
        }
    }
}
