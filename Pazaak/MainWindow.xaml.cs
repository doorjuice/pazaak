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

using DAL;
using Log = global::TinyLogger;

namespace Pazaak
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Log.Start(Log.LogLevel.INFO);

            LocalPlayer = new("Revan"); Opponent = new("Malak");
            CurrentGame = new(LocalPlayer, Opponent);

            InitializeComponent();

            LocalPlayer.Hand.AddCard(new StandardCard(9));

            //Player winner = test.StartGame();
            //Console.WriteLine($"{winner.Name} is the new champion!");

            Log.Stop();
        }

        public Player LocalPlayer { get; }
        public Player Opponent { get; set; }
        public Game CurrentGame { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
