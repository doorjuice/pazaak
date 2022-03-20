using System.ComponentModel;
using Log = TinyLogger;

namespace DAL
{
    public enum Status
    {
        Active,
        Standing,
        Busted
    }

    public class Player : IComparable<Player>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private Deck table;

        public Player(string name)
        {
            Name = name;
            Hand = new Deck(4);
            Hand.AddCard(new StandardCard(5));
            Hand.AddCard(new StandardCard(6));
            table = new Deck(9);
            CurrentStatus = Status.Active;
        }

        public void Reset()
        {
            table = new Deck(9);
            CurrentStatus = Status.Active;
            Log.Debug($"{Name}'s table was reset.");
        }

        public string Name { get; }
        public Deck Hand { get; }

        public int NbWins { get; set; }
        public int CurrentScore { get { return table.TotalValue; } }
        public Status CurrentStatus { get; private set; }

        public Status PlayTurn(Card newCard)
        {
            if (CurrentStatus != Status.Active)
                throw new InvalidOperationException($"Player {Name} is {CurrentStatus}");

            table.AddCard(newCard);
            Log.Info($"{Name}'s turn starts: Received {newCard, -3} (Total={CurrentScore}).");

            CurrentStatus = ChooseAction();
            if (table.TotalValue > 20)
                CurrentStatus = Status.Busted;

            Log.Info($"  {Name}'s turn ends: {CurrentStatus, -12} (Total={CurrentScore}).");
            return CurrentStatus;
        }

        protected Status ChooseAction()
        {
            if (table.TotalValue <= 15)
                return Status.Active;
            else
                return Status.Standing;
        }

        public int CompareTo(Player? other)
        {
            if (other is null)
                return 1;
            else if (NbWins.CompareTo(other.NbWins) != 0)
                return NbWins.CompareTo(other.NbWins);
            else
                return CurrentScore.CompareTo(other.CurrentScore);
        }

        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
