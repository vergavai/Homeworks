namespace ClassesPracticeZoo
{
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

    interface ICageApproacher
    {
        public void ApproachCage(ICage cage);
    }

    interface IAnimalDisplay
    {
        public void DisplayAnimals(IEnumerable<IAnimal> animals);
    }

    interface IAnimalSpeaker
    {
        public void SpeakAnimalsSounds(IEnumerable<IAnimal> animals);
    }

    interface ICage
    {
        public string CageName { get; }
        public IReadOnlyList<IAnimal> Animals { get; }
        public ICageApproacher CageApproacher { get; }
        public IAnimalDisplay AnimalDisplay { get; }
        public IAnimalSpeaker AnimalSpeaker { get; }

        public void ApproachCage();

        public void DisplayAnimals();

        public void SpeakAnimalsSounds();
    }

    class CageApproacher : ICageApproacher
    {
        public void ApproachCage(ICage cage)
        {
            Console.WriteLine($"Вы подошли к вольеру {cage.CageName}");
            Console.WriteLine("Животные: ");
            cage.AnimalDisplay.DisplayAnimals(cage.Animals);
            Console.WriteLine("Их звуки: ");
            cage.AnimalSpeaker.SpeakAnimalsSounds(cage.Animals);
        }
    }

    class AnimalDisplay : IAnimalDisplay
    {
        public void DisplayAnimals(IEnumerable<IAnimal> animals)
        {
            foreach (var animal in animals)
            {
                Console.WriteLine(animal.AnimalName + " ");
            }

            Console.WriteLine();
        }
    }

    class AnimalSpeaker : IAnimalSpeaker
    {
        public void SpeakAnimalsSounds(IEnumerable<IAnimal> animals)
        {
            foreach (var animal in animals)
            {
                animal.MakeSound();
            }

            Console.WriteLine();
        }
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
        public string CageName => _cageName;
        public IReadOnlyList<IAnimal> Animals => _animals;
        public ICageApproacher CageApproacher => _cageApproacher;
        public IAnimalDisplay AnimalDisplay => _animalDisplay;
        public IAnimalSpeaker AnimalSpeaker => _animalSpeaker;

        private readonly List<IAnimal> _animals;
        private readonly ICageApproacher _cageApproacher;
        private readonly IAnimalSpeaker _animalSpeaker;
        private readonly IAnimalDisplay _animalDisplay;

        private string _cageName;

        public Cage(List<IAnimal> animals, string cageName, IAnimalDisplay animalDisplay, IAnimalSpeaker animalSpeaker,
            ICageApproacher cageApproacher)
        {
            _cageName = cageName;
            _animals = animals;
            _animalDisplay = animalDisplay;
            _animalSpeaker = animalSpeaker;
            _cageApproacher = cageApproacher;
        }

        public void ApproachCage()
        {
            _cageApproacher.ApproachCage(this);
        }

        public void DisplayAnimals()
        {
            _animalDisplay.DisplayAnimals(_animals);
        }

        public void SpeakAnimalsSounds()
        {
            _animalSpeaker.SpeakAnimalsSounds(_animals);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string stringInput;
            int intInput;
            
            AnimalDisplay animalDisplay = new AnimalDisplay();
            AnimalSpeaker animalSpeaker = new AnimalSpeaker();
            CageApproacher cageApproacher = new CageApproacher();

            ICage cage = new Cage(new List<IAnimal>()
            {
                new Animal("Fox", AnimalSounds.Awooo),
                new Animal("Dog", AnimalSounds.Awooo),
                new Animal("Wolf", AnimalSounds.Awooo),
            }, "Псовые", animalDisplay, animalSpeaker, cageApproacher);

            ICage cage2 = new Cage(new List<IAnimal>()
            {
                new Animal("Lion", AnimalSounds.Meow),
                new Animal("Tiger", AnimalSounds.Meow),
                new Animal("Leopard", AnimalSounds.Meow),
            }, "Кошачьи", animalDisplay, animalSpeaker, cageApproacher);

            ICage cage3 = new Cage(new List<IAnimal>()
            {
                new Animal("Bear", AnimalSounds.Harhar),
                new Animal("Panda", AnimalSounds.Harhar),
            }, "Медвежьи", animalDisplay, animalSpeaker, cageApproacher);

            ICage cage4 = new Cage(new List<IAnimal>()
            {
                new Animal("Whale", AnimalSounds.Roar),
                new Animal("Gryphon", AnimalSounds.Roar),
            }, "Ревущие", animalDisplay, animalSpeaker, cageApproacher);

            List<ICage> cages = new List<ICage>() { cage, cage2, cage3, cage4 };

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
}