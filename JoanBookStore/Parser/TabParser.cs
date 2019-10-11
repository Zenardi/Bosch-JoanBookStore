using JoanBookStore.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace JoanBookStore.Parser
{
    public class TabParser
    {
        public static List<BookManifest> TabDelimitedToBookManifest(String filePath, List<BookManifest> booksManifests)
        {
            BookManifest bookManifest = new BookManifest();

            DataTable datatable = new DataTable();
            StreamReader streamreader = new StreamReader(filePath);
            char[] delimiter = new char[] { '\t' };
            string[] columnheaders = streamreader.ReadLine().Split(delimiter);
            foreach (string columnheader in columnheaders)
            {
                datatable.Columns.Add(columnheader); // I've added the column headers here.
            }

            while (streamreader.Peek() > 0)
            {
                DataRow datarow = datatable.NewRow();
                datarow.ItemArray = streamreader.ReadLine().Split(delimiter);
                datatable.Rows.Add(datarow);
            }

            foreach (DataRow row in datatable.Rows)
            {
                //Console.WriteLine(""----Row No: " + datatable.Rows.IndexOf(row) + "----"");

                bookManifest = new BookManifest();

                bookManifest.Title = row.ItemArray[0].ToString();
                bookManifest.Description = row.ItemArray[1].ToString();
                bookManifest.ISBN = Convert.ToInt64(row.ItemArray[2].ToString());
                bookManifest.Author = row.ItemArray[3].ToString();
                bookManifest.Genre = row.ItemArray[4].ToString();
                bookManifest.Pages = Convert.ToInt32(row.ItemArray[5].ToString());
                bookManifest.AgeRange = row.ItemArray[6].ToString();
                bookManifest.Price = row.ItemArray[7].ToString();
                bookManifest.Quantity = Convert.ToInt32(row.ItemArray[8].ToString());


                BookManifestOperations.CreateOrUpdateEntry(booksManifests, bookManifest);

                //booksManifests.Add(bookManifest);
            }

            return booksManifests;
        }
    }
}
