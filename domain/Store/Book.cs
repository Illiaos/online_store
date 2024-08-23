using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Store
{
    public class Book
    {
        public int Id { get; }
        public string ISBN { get; }
        public string Author { get; }
        public string Title { get; }
        
        public Book(int id, string isbn, string author, string tite)
        {
            Id = id;
            ISBN = isbn;
            Author = author;
            Title = tite;
        }

        internal static bool IsISBN(string isbn)
        {
            if(isbn == null) return false;
            isbn = isbn.Replace(" ", "").Replace("-", "").ToUpper();
            if (isbn.Length == 0) return false;
            return Regex.IsMatch(isbn, "^ISBN\\d{10}(\\d{3})?$");
        }
    }
}
