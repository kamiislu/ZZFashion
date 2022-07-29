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
    public class IssueVoucherDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        //Constructor
        public IssueVoucherDAL()
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
        public int Create(IssueVoucher voucher)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO CashVoucher (MemberID,Amount, MonthIssuedFor, YearIssuedFor, 
DateTimeIssued)
OUTPUT INSERTED.IssuingID
VALUES(@memberID, @amount, @monthIssuedFor, @yearIssuedFor, @dateTimeIssued)";

            cmd.Parameters.AddWithValue("@memberID", voucher.MemberID);
            cmd.Parameters.AddWithValue("@amount", voucher.Amount);
            cmd.Parameters.AddWithValue("@monthIssuedFor", voucher.MonthIssuedFor);
            cmd.Parameters.AddWithValue("@yearIssuedFor", voucher.YearIssuedFor);
            cmd.Parameters.AddWithValue("@dateTimeIssued", voucher.DateTimeIssued);

            conn.Open();
            voucher.IssuingID = (int)cmd.ExecuteScalar();
            conn.Close();

            return voucher.IssuingID;
        }
    }
}
