using JoanBookStore.Domain;
using JoanBookStore.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TinyCsvParser;
using TinyCsvParser.Mapping;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;


namespace JoanBookStore
{
    class Program
    {
        static void Main(string[] args)
        {
            String filesPath = args[0];
            List<BookManifest> bookManifests = new List<BookManifest>();

            string[] tabFiles = System.IO.Directory.GetFiles(filesPath, "*.txt");
            string[] csvFiles = System.IO.Directory.GetFiles(filesPath, "*.csv");
            string[] jsonFiles = System.IO.Directory.GetFiles(filesPath, "*.json");


            foreach (var item in tabFiles)
            {
                TabParser.TabDelimitedToBookManifest(item, bookManifests);
            }

            foreach (var item in csvFiles)
            {
                CsvParser.CsvToManifestObject(item, bookManifests);
            }

            foreach (var item in jsonFiles)
            {
                JsonParser.JsonToBookManifest(filesPath + @"\books.json", bookManifests);
            }

            //Sort by Alphabetical in Ascending Order by Genre, Last Name, First Name
            List<BookManifest> SortedList = bookManifests.OrderBy(o => o.Genre).OrderBy(o => o.Author.Split(' ')[1]).OrderBy(o => o.Author.Split(' ')[0]).ToList();

            ///serialize and write file
            ///
            JsonParser.ConsolidadeJsonFile(filesPath + @"\ConsolidatedBooks.json", SortedList);



            ///INSERT TO MONGODB
            String json = String.Empty;
            List<BsonDocument> documents = new List<BsonDocument>();
            for (int i = 0; i < SortedList.Count; i++)
            {
                String j = JsonParser.ToJsonObjectSingle(SortedList.ElementAt(i));
                documents.Add(BsonSerializer.Deserialize<BsonDocument>(j));
            }

            InsertToMongDb(documents);

        }

        public static void InsertToMongDb(List<BsonDocument> documents)
        {
            /// Conntection String
            var conString = "mongodb://localhost:27017";

            /// creating MongoClient
            var client = new MongoClient(conString);

            ///Get the database
            var DB = client.GetDatabase("BookstoreDb");

            ///Get the collcetion from the DB in which you want to insert the data
            ///
            var collection = DB.GetCollection<BsonDocument>("Books");

            /// initializes BSONDocument with the data you want to insert
            //MongoDB.Bson.BsonDocument document = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(Json);

            /// use InsertOne menthod to insert one record at a time
            collection.InsertMany(documents);
        }

    }
}
