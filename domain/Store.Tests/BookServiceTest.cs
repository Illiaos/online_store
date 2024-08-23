using Moq;
using Xunit;

namespace Store.Tests
{
    public class BookServiceTest
    {
        [Fact]
        public void GetAllByQuery_WithIsbn_CallsGetAllByIsbn()
        {
            var bookRespositoryStub = new Mock<IBookRepository>();
            bookRespositoryStub.Setup(x => x.GetAllByISBN(It.IsAny<string>())).Returns(new[]
            {
                new Book(1, "", "", "")
            });

            bookRespositoryStub.Setup(x => x.GetAllByTitleOrAuthor(It.IsAny<string>())).Returns(new[]
            {
                new Book(2, "", "", "")
            });

            var bookService = new BookService(bookRespositoryStub.Object);

            var actual = bookService.GetAllByQuery("ISBN 12345-67890");
            Assert.Collection(actual, item => Assert.Equal(1, item.Id));
        }

        [Fact]
        public void GetAllByQuery_WithIsbn_CallsGetAllByTitleAuthor()
        {
            var bookRespositoryStub = new Mock<IBookRepository>();
            bookRespositoryStub.Setup(x => x.GetAllByISBN(It.IsAny<string>())).Returns(new[]
            {
                new Book(1, "", "", "")
            });

            bookRespositoryStub.Setup(x => x.GetAllByTitleOrAuthor(It.IsAny<string>())).Returns(new[]
            {
                new Book(2, "", "", "")
            });

            var bookService = new BookService(bookRespositoryStub.Object);

            var actual = bookService.GetAllByQuery("12345-67890");
            Assert.Collection(actual, item => Assert.Equal(2, item.Id));
        }
    }
}
