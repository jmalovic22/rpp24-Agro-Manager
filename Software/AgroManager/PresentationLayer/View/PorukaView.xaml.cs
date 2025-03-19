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
    /// Interaction logic for PorukaView.xaml
    /// </summary>
    public partial class PorukaView : Window
    {
        public PorukaView(Control control, string poruka)
        {
            InitializeComponent();
            TxtPoruka.Text = poruka;
            this.SizeToContent = SizeToContent.WidthAndHeight;

            Point location = GetControlPosition(control);
            this.Left = location.X ;
            var top = location.Y + (control.ActualHeight);
            if (top > SystemParameters.WorkArea.Bottom - 40)
            {
                top = location.Y - (control.ActualHeight) - 30;
            }
            this.Top = top;
        }

        private static Point GetControlPosition(Control control)
        {
            Point locationToScreen = control.PointToScreen(new Point(0, 0));
            var source = PresentationSource.FromVisual(control);
            return source.CompositionTarget.TransformFromDevice.Transform(locationToScreen);
        }

        private void DoubleAnimationCompleted(object sender, EventArgs e)
        {
            if (!this.IsMouseOver)
            {
                this.Close();
            }
        }
    }
}
