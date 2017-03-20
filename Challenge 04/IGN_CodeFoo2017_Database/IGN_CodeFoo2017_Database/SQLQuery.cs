using Microsoft.Analytics.Interfaces;
using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace IGN_CodeFoo2017_Database
{
    public class SQLQuery
    {
        //database stuff 
        private const String SERVER = "127.0.0.1";
        private const String DATABASE = "ign_api_info";
        private const String UID = "root";
        private const String PASSWORD = "Jorden12!";
        private static MySqlConnection dbConnection;

        public static void IntializeDB()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = SERVER;
            builder.UserID = UID;
            builder.Password = PASSWORD;
            builder.Database = DATABASE;

            String connectionString = builder.ToString();

            builder = null;

            Console.WriteLine(connectionString);
            dbConnection = new MySqlConnection(connectionString);
            Application.ApplicationExit += (sender, args) =>
            {
                if (dbConnection != null)
                {
                    dbConnection.Dispose();
                    dbConnection = null;
                }
            };

            
        }

        public static void IntializeThumbnailsTable(DataInfo database)
        {
            dbConnection.Open();
            
            int i = 0;
            
            if(IsThumbnailsEmpty() == true)
            {
                foreach (var datum in database.data)
                {

                    foreach (var thumbnail in datum.thumbnails)
                    {
                        String query = "INSERT INTO thumbnails(id, url, size, width, height) VALUES (@id, @url,@size, @width, @height)";

                        MySqlCommand cmd = new MySqlCommand(query, dbConnection);

                        cmd.Parameters.AddWithValue("@id", i);
                        cmd.Parameters.AddWithValue("@url", thumbnail.url);
                        cmd.Parameters.AddWithValue("@size", thumbnail.size);
                        cmd.Parameters.AddWithValue("@width", thumbnail.width);
                        cmd.Parameters.AddWithValue("@height", thumbnail.height);

                        cmd.ExecuteNonQuery();

                    }
                    i++;
                }
            }
            
            dbConnection.Close();
        }

        public static void IntializeAPIDataTable(DataInfo database)
        {
            dbConnection.Open();

            int i = 0;

            if (IsAPI_DataEmpty() == true)
            {
                foreach (var datum in database.data)
                {
                    String query = "INSERT INTO apidata(id, headline, state, slug, subheadline, publishdate, articletype) VALUES (@id, @headline, @state, @slug, @subheadline, @publishdate, @articletype)";

                    MySqlCommand cmd = new MySqlCommand(query, dbConnection);

                    cmd.Parameters.AddWithValue("@id", i);
                    cmd.Parameters.AddWithValue("@headline", datum.metadata.headline);
                    cmd.Parameters.AddWithValue("@state", datum.metadata.state);
                    cmd.Parameters.AddWithValue("@slug", datum.metadata.slug);
                    cmd.Parameters.AddWithValue("@subheadline", datum.metadata.subHeadline);
                    cmd.Parameters.AddWithValue("@publishdate", datum.metadata.publishDate);
                    cmd.Parameters.AddWithValue("@articletype", datum.metadata.articleType);

                    cmd.ExecuteNonQuery();



                    i++;
                }
            }

            dbConnection.Close();
        }

        public static void IntializeNetworksTable(DataInfo database)
        {
            dbConnection.Open();

            int i = 0;

            if (IsNetworksEmpty() == true)
            {
                foreach (var datum in database.data)
                {
                    foreach (var network in datum.metadata.networks)
                    {

                        String query = "INSERT INTO networks(id,network) VALUES (@id, @network)";
                        
                        MySqlCommand cmd = new MySqlCommand(query, dbConnection);
                        cmd.Parameters.AddWithValue("@id", i);
                        cmd.Parameters.AddWithValue("@network", network);
                        cmd.ExecuteNonQuery();
                    }

                    i++;
                }
            }

            dbConnection.Close();
        }

        public static void IntializeTagsTable(DataInfo database)
        {
            dbConnection.Open();

            int i = 0;

            if (IsTagsEmpty() == true)
            {
                foreach (var datum in database.data)
                {
                    foreach (var tag in datum.tags)
                    {

                        String query = "INSERT INTO tags(id,tag) VALUES (@id, @tag)";

                        MySqlCommand cmd = new MySqlCommand(query, dbConnection);
                        cmd.Parameters.AddWithValue("@id", i);
                        cmd.Parameters.AddWithValue("@tag", tag);
                        cmd.ExecuteNonQuery();
                    }

                    i++;
                }
            }

            dbConnection.Close();
        }

        public static bool IsThumbnailsEmpty()
        {

            bool isEmpty;

            String query = "SELECT * FROM thumbnails";

            MySqlCommand cmd = new MySqlCommand(query, dbConnection);


            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                isEmpty = false;
            }
            else
                isEmpty = true;

            reader.Close();

            return isEmpty;
        }

        public static bool IsAPI_DataEmpty()
        {

            bool isEmpty;

            String query = "SELECT * FROM apiData";

            MySqlCommand cmd = new MySqlCommand(query, dbConnection);


            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                isEmpty = false;
            }
            else
                isEmpty = true;

            reader.Close();

            return isEmpty;
        }

        public static bool IsNetworksEmpty()
        {

            bool isEmpty;

            String query = "SELECT * FROM networks";

            MySqlCommand cmd = new MySqlCommand(query, dbConnection);


            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                isEmpty = false;
            }
            else
                isEmpty = true;

            reader.Close();

            return isEmpty;
        }

        public static bool IsTagsEmpty()
        {

            bool isEmpty;

            String query = "SELECT * FROM tags";

            MySqlCommand cmd = new MySqlCommand(query, dbConnection);


            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                isEmpty = false;
            }
            else
                isEmpty = true;

            reader.Close();

            return isEmpty;
        }
    }
}