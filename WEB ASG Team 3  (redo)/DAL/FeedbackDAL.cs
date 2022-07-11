using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient;
using WEB2022Apr_P02_T3.Models;

namespace WEB2022Apr_P02_T3.DAL
{
    public class FeedbackDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        //Constructor
        public FeedbackDAL()
        {
            //Read ConnectionString from appsettings.json file
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString(
            "ZZFashionConnectionString");
            //Instantiate a SqlConnection object with the
            //Connection String read.
            conn = new SqlConnection(strConn);
        }
        public List<Feedback> GetAllFeedback()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement
            cmd.CommandText = @"SELECT * FROM Feedback ORDER BY FeedbackID";
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            //Read all records until the end, save data into a staff list
            List<Feedback> feedbackList = new List<Feedback>();
            while (reader.Read())
            {
                feedbackList.Add(
                new Feedback
                {
                    FeedbackID = reader.GetInt32(0), //0: 1st column
                    DatePosted = reader.GetDateTime(2), //2: 3rd column
                    Title = reader.GetString(3), //3: 4th column
                    Text = reader.GetString(4)
                }
                );
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return feedbackList;
        }
    }
}
