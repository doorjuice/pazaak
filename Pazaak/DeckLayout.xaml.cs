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

namespace Pazaak
{
    /// <summary>
    /// Logique d'interaction pour DeckLayout.xaml
    /// </summary>
    public partial class DeckLayout : UserControl
    {
        public static readonly DependencyProperty NbColumnsProperty =
            DependencyProperty.Register("NbColumns", typeof(int), typeof(DeckLayout), new PropertyMetadata(5));

        public DeckLayout()
        {
            InitializeComponent();
        }

        public int NbColumns
        {
            get { return (int)GetValue(NbColumnsProperty); }
            set { SetValue(NbColumnsProperty, value); }
        }

        private void UniformGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double ratio = ActualHeight > 0 ? (ActualWidth * 4) / (ActualHeight * 3) : 1;
            NbColumns = (int)Math.Ceiling(Math.Sqrt(ratio * 10));
        }
    }
}
