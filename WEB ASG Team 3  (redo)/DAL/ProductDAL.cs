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
    public class ProductDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        //Constructor
        public ProductDAL()
        {
            //Read ConnectionString from appsettings.json file
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString(
            "ZZFashionCRMConnectionString");
            //Instantiate a SqlConnection object with the
            //Connection String read.
            conn = new SqlConnection(strConn);
        }
        public List<Product> GetAllProduct()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement
            cmd.CommandText = @"SELECT * FROM Product ORDER BY ProductID";
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            //Read all records until the end, save data into a staff list
            List<Product> productList = new List<Product>();
            while (reader.Read())
            {
                productList.Add(
                new Product
                {
                    ProductId = reader.GetInt32(0), //0: 1st column
                    ProductTitle = reader.GetString(1), //1: 2nd column
                                                        //Get the first character of a string
                    ProductImage = reader.IsDBNull(2) ?
                    reader.GetString(2) : string.Empty, //2: 3rd column
                    Price = reader.GetDouble(3), //3: 4th column
                    EffectiveDate = reader.GetDateTime(5), //5: 6th column
                    Obsolete = reader.GetString(6), //6: 7th column
                }
                );
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return productList;
        }
    }
}

