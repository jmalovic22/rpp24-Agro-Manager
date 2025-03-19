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
    /// Interaction logic for GreskaView.xaml
    /// </summary>
    public partial class GreskaView : Window
    {
        public GreskaView(string poruka)
        {
            InitializeComponent();
            TxtPoruka.Text = poruka;
            this.SizeToContent = SizeToContent.Height;
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
