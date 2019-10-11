using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JoanBookStoreApi.Model
{
    /// <summary>
    /// </summary>
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public String Title { get; set; }

        public String Description { get; set; }

        public long ISBN { get; set; }

        public String Author { get; set; }

        public String Genre { get; set; }

        public int Pages { get; set; }

        public String AgeRange { get; set; }

        public String Price { get; set; }

        public int Quantity { get; set; }
    }
}
