using Xunit;

namespace Store.Tests
{
    public class BookTest
    {
        [Fact]
        public void IsIsbn_WithNull_ReturnFalse()
        {
            bool actual = Book.IsISBN(null);
            Assert.False(actual);
        }

        [Fact]
        public void IsIsbn_WithBlankString_RefurnFalse()
        {
            bool actual = Book.IsISBN("    ");
            Assert.False(actual);
        }

        [Fact]
        public void IsIsbn_WithInvalidString_RefurnFalse()
        {
            bool actual = Book.IsISBN("ISBN 123");
            Assert.False(actual);
        }

        [Fact]
        public void IsIsbn_WithValidString_RefurnFalse()
        {
            bool actual = Book.IsISBN("ISBN 12312-31231");
            Assert.True(actual);
        }

        [Fact]
        public void IsIsbn_StartWrong_ReturnFalse()
        {
            bool actual = Book.IsISBN("xxx Isbn 12312-31231 yyy");
            Assert.False(actual);
        }
    }
}