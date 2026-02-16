namespace ClassesPracticeCards;



//Есть колода с картами. Игрок достает карты, пока не решит, что ему хватит карт (может быть как выбор пользователя, так и количество сколько карт надо взять). После выводиться вся информация о вытянутых картах.
//Возможные классы: Карта, Колода, Игрок.


interface ICard
{
    public string Name { get; }
}

interface IPuller
{
    public void PullCard(ICard card, IPlayerDeck playerDeck);
}

interface ICardShower
{
    public void ShowCards();
}

interface IPlayerDeck : ICardShower
{
    public IReadOnlyList<ICard> Cards { get; }
    public void AddCard(ICard card);
}

interface IDeck : IPuller, ICardShower
{
    public IReadOnlyList<ICard> Cards { get; }
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
    public IReadOnlyList<ICard> Cards => _cards;

    private readonly List<ICard> _cards;

    public Deck(List<ICard> cards)
    {
        _cards = cards;
    }

    public void PullCard(ICard card, IPlayerDeck playerDeck)
    {
        playerDeck.AddCard(card);
        _cards.Remove(card);
    }

    public void ShowCards()
    {
        foreach (var card in _cards)
        {
            Console.Write(card.Name + " ");
        }

        Console.WriteLine();
    }
}

class PlayerDeck : IPlayerDeck
{
    public IReadOnlyList<ICard> Cards => _cards;

    private readonly List<ICard> _cards;

    public PlayerDeck(List<ICard> cards)
    {
        _cards = cards;
    }

    public void AddCard(ICard card)
    {
        _cards.Add(card);
    }

    public void ShowCards()
    {
        foreach (var card in _cards)
        {
            Console.Write(card.Name + " ");
        }

        Console.WriteLine();
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

class Program
{
    static void Main(string[] args)
    {
        string inputString;
        int inputInt;
        
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
        });

        IPlayerDeck playerDeck = new PlayerDeck(new List<ICard>());

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
                    deck.PullCard(deck.Cards[0], playerDeck);
                }
                
            }

            else if(inputString == "show")
            {
                Console.WriteLine("Ваши карты: ");
                playerDeck.ShowCards();
                Console.WriteLine("Карты в колоде: ");
                deck.ShowCards();
            }
            
            

        }
        


    }
}
