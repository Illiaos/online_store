using System.Linq;

namespace Store.Memory
{
    public class BookRepository : IBookRepository
    {
        private readonly Book[] books = new[]
        {
            new Book(1, "ISBN 12312-31231", "D. Knuth", "Art Of Programming"),
            new Book(2, "ISBN 12312-31232", "M. Fowler", "Refactoring"),
            new Book(3, "ISBN 12312-31233", "D. Kernighan", "C Programming Language"),
        };

        public Book[] GetAllByISBN(string ISBN)
        {
            return books.Where(book => book.ISBN == ISBN).ToArray();
        }

        public Book[] GetAllByTitleOrAuthor(string titleOrAuthor)
        {
            return books.Where(book => book.Title.Contains(titleOrAuthor)
                                       || book.Author.Contains(titleOrAuthor)).ToArray();
        }
    }
}
