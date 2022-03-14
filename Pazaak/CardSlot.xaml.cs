using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Logique d'interaction pour Card.xaml
    /// </summary>
    public partial class CardSlot : UserControl
    {
        public static readonly DependencyProperty BorderProperty = DependencyProperty.Register("CardBorder", typeof(Brush), typeof(CardSlot),
            new PropertyMetadata(Brushes.DarkGray, default, OnBorderChanged));

        private static object OnBorderChanged(DependencyObject d, object o)
        {
            var card = (CardSlot)d;
            return (card.Playable && o is Brush) ? o : Brushes.DarkGray;
        }

        private static readonly Brush[] Palette = new Brush[] {
            Brushes.LightGray, Brushes.DarkGreen, Brushes.DarkBlue, Brushes.DarkRed, Brushes.DarkMagenta
        };

        private enum CardType { Placeholder, MainDeck, Positive, Negative, Toggle }
        private CardType type = CardType.Placeholder;
        private int value = 0;

        public CardSlot()
        {
            InitializeComponent();
            Allo.Text = "Card";
        }

        public bool Playable { get { return type > CardType.MainDeck; } }
        public string CardText { get { return value == 0 ? "" : value.ToString(); } }
        public Brush CardColor { get { return Palette[(int)type]; } }
        public Brush CardBorder
        {
            get => (Brush)GetValue(BorderProperty);
            set => SetValue(BorderProperty, value);
        }
    }

    public class FixedRatioConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value * 4/3;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value * 3/4;
        }
    }
}
