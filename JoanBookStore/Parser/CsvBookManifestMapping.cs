using JoanBookStore.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using TinyCsvParser.Mapping;

namespace JoanBookStore.Parser
{
    public class CsvBookManifestMapping : CsvMapping<BookManifest>
    {
        public CsvBookManifestMapping() : base()
        {
            MapProperty(0, x => x.Title);
            MapProperty(1, x => x.Description);
            MapProperty(2, x => x.ISBN);
            MapProperty(3, x => x.Author);
            MapProperty(4, x => x.Genre);
            MapProperty(5, x => x.Pages);
            MapProperty(6, x => x.AgeRange);
            MapProperty(7, x => x.Price);
            MapProperty(8, x => x.Quantity);


        }
    }
}
