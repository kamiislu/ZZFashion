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
    public class StaffDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        public List<Staff> GetAllStaff()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement
            cmd.CommandText = @"SELECT * FROM Staff";
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            List<Staff> staffList = new List<Staff>();
            while (reader.Read())
            {
                staffList.Add(
                    new Staff
                    {
                        StaffID = reader.GetString(0),
                        StoreID = !reader.IsDBNull(1)? reader.GetString(1): null,
                        SName = reader.GetString(2),
                        SGender = reader.GetChar(3),
                        SAppt = reader.GetString(4),
                        STelNo = reader.GetString(5),
                        SEmailAddr = reader.GetString(6),
                        SPassword = reader.GetString(7)
                    });
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return staffList;
        }
    }
}
