namespace ClassesPracticeCards;

interface ICard
{
    public string Name { get; }
}

interface ICardShower
{
    public void ShowCards(List<ICard> cards);
}

interface IPlayerDeck
{
    public IReadOnlyList<ICard> Cards { get; }
    public ICardShower CardShower { get; }
    public void AddCard(ICard card);

    public void ShowCards();
}

interface IDeck
{
    public IReadOnlyList<ICard> Cards { get; }

    public IPlayerDeck PlayerDeck { get; }

    public ICardShower CardShower { get; }

    public void PullCard(ICard card);

    public void ShowCards();
}

interface IPlayer
{
    public IPlayerDeck Deck { get; }
}

class Card : ICard
{
    public string Name => _name;

    private readonly string _name;

    public Card(string name)
    {
        _name = name;
    }
}

class Deck : IDeck
{
    public Deck(List<ICard> cards, IPlayerDeck playerDeck, ICardShower cardShower)
    {
        _cards = cards;
        _playerDeck = playerDeck;
        _cardShower = cardShower;
    }

    public IReadOnlyList<ICard> Cards => _cards;

    public ICardShower CardShower => _cardShower;

    public IPlayerDeck PlayerDeck => _playerDeck;

    private readonly IPlayerDeck _playerDeck;

    private readonly List<ICard> _cards;

    private readonly ICardShower _cardShower;


    public void PullCard(ICard card)
    {
        _playerDeck.AddCard(card);
        _cards.Remove(card);
    }

    public void ShowCards()
    {
        _cardShower.ShowCards(_cards);
    }
}

class PlayerDeck : IPlayerDeck
{
    public IReadOnlyList<ICard> Cards => _cards;
    public ICardShower CardShower => _cardShower;

    private readonly List<ICard> _cards;
    private readonly ICardShower _cardShower;

    public PlayerDeck(List<ICard> cards, ICardShower cardShower)
    {
        _cards = cards;
        _cardShower = cardShower;
    }

    public void AddCard(ICard card)
    {
        _cards.Add(card);
    }

    public void ShowCards()
    {
        _cardShower.ShowCards(_cards);
    }
}

class Player : IPlayer
{
    public IPlayerDeck Deck => _playerDeck;

    private readonly IPlayerDeck _playerDeck;

    public Player(IPlayerDeck playerDeck)
    {
        _playerDeck = playerDeck;
    }
}

class CardShower : ICardShower
{
    public void ShowCards(List<ICard> cards)
    {
        foreach (var card in cards)
        {
            Console.Write(card.Name + " ");
        }

        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        string inputString;
        int inputInt;

        IPlayerDeck playerDeck = new PlayerDeck(new List<ICard>(), new CardShower());

        IDeck deck = new Deck
        (new List<ICard>
        {
            new Card("Tuz"),
            new Card("Gaz"),
            new Card("Pika"),
            new Card("Den"),
            new Card("Bubna"),
            new Card("Dota"),
            new Card("War"),
            new Card("Beb"),
        }, playerDeck, new CardShower());


        IPlayer player = new Player(playerDeck);

        Console.WriteLine(deck.Cards.Count);

        while (true)
        {
            if (deck.Cards.Count == 0)
            {
                Console.WriteLine("Вы забрали все карты");
                playerDeck.ShowCards();
                break;
            }

            Console.Write("Ваш выбор: ");
            inputString = Console.ReadLine();

            if (inputString == "pull")
            {
                Console.Write("Сколько кард взять?: ");
                inputInt = int.Parse(Console.ReadLine());

                if (inputInt > deck.Cards.Count)
                {
                    Console.WriteLine("Вы написали число больше, чем есть кард, вместо этого вы берете все карты");
                    inputInt = deck.Cards.Count;
                }

                for (int i = 0; i < inputInt; i++)
                {
                    deck.PullCard(deck.Cards[0]);
                }
            }

            else if (inputString == "show")
            {
                Console.WriteLine("Ваши карты: ");
                playerDeck.ShowCards();
                Console.WriteLine("Карты в колоде: ");
                deck.ShowCards();
            }
        }
    }
}