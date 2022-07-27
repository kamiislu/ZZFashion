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
        public int ChangePassword(string pwd, string id)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an UPDATE SQL statement
            cmd.CommandText = @"UPDATE Customer SET MPassword=@password
                                WHERE MemberID = @customerID";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@customerID", id);
            cmd.Parameters.AddWithValue("@password", pwd);
            //Open a database connection
            conn.Open();
            //ExecuteNonQuery is used for UPDATE and DELETE
            int count = cmd.ExecuteNonQuery();
            //Close the database connection
            conn.Close();
            return count;
        }

        public List<Customer> GetAllCustomer()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement 
            cmd.CommandText = @"SELECT * FROM Customer ORDER BY MemberID";
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            //Read all records until the end, save data into a staff list
            List<Customer> customerList = new List<Customer>();
            while (reader.Read())
            {
                customerList.Add(
                new Customer
                {
                    MemberId = reader.GetString(0), //0: 1st column
                    MName = reader.GetString(1), //1: 2nd column
                                                 //Get the first character of a string
                    MGender = reader.GetString(2)[0],//2: 3rd column
                    MBirthDate = reader.GetDateTime(3), //3: 4th column
                    MAddress = !reader.IsDBNull(4) ? reader.GetString(4) : string.Empty, //4: 5th column
                    MCountry = reader.GetString(5), //5: 6th column
                    MTelNo = !reader.IsDBNull(6) ? reader.GetString(6) : string.Empty, //6: 7th column
                    MEmailAddr = !reader.IsDBNull(7) ? reader.GetString(7) : string.Empty, //7: 8th column
                    MPassword = reader.GetString(8), //8: 9th column
                }

                );

            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return customerList;
        }
        public int Add(Customer customer)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an INSERT SQL statement
            conn.Open();
            //Register Member
            cmd.CommandText = @"INSERT INTO Customer (MemberId, MName, MGender, MBirthDate, MAddress, MCountry, MTelNo, MEmailAddr, MPassword)
            VALUES(@memberid, @mname, @mgender, @mbirthdate, @maddress, @mcountry, @mtelno, @memailaddr, @mpassword)";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@memberid", customer.MemberId);
            cmd.Parameters.AddWithValue("@mname", customer.MName);
            cmd.Parameters.AddWithValue("@mgender", customer.MGender);
            cmd.Parameters.AddWithValue("@mbirthdate", customer.MBirthDate);

            if (string.IsNullOrEmpty(customer.MAddress))
                cmd.Parameters.AddWithValue("@maddress", DBNull.Value);

            else
                cmd.Parameters.AddWithValue("@maddress", customer.MAddress);

            cmd.Parameters.AddWithValue("@mcountry", customer.MCountry);

            if (string.IsNullOrEmpty(customer.MTelNo))
                cmd.Parameters.AddWithValue("@mtelno", DBNull.Value);

            else
                cmd.Parameters.AddWithValue("@mtelno", customer.MTelNo);

            if (string.IsNullOrEmpty(customer.MEmailAddr))
                cmd.Parameters.AddWithValue("@memailaddr", DBNull.Value);

            else
                cmd.Parameters.AddWithValue("@memailaddr", customer.MEmailAddr);


            cmd.Parameters.AddWithValue("@mpassword", customer.MPassword);


            int addcustomer = cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();

            return addcustomer;
        }

        public int Delete(string memberId)
        {
            //Instantiate a SqlCommand object, supply it with a DELETE SQL statement
            //to delete a staff record specified by a Member ID
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"DELETE FROM Customer 
                                WHERE MemberId = @selectMemberId";
            cmd.Parameters.AddWithValue("@selectMemberId", memberId);
            //Open a database connection
            conn.Open();
            int rowAffected = 0;
            //Execute the DELETE SQL to remove the staff record
            rowAffected += cmd.ExecuteNonQuery();
            //Close database connection
            conn.Close();


            //Return number of row of staff record updated or deleted
            return rowAffected;
        }

        public bool IsEmailExist(string memail, string memberId)
        {
            bool emailFound = false;
            //Create a SqlCommand object and specify the SQL statement 
            //to get a staff record with the email address to be validated
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT MemberId FROM Customer 
                             WHERE MEmailAddr= @selectedEmail";
            cmd.Parameters.AddWithValue("@selectedEmail", memail);
            //Open a database connection and execute the SQL statement
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            { //Records found
                while (reader.Read())
                {
                    if (reader.GetString(0) != memberId)
                        //The email address is used by another staff
                        emailFound = true;
                }
            }
            else
            { //No record
                emailFound = false; // The email address given does not exist
            }
            reader.Close();
            conn.Close();
            return emailFound;
        }

    }


}