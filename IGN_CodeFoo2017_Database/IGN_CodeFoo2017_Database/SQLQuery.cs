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
                        String query = string.Format("INSERT INTO thumbnails(id, url, size, width, height) VALUES ('{0}', '{1}','{2}', '{3}', '{4}')", i, thumbnail.url, thumbnail.size, thumbnail.width, thumbnail.height);

                        MySqlCommand cmd = new MySqlCommand(query, dbConnection);

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
                    String query = string.Format("INSERT INTO apidata(id, headline, state, slug, subheadline, publishdate, articletype) VALUES ('{0}', '{1}','{2}', '{3}', '{4}', '{5}', '{6}')",
    i, datum.metadata.headline, datum.metadata.state, datum.metadata.slug, datum.metadata.subHeadline, datum.metadata.publishDate, datum.metadata.articleType);

                    MySqlCommand cmd = new MySqlCommand(query, dbConnection);

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

                        String query = string.Format("INSERT INTO networks(id,network) VALUES ('{0}', '{1}')", i, network);

                        MySqlCommand cmd = new MySqlCommand(query, dbConnection);

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

                        String query = string.Format("INSERT INTO tags(id,tag) VALUES ('{0}', '{1}')", i, tag);

                        MySqlCommand cmd = new MySqlCommand(query, dbConnection);

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