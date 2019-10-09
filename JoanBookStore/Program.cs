using JoanBookStore.Domain;
using JoanBookStore.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TinyCsvParser;
using TinyCsvParser.Mapping;

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
            JsonParser.ConsolidadeJsonFile(filesPath + @"ConsolidatedBooks.json", SortedList);
           
        }

    }
}
