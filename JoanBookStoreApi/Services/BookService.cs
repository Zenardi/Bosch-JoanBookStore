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

        public Book Get(string Id) =>
            _books.Find<Book>(book => book.Id == Id).FirstOrDefault();

        public Book GetByIsbn(string isbn) =>
            _books.Find<Book>(book => book.ISBN == isbn).FirstOrDefault();

        public Book Create(Book book)
        {
            _books.InsertOne(book);
            return book;
        }

        public void Update(string id, Book bookIn) =>
            _books.ReplaceOne(book => book.Id == id, bookIn);

        public void UpdateByIsbn(string isbn, Book bookIn) =>
           _books.ReplaceOne(book => book.ISBN == isbn, bookIn);


        public void Remove(Book bookIn) =>
            _books.DeleteOne(book => book.Id == bookIn.Id);

        public void Remove(string Id) =>
            _books.DeleteOne(book => book.Id == Id);
    }
}
