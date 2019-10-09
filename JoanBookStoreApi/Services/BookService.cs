using JoanBookStoreApi.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JoanBookStoreApi.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _books;

        public BookService(IBookstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _books = database.GetCollection<Book>(settings.BooksCollectionName);
        }

        public List<Book> Get() =>
            _books.Find(book => true).ToList();

        public Book Get(string ISBN) =>
            _books.Find<Book>(book => book.ISBN == ISBN).FirstOrDefault();

        public Book Create(Book book)
        {
            _books.InsertOne(book);
            return book;
        }

        public void Update(string ISBN, Book bookIn) =>
            _books.ReplaceOne(book => book.ISBN == ISBN, bookIn);

        public void Remove(Book bookIn) =>
            _books.DeleteOne(book => book.ISBN == bookIn.ISBN);

        public void Remove(string ISBN) =>
            _books.DeleteOne(book => book.ISBN == ISBN);
    }
}
