using JoanBookStoreApi.Model;
using MongoDB.Driver;
using NHibernate.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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
            _books.Find(book => true).ToList().OrderBy(o => o.Genre).OrderBy(o => o.Author.Split(' ')[1]).OrderBy(o => o.Author.Split(' ')[0]).ToList();

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


        public List<Book> Filter(string genre, string author, string title)
        {
            Expression<Func<Book, bool>> filter = null;

            if(!String.IsNullOrEmpty(author) && !String.IsNullOrEmpty(genre) && !String.IsNullOrEmpty(title)) //author, genre, title
                filter = x => x.Author.Contains(author) && x.Genre == genre && x.Title == title;
            if (!String.IsNullOrEmpty(author) && !String.IsNullOrEmpty(genre) && String.IsNullOrEmpty(title)) //author and genre 
                filter = x => x.Author.Contains(author) && x.Genre == genre;
            if (!String.IsNullOrEmpty(author) && String.IsNullOrEmpty(genre) && !String.IsNullOrEmpty(title)) //author and title
                filter = x => x.Author.Contains(author) && x.Title == title;
            if (String.IsNullOrEmpty(author) && !String.IsNullOrEmpty(genre) && !String.IsNullOrEmpty(title)) //genre and title
                filter = x => x.Genre.Contains(genre) && x.Title == title;
            if (!String.IsNullOrEmpty(author) && String.IsNullOrEmpty(genre) && String.IsNullOrEmpty(title)) //author
                filter = x => x.Author.Contains(author);
            if (String.IsNullOrEmpty(author) && !String.IsNullOrEmpty(genre) && String.IsNullOrEmpty(title)) //genre
                filter = x => x.Genre.Contains(genre);
            if (String.IsNullOrEmpty(author) && String.IsNullOrEmpty(genre) && !String.IsNullOrEmpty(title)) //title
                filter = x => x.Title.Contains(title);


            List<Book> items = _books.Find(filter).ToList();
            return items;
        }

        private FilterDefinitionBuilder<TDocument> CreateSubject<TDocument>()
        {
            return new FilterDefinitionBuilder<TDocument>();
        }
    }
}
