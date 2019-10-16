using JoanBookStoreApi.Controllers;
using JoanBookStoreApi.Model;
using JoanBookStoreApi.Services;
using Moq;
using NUnit.Framework;
using System;

namespace JoanBookStoreApiTest.Controllers
{
    [TestFixture]
    public class BooksControllerTests
    {
        private MockRepository mockRepository;

        private Mock<BookService> mockBookService;

        [SetUp]
        public void SetUp()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockBookService = this.mockRepository.Create<BookService>();
        }

        [TearDown]
        public void TearDown()
        {
            this.mockRepository.VerifyAll();
        }

        private BooksController CreateBooksController()
        {
            return new BooksController(
                this.mockBookService.Object);
        }

        [Test]
        public void Get_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var booksController = this.CreateBooksController();
            string isbn = "9780062963664";

            // Act
            var result = booksController.Get(
                isbn);

            Assert.IsNotNull(result);
            
        }

        [Test]
        public void Create_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var booksController = this.CreateBooksController();
            Book book = new Book();
            book.Id = "0000";
            book.ISBN = 0;
            book.Pages = 5;
            book.Price = "0";
            book.Title = "Book Test";

            // Act
            var result = booksController.Create(
                book);


            Assert.IsNotNull(result);

        }

        [Test]
        public void Update_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var booksController = this.CreateBooksController();
            long isbn = 0;
            Book book = new Book();
            book.Id = "0000";
            book.ISBN = 0;
            book.Pages = 5;
            book.Price = "0";
            book.Title = "Book Test";


            // Act
            var result = booksController.Update(
                isbn,
                book);

            // Assert
            Assert.IsNotNull(result);

        }

        [Test]
        public void Path_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var booksController = this.CreateBooksController();
            long isbn = 0;
            Book book = new Book();
            book.Id = "0000";
            book.ISBN = 0;
            book.Pages = 5;
            book.Price = "0";
            book.Title = "Book Test";


            // Act
            var result = booksController.Path(
                isbn,
                book);

            // Assert
            Assert.IsNotNull(result);

        }

        [Test]
        public void Delete_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var booksController = this.CreateBooksController();
            string isbn = "0";

            // Act
            var result = booksController.Delete(
                isbn);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
