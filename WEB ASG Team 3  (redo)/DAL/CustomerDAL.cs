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
    public class CustomerDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        public CustomerDAL()
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
        public Customer GetDetails(string custID)
        {
            Customer customer = new Customer();
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement that
            //retrieves all attributes of a staff record.
            cmd.CommandText = @"SELECT * FROM Customer
                                    WHERE MemberID = @customerID";
            //Define the parameter used in SQL statement, value for the
            //parameter is retrieved from the method parameter “staffId”.
            cmd.Parameters.AddWithValue("@customerID", custID);
            //Open a database connection
            conn.Open();
            //Execute SELCT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                //Read the record from database
                while (reader.Read())
                {
                    // Fill staff object with values from the data reader
                    customer.MemberId = custID;
                    customer.MName = !reader.IsDBNull(1) ? reader.GetString(1) : null;
                    // (char) 0 - ASCII Code 0 - null value
                    customer.MGender = !reader.IsDBNull(2) ?
                    reader.GetString(2)[0] : (char)0;
                    customer.MBirthDate = !reader.IsDBNull(3) ?
                    reader.GetDateTime(3) : (DateTime?)null;
                    customer.MAddress = !reader.IsDBNull(4) ?
                    reader.GetString(4) : null;
                    customer.MCountry = !reader.IsDBNull(5) ?
                    reader.GetString(5) : null;
                    customer.MTelNo = !reader.IsDBNull(6) ?
                    reader.GetString(6) : null;
                    customer.MEmailAddr = !reader.IsDBNull(7) ?
                    reader.GetString(7) : null;
                    customer.MPassword = !reader.IsDBNull(8) ?
                    reader.GetString(8) : null;
                }
            }
            //Close data reader
            reader.Close();
            //Close database connection
            conn.Close();
            return customer;
        }
        public bool ValidatePassword(string username, string pwd)
        {
            Customer customer = new Customer();
            bool validation = false;
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement that
            //retrieves all attributes of a staff record.
            cmd.CommandText = @"SELECT * FROM Customer
                                    WHERE MemberID = @username";
            //Define the parameter used in SQL statement, value for the
            //parameter is retrieved from the method parameter “staffId”.
            cmd.Parameters.AddWithValue("@username", username);
            //Open a database connection
            conn.Open();
            //Execute SELCT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            { //Records found
                while (reader.Read())
                {
                    if (reader.GetString(8) == pwd)
                        //The email address is used by another staff
                        validation = true;
                    else
                    {
                        validation = false;
                    }
                }
            }
            else
            { //No record
                validation = false; // The email address given does not exist
            }
            reader.Close();
            conn.Close();
            return validation;
        }
        public int ChangePassword(Customer cust)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an UPDATE SQL statement
            cmd.CommandText = @"UPDATE Customer SET MPassword=@password
                                WHERE MemberID = @customerID";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@password", cust.MPassword);
            cmd.Parameters.AddWithValue("@customerID", cust.MemberId);
            //Open a database connection
            conn.Open();
            //ExecuteNonQuery is used for UPDATE and DELETE
            int count = cmd.ExecuteNonQuery();
            //Close the database connection
            conn.Close();
            return count;
        }
    }
}
