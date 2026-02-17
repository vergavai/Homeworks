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
        public void ApproachCage(string cageName, List<IAnimal> animals, IAnimalDisplay animalDisplay,
            IAnimalSpeaker animalSpeaker);
    }

    interface IAnimalDisplay
    {
        public void DisplayAnimals(List<IAnimal> animals);
    }

    interface IAnimalSpeaker
    {
        public void SpeakAnimalsSounds(List<IAnimal> animals);
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
        public void ApproachCage(string cageName, List<IAnimal> animals, IAnimalDisplay animalDisplay,
            IAnimalSpeaker animalSpeaker)
        {
            Console.WriteLine($"Вы подошли к вольеру {cageName}");
            Console.WriteLine("Животные: ");
            animalDisplay.DisplayAnimals(animals);
            Console.WriteLine("Их звуки: ");
            animalSpeaker.SpeakAnimalsSounds(animals);
        }
    }

    class AnimalDisplay : IAnimalDisplay
    {
        public void DisplayAnimals(List<IAnimal> animals)
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
        public void SpeakAnimalsSounds(List<IAnimal> animals)
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
        public AnimalSounds Sounds => _sound;

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
            _cageApproacher.ApproachCage(_cageName, _animals, _animalDisplay, _animalSpeaker);
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

            ICage cage = new Cage(new List<IAnimal>()
            {
                new Animal("Fox", AnimalSounds.Awooo),
                new Animal("Dog", AnimalSounds.Awooo),
                new Animal("Wolf", AnimalSounds.Awooo),
            }, "Псовые", new AnimalDisplay(), new AnimalSpeaker(), new CageApproacher());

            ICage cage2 = new Cage(new List<IAnimal>()
            {
                new Animal("Lion", AnimalSounds.Meow),
                new Animal("Tiger", AnimalSounds.Meow),
                new Animal("Leopard", AnimalSounds.Meow),
            }, "Кошачьи", new AnimalDisplay(), new AnimalSpeaker(), new CageApproacher());

            ICage cage3 = new Cage(new List<IAnimal>()
            {
                new Animal("Bear", AnimalSounds.Harhar),
                new Animal("Panda", AnimalSounds.Harhar),
            }, "Медвежьи", new AnimalDisplay(), new AnimalSpeaker(), new CageApproacher());

            ICage cage4 = new Cage(new List<IAnimal>()
            {
                new Animal("Whale", AnimalSounds.Roar),
                new Animal("Gryphon", AnimalSounds.Roar),
            }, "Ревущие", new AnimalDisplay(), new AnimalSpeaker(), new CageApproacher());

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