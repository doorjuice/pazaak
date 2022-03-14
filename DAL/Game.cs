using Log = TinyLogger;

namespace DAL
{
    public class Game
    {
        private readonly List<Player> allPlayers;
        private readonly Deck mainDeck;

        public Game(List<Player> players)
        {
            if (players.Count < 2)
                throw new ArgumentException("Must have at least 2 players to start a game");
            allPlayers = players;
            mainDeck = Deck.GenMainDeck();
        }

        public Game(params Player[] players) : this(players.ToList()) { }

        public Player StartGame()
        {
            Log.Info($"Game starts ({allPlayers.Count} players).");
            Player? winner;
            int nRound = 0;

            try
            {
                do
                {
                    foreach (Player p in allPlayers)
                        p.Reset();
                    Log.Info($"Round {++nRound} starts.");
                    GameLoop();
                    winner = FindWinner();
                } while (winner is null);
            }
            catch (Exception ex)
            {
                Log.Warn($"Game ended abruptly: {ex.Message}.");
                allPlayers.Sort();
                winner = allPlayers.Last();
            }

            Log.Info($"{winner.Name} wins the game ({winner.NbWins} points).");
            return winner;
        }

        private void GameLoop()
        {
            var activePlayers = new Queue<Player>(allPlayers);
            var standingPlayers = new List<Player>();
            var maxScore = 0;

            while (activePlayers.Count > 0)
            {
                var player = activePlayers.Dequeue();
                var status = player.PlayTurn(mainDeck.TakeRandomCard());
                switch (status)
                {
                    case Status.Active:
                        activePlayers.Enqueue(player);
                        break;
                    case Status.Standing:
                        standingPlayers.Add(player);
                        maxScore = player.CurrentScore > maxScore ? player.CurrentScore : maxScore;
                        break;
                    case Status.Busted:
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }

            Log.Info($"Round ended:");
            foreach (Player p in standingPlayers.Where(p => p.CurrentScore == maxScore))
            {
                p.NbWins++;
                Log.Info($"  {p.Name} scores a point (now at {p.NbWins}).");
            }
        }

        private Player? FindWinner()
        {
            var possibleWinners = allPlayers.OrderByDescending(p => p.NbWins).ToList();
            if (possibleWinners[0].NbWins >= 3 && possibleWinners[0].NbWins > possibleWinners[1].NbWins)
                return possibleWinners[0];
            else
                return null;
        }
    }
}
