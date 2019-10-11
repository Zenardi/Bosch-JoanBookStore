using JoanBookStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JoanBookStore
{
    public class BookManifestOperations
    {
        public static List<BookManifest> SumQuantityWithSameISBN(List<BookManifest> bookManifests)
        {
            List<BookManifest> bookManifestsConsolidated = new List<BookManifest>();

  
            return bookManifestsConsolidated;
        }

        public static bool IsIsbnExistInList(List<BookManifest> bookManifests, long Isbn)
        {
            var r = bookManifests.FindAll(x => x.ISBN == Isbn);

            if (r.Count > 0)
                return true;
            else
                return false;
        }

        public static void CreateOrUpdateEntry(List<BookManifest> booksManifests, BookManifest bookManifest)
        {
            //int convertedISBN = 0;
            //int.TryParse(bookManifest.ISBN, out convertedISBN);

            if (!BookManifestOperations.IsIsbnExistInList(booksManifests, bookManifest.ISBN))
            {
                booksManifests.Add(bookManifest);
            }
            else
            {
                var dict = booksManifests.ToDictionary(x => x.ISBN);
                BookManifest found;
                if (dict.TryGetValue(bookManifest.ISBN, out found)) found.Quantity += bookManifest.Quantity;

            }
        }
    }
}
