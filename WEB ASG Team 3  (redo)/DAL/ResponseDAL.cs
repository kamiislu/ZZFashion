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
    public class ResponseDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        //Constructor
        public ResponseDAL()
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

        public int Create(Response response)
        {
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"INSERT INTO Response (FeedbackID, MemberID,
StaffID, DateTimePosted, Text)
OUTPUT INSERTED.ResponseID
VALUES(@feedbackID, @memberID, @staffID, @dateTimePosted, @text)
        ";
            cmd.Parameters.AddWithValue("@feedbackID", response.FeedbackID);
            cmd.Parameters.AddWithValue("@MemberID", response.MemberID);
            cmd.Parameters.AddWithValue("@staffID", response.StaffID);
            cmd.Parameters.AddWithValue("@dateTimePosted", response.DatePosted);
            cmd.Parameters.AddWithValue("@text", response.Text);

            conn.Open();

            response.ResponseID = (int)cmd.ExecuteScalar();

            conn.Close();

            return response.ResponseID;
        }

        public List<Response> GetAllResponse()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement
            cmd.CommandText = @"SELECT * FROM Response ORDER BY DateTimePosted DESC";
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            List<Response> responseList = new List<Response>();
            while (reader.Read())
            {
                responseList.Add(
                    new Response
                    {
                        ResponseID = reader.GetInt32(0),
                        FeedbackID = reader.GetInt32(1),
                        MemberID = !reader.IsDBNull(2) ? reader.GetString(2) : null,
                        StaffID = !reader.IsDBNull(3) ? reader.GetString(3) : null,
                        DatePosted = reader.GetDateTime(4),
                        Text = reader.GetString(5)
                    });
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return responseList;
        }
    }
}
