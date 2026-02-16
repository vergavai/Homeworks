namespace ClassesPracticeZoo;

//Пользователь запускает приложение и перед ним находится меню, в котором он может выбрать,
//к какому вольеру подойти. При приближении к вольеру, пользователю выводится информация о том,
//что это за вольер, сколько животных там обитает, их пол и какой звук издает животное.
//Вольеров в зоопарке может быть много, в решении нужно создать минимум 4 вольера.

public enum AnimalSounds
{
    Roar,
    Meow,
    Harhar,
    Awooo,
}

interface IAnimal
{
    public AnimalSounds Sound { get; }

    public string AnimalName { get; }
    
    public void MakeSound();
}

interface ICage
{
    public string CageName { get; }
    public IReadOnlyList<IAnimal> Animals { get; }

    public void ApproachCage();

    public void ShowAnimals();

    public void SpeakAnimalsSounds();
}

class Animal : IAnimal
{
    public string AnimalName => _animalName;

    public AnimalSounds Sound => _sound;

    private string _animalName;

    private AnimalSounds _sound;

    public Animal(string animalName, AnimalSounds sound)
    {
        _animalName = animalName;
        _sound = sound;
    }

    public void MakeSound()
    {
        Console.WriteLine(_sound);
    }
}

class Cage : ICage
{
    public Cage(List<IAnimal> animals, string cageName)
    {
        _animals = animals;
        _cageName = cageName;
    }

    public string CageName => _cageName;
    public IReadOnlyList<IAnimal> Animals => _animals;

    private readonly List<IAnimal> _animals;
    
    private string _cageName;

    public Cage(List<IAnimal> animals)
    {
        _animals = animals;
    }

    public void ApproachCage()
    {
        Console.WriteLine($"Вы подошли к вольеру {_cageName}");
        Console.WriteLine("Животные: ");
        ShowAnimals();
        Console.WriteLine("Их звуки: ");
        SpeakAnimalsSounds();
    }

    public void ShowAnimals()
    {
        foreach (var animal in Animals)
        {
            Console.WriteLine(animal.AnimalName + " ");
        }
        
        Console.WriteLine();
    }

    public void SpeakAnimalsSounds()
    {
        foreach (var animal in Animals)
        {
            animal.MakeSound();
        }
        
        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        string stringInput;
        int intInput;
        
        ICage cage = new Cage(new List<IAnimal>()
        {
            new Animal("Fox", AnimalSounds.Awooo),
            new Animal("Dog", AnimalSounds.Awooo),
            new Animal("Wolf", AnimalSounds.Awooo),
        }, "Псовые");
        
        ICage cage2 = new Cage(new List<IAnimal>()
        {
            new Animal("Lion", AnimalSounds.Meow),
            new Animal("Tiger", AnimalSounds.Meow),
            new Animal("Leopard", AnimalSounds.Meow),
        }, "Кошачьи");
        
        ICage cage3 = new Cage(new List<IAnimal>()
        {
            new Animal("Bear", AnimalSounds.Harhar),
            new Animal("Panda", AnimalSounds.Harhar),
        }, "Медвежьи");
        
        ICage cage4 = new Cage(new List<IAnimal>()
        {
            new Animal("Whale", AnimalSounds.Roar),
            new Animal("Gryphon", AnimalSounds.Roar),
        }, "Ревущие");
        
        List<ICage> cages = new List<ICage>() {cage, cage2, cage3, cage4};

        while (true)
        {
            Console.Write("Что делать: ");
            stringInput = Console.ReadLine();

            if (stringInput == "see")
            {
                Console.Write("Всего 4 вольера, к какому подойти: ");
                intInput = Convert.ToInt32(Console.ReadLine());
                cages[intInput - 1].ApproachCage();
            }
            else if (stringInput == "quit")
            {
                break;
            }
        }
        
        
    }
}