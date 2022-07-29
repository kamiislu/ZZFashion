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
    public class SalesTransactionDAL
    {
        private IConfiguration Configuration { get; set; }
        private SqlConnection conn;

        public SalesTransactionDAL()
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

        public List<SalesTransaction> GetAllTransactions()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement
            cmd.CommandText = @"SELECT * FROM SalesTransaction";
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            List<SalesTransaction> salesTransactionList = new List<SalesTransaction>();
            while (reader.Read())
            {
                salesTransactionList.Add(
                    new SalesTransaction
                    {
                        transactionID = reader.GetInt32(0),
                        storeID = reader.GetString(1),
                        memberID = !reader.IsDBNull(2)? reader.GetString(2):null,
                        subtotal = reader.GetDecimal(3),
                        Tax = reader.GetDecimal(4),
                        discountPercent = reader.GetDouble(5),
                        discountAmount = reader.GetDecimal(6),
                        total = reader.GetDecimal(7),
                        dateCreated = reader.GetDateTime(8)
                    });
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return salesTransactionList;
        }

        public List<Customer> GetAllCustomers()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement
            cmd.CommandText = @"SELECT * FROM Customer";
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            List<Customer> customerList = new List<Customer>();
            while (reader.Read())
            {
                customerList.Add(
                    new Customer
                    {
                        MemberId = reader.GetString(0),
                        MName = reader.GetString(1),
                        MGender = reader.GetString(2)[0],
                        MBirthDate = reader.GetDateTime(3),
                        MAddress = !reader.IsDBNull(4)? reader.GetString(4):null,
                        MCountry = reader.GetString(5),
                        MTelNo = !reader.IsDBNull(6)? reader.GetString(6):null,
                        MEmailAddr = !reader.IsDBNull(7)? reader.GetString(7):null,
                        MPassword = reader.GetString(8)
                    });
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return customerList;
        }

        public List<SalesTransaction> GetCustomerSalesTransaction(string memberId)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SQL statement that select all branches
            cmd.CommandText = @"SELECT * FROM SalesTransaction WHERE MemberID = @selectedCustomer ORDER BY SubTotal DESC";
            //Define the parameter used in SQL statement, value for the
            //parameter is retrieved from the method parameter .
            cmd.Parameters.AddWithValue("@selectedCustomer", memberId);
            //Open a database connection
            conn.Open();
            //Execute SELCT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            List<SalesTransaction> salesTransactionsList = new List<SalesTransaction>();
            while (reader.Read())
            {
                salesTransactionsList.Add(
                    new SalesTransaction
                    {
                        transactionID = reader.GetInt32(0),
                        storeID = reader.GetString(1),
                        memberID = memberId,
                        subtotal = reader.GetDecimal(3),
                        Tax = reader.GetDecimal(4),
                        discountPercent = reader.GetDouble(5),
                        discountAmount = reader.GetDecimal(6),
                        total = reader.GetDecimal(7),
                        dateCreated = reader.GetDateTime(8)
                    });
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return salesTransactionsList;
        }
        public List<Customer> GetAllCustomersRanked()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement
            cmd.CommandText = @"SELECT Customer.*, SalesTransaction.* FROM Customer INNER JOIN SalesTransaction ON Customer.MemberID= SalesTransaction.MemberID WHERE (((DATEPART(MONTH,GETDATE())-1) = Month(SalesTransaction.DateCreated))AND(DATEPART(YEAR,GETDATE()) = YEAR(SalesTransaction.DateCreated))) ORDER BY Total DESC  ";
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            List<Customer> customerList = new List<Customer>();
            while (reader.Read())
            {
                customerList.Add(
                    new Customer
                    {
                        MemberId = !reader.IsDBNull(0)? reader.GetString(0):null,
                        MName = reader.GetString(1),
                        MGender = reader.GetString(2)[0],
                        MBirthDate = reader.GetDateTime(3),
                        MAddress = !reader.IsDBNull(4) ? reader.GetString(4) : null,
                        MCountry = reader.GetString(5),
                        MTelNo = !reader.IsDBNull(6) ? reader.GetString(6) : null,
                        MEmailAddr = !reader.IsDBNull(7) ? reader.GetString(7) : null,
                        MPassword = reader.GetString(8)
                    });
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return customerList;
        }
        public List<SalesTransaction> GetCustomerRankedSalesTransaction(string memberId)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SQL statement that select all branches
            cmd.CommandText = @"SELECT Customer.*, SalesTransaction.* 
FROM Customer INNER JOIN SalesTransaction ON Customer.MemberID= SalesTransaction.MemberID 
WHERE (((DATEPART(MONTH,GETDATE())-1) = Month(SalesTransaction.DateCreated))AND(DATEPART(YEAR,GETDATE()) = YEAR(SalesTransaction.DateCreated))) 
ORDER BY Total DESC";
            //Define the parameter used in SQL statement, value for the
            //parameter is retrieved from the method parameter “branchNo”.
            cmd.Parameters.AddWithValue("@selectedCustomer", memberId);
            //Open a database connection
            conn.Open();
            //Execute SELCT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            List<SalesTransaction> salesTransactionsList = new List<SalesTransaction>();
            while (reader.Read())
            {
                salesTransactionsList.Add(
                    new SalesTransaction
                    {
                        transactionID = reader.GetInt32(9),
                        storeID = reader.GetString(10),
                        memberID = memberId,
                        subtotal = reader.GetDecimal(12),
                        Tax = reader.GetDecimal(13),
                        discountPercent = reader.GetDouble(14),
                        discountAmount = reader.GetDecimal(15),
                        total = reader.GetDecimal(16),
                        dateCreated = reader.GetDateTime(17)
                    });
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return salesTransactionsList;
        }
        public List<TotalAmountViewModel> GetCustomerByTotalAmount()
        {
            List<TotalAmountViewModel> customerByTransactionList = new List<TotalAmountViewModel>();
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SQL statement that select all branches
            cmd.CommandText = @"SELECT s.MemberID, c.MName,SUM(Total)AS ""MemberTotal"", s.DateCreated 
FROM Customer c INNER JOIN SalesTransaction s ON c.MemberID = s.MemberID
WHERE s.MemberID IS NOT NULL  AND(((DATEPART(MONTH, GETDATE()) - 1) =
Month(s.DateCreated))AND(DATEPART(YEAR, GETDATE()) = YEAR(s.DateCreated)))
GROUP BY s.MemberID, c.MName, s.DateCreated
ORDER BY MemberTotal DESC"; 

            //Open a database connection
            conn.Open();
            //Execute SELCT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                customerByTransactionList.Add(
                    new TotalAmountViewModel
                    {
                        MemberID = reader.GetString(0),
                        MName = reader.GetString(1),
                        TotalAmount = !reader.IsDBNull(2) ? reader.GetDecimal(2) : (decimal?)null,
                        DateCreated = reader.GetDateTime(3)
                    });
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return customerByTransactionList;
        }
    }
}
