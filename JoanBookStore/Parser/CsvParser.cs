
using JoanBookStore.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using TinyCsvParser;

namespace JoanBookStore.Parser
{
    public class CsvParser
    {
        /// <summary>
        /// From csv file, retrieve a bookmanifest to add to a list do bookmanifest later
        /// </summary>
        /// <param name="csvFilePath"></param>
        /// <returns></returns>
        public static List<BookManifest> CsvToManifestObject(String csvFilePath, List<BookManifest> booksManifests)
        {
            BookManifest bookManifest = new BookManifest();
            
            CsvParserOptions csvParserOptions = new CsvParserOptions(true, ',');
            CsvReaderOptions csvReaderOptions = new CsvReaderOptions(new[] { Environment.NewLine });
            CsvBookManifestMapping csvMapper = new CsvBookManifestMapping();
            CsvParser<BookManifest> csvParser = new CsvParser<BookManifest>(csvParserOptions, csvMapper);
            var result = csvParser.ReadFromFile(csvFilePath, Encoding.ASCII).ToList();

            foreach (var item in result)
            {
                bookManifest = new BookManifest();

                bookManifest.Title = item.Result.Title;
                bookManifest.Description = item.Result.Description;
                bookManifest.ISBN = item.Result.ISBN;
                bookManifest.Author = item.Result.Author;
                bookManifest.Genre = item.Result.Genre;
                bookManifest.Pages = item.Result.Pages;
                bookManifest.AgeRange = item.Result.AgeRange;
                bookManifest.Price = item.Result.Price;
                bookManifest.Quantity = item.Result.Quantity;

                BookManifestOperations.CreateOrUpdateEntry(booksManifests, bookManifest);

            }

            return booksManifests;
        }

        /// <summary>
        /// Convert list of book manifests to unified format for file write
        /// </summary>
        /// <param name="bookManifests"></param>
        /// <returns></returns>
        public String ListBooksManifestToCsv(List<BookManifest> bookManifests)
        {
            return null;
        }

    }
}
