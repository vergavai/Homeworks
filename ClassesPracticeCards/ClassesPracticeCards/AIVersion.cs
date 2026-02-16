// Карта
public interface ICard
{
    string Name { get; }
}

// Базовая коллекция карт (только чтение)
public interface IReadOnlyCardCollection
{
    IReadOnlyList<ICard> Cards { get; }
}

// Колода, из которой можно вытягивать карты
public interface IDeck : IReadOnlyCardCollection
{
    ICard DrawCard(); // тянет верхнюю карту
    ICard DrawCard(ICard card); // тянет конкретную карту (если есть)
}

// Колода игрока (можно добавлять карты)
public interface IPlayerDeck : IReadOnlyCardCollection
{
    void AddCard(ICard card);
    void RemoveCard(ICard card); // если нужно
}

// Игрок
public interface IPlayer
{
    IPlayerDeck Deck { get; }
}

// Реализация карты
public class Card : ICard
{
    public string Name { get; }
    public Card(string name) => Name = name;
}

// Базовая реализация коллекции (для избежания дублирования)
public class CardCollection : IReadOnlyCardCollection
{
    private readonly List<ICard> _cards;

    public IReadOnlyList<ICard> Cards => _cards;

    public CardCollection(IEnumerable<ICard> cards)
    {
        _cards = cards?.ToList() ?? new List<ICard>();
    }

    protected void Add(ICard card) => _cards.Add(card);
    protected bool Remove(ICard card) => _cards.Remove(card);
    protected void Clear() => _cards.Clear();
}

// Колода
public class Deck : CardCollection, IDeck
{
    public Deck(IEnumerable<ICard> cards) : base(cards) { }

    public ICard DrawCard()
    {
        if (Cards.Count == 0) return null;
        var card = Cards[0];
        Remove(card);
        return card;
    }

    public ICard DrawCard(ICard card)
    {
        if (Remove(card))
            return card;
        return null;
    }
}

// Колода игрока
public class PlayerDeck : CardCollection, IPlayerDeck
{
    public PlayerDeck(IEnumerable<ICard> cards) : base(cards) { }

    public void AddCard(ICard card) => Add(card);
    public void RemoveCard(ICard card) => Remove(card);
}

// Игрок
public class Player : IPlayer
{
    public IPlayerDeck Deck { get; }
    public Player(IPlayerDeck deck) => Deck = deck;
}