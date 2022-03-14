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
            InitializeComponent();
            Log.Start(Log.LogLevel.INFO);

            Player p1 = new("Revan"), p2 = new("Malak");
            Game test = new(p1, p2);
            Player winner = test.StartGame();
            Console.WriteLine($"{winner.Name} is the new champion!");

            Log.Stop();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
