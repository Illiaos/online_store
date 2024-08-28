using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public interface IBookRepository
    {
        Book[] GetAllByISBN(string ISBN);
        Book[] GetAllByTitleOrAuthor(string titleOrAuthor);
        Book GetById(int id);
        Book[] GetAllByIds(int[] ids);
    }
}
