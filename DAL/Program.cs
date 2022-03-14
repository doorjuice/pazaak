using Log = TinyLogger;

namespace DAL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Log.Start(Log.LogLevel.INFO);

            Player p1 = new("Revan"), p2 = new("Malak");
            Game test = new(p1, p2);
            Player winner = test.StartGame();
            Console.WriteLine($"{winner.Name} is the new champion!");

            Log.Stop();
        }
    }
}
