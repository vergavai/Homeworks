interface ITrader : ISeller
{
    IReadOnlyList<IProduct> ProductsInStock { get; }
}

interface ISeller
{
    void Sell(IProduct product);
}

interface IProduct
{
    string Name { get; }
}

interface IPlayer
{
    IInventory Inventory { get; }
}

interface IInventory
{
    IReadOnlyList<IItem> Items { get; }
    void AddItem(IItem item);
}

interface IItem
{
    string Name { get; }
}

class Trader : ITrader
{
    public IReadOnlyList<IProduct> ProductsInStock => _productsInStock;

    private readonly List<IProduct> _productsInStock;
    private readonly IInventory _inventory;

    public Trader(List<IProduct> productsInStock, IInventory inventory)
    {
        _productsInStock = productsInStock;
        _inventory = inventory;
    }

    public void Sell(IProduct product)
    {
        _inventory.AddItem(new Item(product.Name));
        _productsInStock.Remove(product);
    }
}

class Product : IProduct
{
    public string Name => _name;

    private readonly string _name;

    public Product(string name)
    {
        _name = name;
    }
}

class Player : IPlayer
{
    public IInventory Inventory => _inventory;

    private readonly IInventory _inventory;

    public Player(IInventory inventory)
    {
        _inventory = inventory;
    }
}

class Inventory : IInventory
{
    public IReadOnlyList<IItem> Items => _items;

    private readonly List<IItem> _items;

    public Inventory(List<IItem> items)
    {
        _items = items;
    }

    public void AddItem(IItem item)
    {
        _items.Add(item);
    }
} 

class Item : IItem
{
    public string Name => _name;

    private readonly string _name;

    public Item(string name)
    {
        _name = name;
    }
}

class Program
{
    public static void Main()
    {
        IInventory inventory = new Inventory(new List<IItem>());

        List<IProduct> productsInStock = new List<IProduct>
        {
            new Product("Balalaika"),
            new Product("Xui")
        };
        
        ITrader trader = new Trader(productsInStock, inventory);
        
        Console.WriteLine("Trader stock before sale:");
        
        foreach (var product in trader.ProductsInStock)
        {
            Console.WriteLine(product.Name);
        }
        
        Console.WriteLine();

        IPlayer player = new Player(inventory);
        
        trader.Sell(trader.ProductsInStock[1]);

        Console.WriteLine("Trader stock after sale:");
        
        foreach (var product in trader.ProductsInStock)
        {
            Console.WriteLine(product.Name);
        }
        
        Console.WriteLine();
        
        Console.WriteLine("Player Inventory:");
        
        foreach (var item in player.Inventory.Items)
        {
            Console.WriteLine(item.Name);
        }
    }
}