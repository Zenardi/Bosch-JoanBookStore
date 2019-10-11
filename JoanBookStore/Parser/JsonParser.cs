using JoanBookStore.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;

namespace JoanBookStore.Parser
{
    public class JsonParser
    {
        public static List<BookManifest> JsonToBookManifest(String filePath, List<BookManifest> bookManifests)
        {
            String content = "";
            using(StreamReader streamReader = new StreamReader(filePath))
            {
                content = streamReader.ReadToEnd();
            }
            content = content.Replace("Age Range", "AgeRange");
            content = content.Replace("Qty.", "Quantity");

            List<BookManifest> auxBookManifests = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BookManifest>>(content);

            foreach (var item in auxBookManifests)
            {
                if(BookManifestOperations.IsIsbnExistInList(bookManifests, item.ISBN))
                {
                    //int convertedISBN = 0;
                    //int.TryParse(item.ISBN, out convertedISBN);
                    //BookManifestOperations.CreateOrUpdateEntry(booksManifests, bookManifest);
                    var dict = bookManifests.ToDictionary(x => x.ISBN);
                    BookManifest found;
                    if (dict.TryGetValue(item.ISBN, out found)) found.Quantity += item.Quantity;
                }
                else
                {
                    bookManifests.Add(item);
                }
            }
            return bookManifests;
        }


        public static void ConsolidadeJsonFile(String filePath, List<BookManifest> bookManifests)
        {

            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter(filePath))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, bookManifests);
            }
        }

        public static string ToJsonObjectSingle(BookManifest bookManifest)
        {
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(BookManifest));
            MemoryStream msObj = new MemoryStream();
            js.WriteObject(msObj, bookManifest);
            msObj.Position = 0;
            StreamReader sr = new StreamReader(msObj);

            string json = sr.ReadToEnd();

            return json;
        }
    }
}
