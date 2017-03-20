using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IGN_CodeFoo2017_Database
{
    class APIReader
    {
        public static void ReadAPI()
        {
            //Console.WriteLine("Hello World");
            System.Net.WebClient client = new System.Net.WebClient();

            SQLQuery.IntializeDB();



            string content = client.DownloadString("http://ign-apis.herokuapp.com/articles?startIndex=30\u0026count=10");
            DataInfo database = new DataInfo();

            Newtonsoft.Json.JsonConvert.PopulateObject(content, database);


            SQLQuery.IntializeThumbnailsTable(database);
            SQLQuery.IntializeAPIDataTable(database);
            SQLQuery.IntializeNetworksTable(database);
            SQLQuery.IntializeTagsTable(database);

            Console.WriteLine(database.startIndex);
            foreach (var datum in database.data)
            {
                Console.WriteLine("Headline: " + datum.metadata.headline);
                foreach (var network in datum.metadata.networks)
                {
                    Console.WriteLine("Network: " + network);
                }
                Console.WriteLine("State: " + datum.metadata.state);
                Console.WriteLine("Slug: " + datum.metadata.slug);
                Console.WriteLine("SubHeadline: " + datum.metadata.subHeadline);
                Console.WriteLine("Publish Date: " + datum.metadata.publishDate);
                Console.WriteLine("Article Type: " + datum.metadata.articleType);
                foreach (var thumbnail in datum.thumbnails)
                {
                    Console.WriteLine("Thumbnails: " + thumbnail.url + " | " + thumbnail.size + " | " + thumbnail.width + " | " + thumbnail.height);
                    //Console.WriteLine(thumbnail.size);
                    //Console.WriteLine(thumbnail.width);
                    //Console.WriteLine(thumbnail.height);
                }
                Console.Write("Tags: ");
                foreach (var tag in datum.tags)
                {
                    Console.Write(tag + ", ");
                }

                Console.WriteLine("");
                Console.WriteLine();
            }
        }
    }
}
