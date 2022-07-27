using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.IO;
using WEB2022Apr_P02_T3.Models;

namespace WEB2022Apr_P02_T3.DAL
{
    public class VoucherDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        public VoucherDAL()
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

        public CashVoucher GetDetails(int voucherID)
        {
            CashVoucher cashvoucher = new CashVoucher();
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement that
            //retrieves all attributes of a staff record.
            cmd.CommandText = @"SELECT * FROM CashVoucher
                                    WHERE IssuingID = @voucherID";
            //Define the parameter used in SQL statement, value for the
            //parameter is retrieved from the method parameter “staffId”.
            cmd.Parameters.AddWithValue("@voucherID", voucherID);
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
                    cashvoucher.IssuingID = voucherID;
                    cashvoucher.MemberId = reader.GetString(1);
                    cashvoucher.Amount = !reader.IsDBNull(2) ?
                    reader.GetDecimal(2) : (Decimal)0.00;
                    cashvoucher.MonthIssuedFor = reader.GetInt32(3);
                    cashvoucher.YearIssuedFor = reader.GetInt32(4);
                    cashvoucher.DateTimeIssued = reader.GetDateTime(5);
                    cashvoucher.VoucherSN = !reader.IsDBNull(6) ?
                    reader.GetString(6) : null;
                    // (char) 0 - ASCII Code 0 - null value
                    cashvoucher.Status = !reader.IsDBNull(7) ?
                    reader.GetString(7)[0] : (char)0;
                    cashvoucher.DateTimeRedeemed = !reader.IsDBNull(8) ?
                    reader.GetDateTime(8) : (DateTime?)null;
                }
            }
            //Close data reader
            reader.Close();
            //Close database connection
            conn.Close();
            return cashvoucher;
        }

        public List<CashVoucher> GetAllCashVoucher()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement 
            cmd.CommandText = @"SELECT * FROM CashVoucher ORDER BY IssuingID";
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            //Read all records until the end, save data into a staff list
            List<CashVoucher> cashVoucherList = new List<CashVoucher>();
            CashVoucher cashvoucher = new CashVoucher();
            while (reader.Read())
            {

                cashVoucherList.Add(                  
                    new CashVoucher
                    {
                        IssuingID = reader.GetInt32(0),
                        MemberId = reader.GetString(1),
                        Amount = reader.GetDecimal(2),
                        MonthIssuedFor = reader.GetInt32(3),
                        YearIssuedFor = reader.GetInt32(4),
                        DateTimeIssued = reader.GetDateTime(5),
                        VoucherSN = !reader.IsDBNull(6) ? reader.GetString(6) : string.Empty,
                        // (char) 0 - ASCII Code 0 - null value
                        Status = reader.GetString(7)[0],
                        DateTimeRedeemed = !reader.IsDBNull(8) ? reader.GetDateTime(8) : (DateTime?)null,
                   
                    }
                    
                );

            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return cashVoucherList;
        }
        // Return number of row updated
        public int Collect(CashVoucher cashvoucher)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an UPDATE SQL statement

            cmd.CommandText = @"UPDATE CashVoucher SET Status = (Status + 1),
                        VoucherSN =@voucherSN,
                        WHERE IssuingID = @selectedIssuingID";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            
            cmd.Parameters.AddWithValue("Status", cashvoucher.Status);
            cmd.Parameters.AddWithValue("@voucherSN", cashvoucher.VoucherSN);
            cmd.Parameters.AddWithValue("@selectedIssuingID", cashvoucher.IssuingID);
            
            //Open a database connection
            conn.Open();
            //ExecuteNonQuery is used for UPDATE and DELETE
            int count = cmd.ExecuteNonQuery();
            //Close the database connection
            conn.Close();
            return count;
        }

        public int Redeem(CashVoucher cashvoucher)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an UPDATE SQL statement

            cmd.CommandText = @"UPDATE CashVoucher SET Status = (Status + 1),
                        VoucherSN =@voucherSN,
                        DateTimeRedeemed = getdate()
                        WHERE IssuingID = @selectedIssuingID";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.

            cmd.Parameters.AddWithValue("Status", cashvoucher.Status);
            cmd.Parameters.AddWithValue("@voucherSN", cashvoucher.VoucherSN);
            cmd.Parameters.AddWithValue("DateTimeRedeemed", cashvoucher.DateTimeRedeemed);
            cmd.Parameters.AddWithValue("@selectedIssuingID", cashvoucher.IssuingID);

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

