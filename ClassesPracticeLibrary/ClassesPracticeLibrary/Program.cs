namespace ClassesPracticeLibrary
{
    //Создать хранилище книг.
    //Каждая книга имеет название, автора и год выпуска (можно добавить еще параметры).
    //В хранилище можно добавить книгу, убрать книгу, показать все книги и показать
    //книги по указанному параметру (по названию, по автору, по году выпуска).
    //Про указанный параметр, к примеру нужен конкретный автор, выбирается показ по авторам,
    //запрашивается у пользователя автор и показываются все книги с этим автором.

    interface IBook
    {
        public string Title { get; }
        public string Author { get; }
        public DateOnly Date { get; }
    }

    interface IBookDisplay
    {
        public void DisplayBooks(IEnumerable<IBook> books);
    }

    interface IBookSortStrategy
    {
        IReadOnlyList<IBook> Sort(IEnumerable<IBook> books);
    }

    interface ILibrary
    {
        public IReadOnlyList<IBook> Books { get; }

        public void AddBook(IBook book);
        public void RemoveBook(IBook book);
    }

    class BookDisplay : IBookDisplay
    {
        public void DisplayBooks(IEnumerable<IBook> books)
        {
            foreach (IBook book in books)
            {
                Console.Write($"Title: {book.Title}, Author: {book.Author}, Date: {book.Date}");
            }
        }
    }

    class SortByTitleDescendingStrategy : IBookSortStrategy
    {
        public IReadOnlyList<IBook> Sort(IEnumerable<IBook> books)
        {
            return books.OrderByDescending(book => book.Title, StringComparer.Ordinal).ToList();
        }
    }

    class SortByAuthorDescendingStrategy : IBookSortStrategy
    {
        public IReadOnlyList<IBook> Sort(IEnumerable<IBook> books)
        {
            return books.OrderByDescending(book => book.Author, StringComparer.Ordinal).ToList();
        }
    }

    class SortByDateDescendingStrategy : IBookSortStrategy
    {
        public IReadOnlyList<IBook> Sort(IEnumerable<IBook> books)
        {
            return books.OrderByDescending(book => book.Date).ToList();
        }
    }

    class BookSorterContext
    {
        private readonly IBookSortStrategy _strategy;

        public BookSorterContext(IBookSortStrategy strategy)
        {
            _strategy = strategy;
        }

        public IReadOnlyList<IBook> SortBooks(IEnumerable<IBook> books)
        {
            return _strategy.Sort(books);
        }
    }

    class Book : IBook
    {
        public string Title => _title;
        public string Author => _author;
        public DateOnly Date => _date;

        private readonly string _title;
        private readonly string _author;
        private readonly DateOnly _date;

        public Book(string title, string author, DateOnly date)
        {
            _title = title;
            _author = author;
            _date = date;
        }
    }


    class Library : ILibrary
    {
        public IReadOnlyList<IBook> Books => _books;

        private readonly List<IBook> _books;

        public void AddBook(IBook book)
        {
            _books.Add(book);
        }

        public void RemoveBook(IBook book)
        {
            _books.Remove(book);
        }

        public Library(List<IBook> books)
        {
            _books = books;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            BookSorterContext sorterContext = new BookSorterContext(new SortByDateDescendingStrategy());
            IBookDisplay bookDisplay = new BookDisplay();

            IBook book1 = new Book("Silent Dan", "Ozon671", new DateOnly(2022, 05, 10));
            IBook book2 = new Book("Onegin", "Canon", new DateOnly(1756, 10, 16));
            IBook book3 = new Book("Sad Vovan", "Alen", new DateOnly(2025, 06, 6));
            IBook book4 = new Book("Hero of our time", "Lermontov", new DateOnly(1924, 10, 27));
            IBook book5 = new Book("8000 oborotov", "Ozon671", new DateOnly(2023, 07, 10));

            ILibrary library = new Library(new List<IBook> { book1, book2, book3, book4, book5 });

            bookDisplay.DisplayBooks(library.Books);
            bookDisplay.DisplayBooks(sorterContext.SortBooks(library.Books));
        }
    }
}