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
            "ZZFashionConnectionString");
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
                    ProductImage = !reader.IsDBNull(2) ?
                    reader.GetString(2) : string.Empty, //2: 3rd column
                    Price = reader.GetDecimal(3), //3: 4th column
                    EffectiveDate = reader.GetDateTime(4), //5: 6th column
                    Obsolete = reader.GetString(5), //6: 7th column
                }
                );
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return productList;
        }

        public int Add(Product product)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an INSERT SQL statement which will
            //return the auto-generated StaffID after insertion
            cmd.CommandText = @"INSERT INTO Product (ProductTitle, ProductImage, Price, EffectiveDate, Obsolete)
OUTPUT INSERTED.ProductID
VALUES(@ProductTitle, @ProductImage, @Price,
@EffectiveDate, @Obsolete)";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@ProductTitle", product.ProductTitle);
            if (product.ProductImage != null)
                // A branch is assigned
                cmd.Parameters.AddWithValue("@ProductImage", product.ProductImage);
            else // No branch is assigned
                cmd.Parameters.AddWithValue("@ProductImage", DBNull.Value);
            cmd.Parameters.AddWithValue("@Price", product.Price);
            cmd.Parameters.AddWithValue("@EffectiveDate", product.EffectiveDate);
            cmd.Parameters.AddWithValue("@Obsolete", product.Obsolete);

            //A connection to database must be opened before any operations made.
            conn.Open();
            //ExecuteScalar is used to retrieve the auto-generated
            //StaffID after executing the INSERT SQL statement
            product.ProductId = (int)cmd.ExecuteScalar();
            //A connection should be closed after operations.
            conn.Close();
            //Return id when no error occurs.
            return product.ProductId;
        }

        public Product GetDetails(int productId)
        {
            Product product = new Product();
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement that
            //retrieves all attributes of a product record.
            cmd.CommandText = @"SELECT * FROM Product
 WHERE ProductID = @selectedProductID";
            //Define the parameter used in SQL statement, value for the
            //parameter is retrieved from the method parameter “productId”.
            cmd.Parameters.AddWithValue("@selectedProductID", productId);
            //Open a database connection
            conn.Open();
            //Execute SELCT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                //Read the record from database
                while (reader.Read())
                {
                    // Fill product object with values from the data reader
                    product.ProductId = productId;
                    product.ProductTitle = reader.GetString(1);
                    product.ProductImage = !reader.IsDBNull(2) ? reader.GetString(2) : null;
                    product.Price = !reader.IsDBNull(3) ? reader.GetDecimal(3) : (Decimal)0.00;
                    product.EffectiveDate = reader.GetDateTime(4);
                    product.Obsolete = reader.GetString(5);
                }
            }
            //Close data reader
            reader.Close();
            //Close database connection
            conn.Close();
            return product;
        }

        // Return number of row updated
        public int Update(Product product)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an UPDATE SQL statement
            cmd.CommandText = @"UPDATE Product SET ProductTitle=@ProductTitle,
 ProductImage=@ProductImage, Price=@Price, Obsolete=@Obsolete
WHERE ProductID = @selectedProductID";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@selectedProductID", product.ProductId);
            cmd.Parameters.AddWithValue("@ProductTitle", product.ProductTitle);
            if (product.ProductImage != null)
                // A image is assigned
                cmd.Parameters.AddWithValue("@ProductImage", product.ProductImage);
            else // No image is assigned
                cmd.Parameters.AddWithValue("@ProductImage", DBNull.Value);
            cmd.Parameters.AddWithValue("@Price", product.Price);
            cmd.Parameters.AddWithValue("@EffectiveDate", product.EffectiveDate);
            cmd.Parameters.AddWithValue("@Obsolete", product.Obsolete);

            //A connection to database must be opened before any operations made.
            conn.Open();
            //ExecuteScalar is used to retrieve the auto-generated
            //StaffID after executing the INSERT SQL statement
            int count = cmd.ExecuteNonQuery();
            //A connection should be closed after operations.
            conn.Close();
            //Return id when no error occurs.
            return count;
        }

        public int Delete(int productId)
        {
            //Instantiate a SqlCommand object, supply it with a DELETE SQL statement
            //to delete a staff record specified by a Staff ID
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"DELETE FROM Product
 WHERE ProductID = @selectProductID";
            cmd.Parameters.AddWithValue("@selectProductID", productId);
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

        public List<Product> GetNewProduct()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement
            cmd.CommandText = @"SELECT * FROM Product WHERE DATEPART(Month, EffectiveDate) = DATEPART(Month,GETDATE())
            AND DATEPART(Year, EffectiveDate)= DATEPART(Year, GETDATE())";
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            //Read all records until the end, save data into a staff list
            List<Product> newProductList = new List<Product>();
            while (reader.Read())
            {
                newProductList.Add(
                new Product
                {
                    ProductId = reader.GetInt32(0), //0: 1st column
                    ProductTitle = reader.GetString(1), //1: 2nd column
                    ProductImage = !reader.IsDBNull(2) ?
                    reader.GetString(2) : string.Empty, //2: 3rd column
                    Price = reader.GetDecimal(3), //3: 4th column
                    EffectiveDate = reader.GetDateTime(4), //5: 6th column
                    Obsolete = reader.GetString(5), //6: 7th column
                }
                );
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return newProductList;
        }
    }
}

