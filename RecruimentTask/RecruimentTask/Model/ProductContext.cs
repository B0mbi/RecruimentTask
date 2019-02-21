using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RecruimentTask.Model
{
    public class ProductContext
    {
        private readonly string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="+"\""+ Environment.CurrentDirectory + "\\DataBase.mdf\";Integrated Security=True";

        /// <summary>
        /// Function returns a list of products
        /// </summary>
        /// <returns></returns>
        public List<Product> GetAllProducts()
        {
            List<Product> sqlResult = new List<Product>();
            string query = string.Format("Select [Id], [Name], [Price] from [dbo].[product]");
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlConnection.Open();
                SqlDataReader dataReader = sqlCommand.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        sqlResult.Add(new Product() {
                            Id = dataReader.GetGuid(0),
                            Name = dataReader.GetString(1),
                            Price = dataReader.GetDecimal(2)
                        });
                    }
                }
                sqlConnection.Close();
            }
            return sqlResult;
        }

        /// <summary>
        /// Function returns a single product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetProduct(Guid id)
        {
            Product product = new Product();
            string query = string.Format("Select [Name], [Price] from [dbo].[product] where [Id]=@Id");
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@Id", id);
                sqlConnection.Open();
                SqlDataReader dataReader = sqlCommand.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        product = new Product() {
                            Id = id,
                            Name = dataReader.GetString(0),
                            Price = dataReader.GetDecimal(1)
                        };
                    }
                }
                sqlConnection.Close();
            }
            return product;
        }

        /// <summary>
        /// Method that adds the product to database. Name and price is required
        /// </summary>
        /// <param name="model"></param>
        public Guid AddProduct(ProductCreateInputModel model)
        {
            Guid id = Guid.NewGuid();
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                string query = string.Format("Insert into [dbo].[product] ([Id], [Name], [Price]) values (@guidValue, @name, @price)");
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@guidValue", id);
                sqlCommand.Parameters.AddWithValue("@name", model.Name);
                sqlCommand.Parameters.AddWithValue("@price", model.Price);
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
            return id;
        }

        /// <summary>
        /// Method that updates the product in database. Id, name and price is required
        /// </summary>
        /// <param name="model"></param>
        public void UpdateProduct(ProductUpdateInputModel model)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                string query = string.Format("Update [dbo].[product] set [Name]=@name, [Price]=@price where [Id]=@id");
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@id", model.Id);
                sqlCommand.Parameters.AddWithValue("@name", model.Name);
                sqlCommand.Parameters.AddWithValue("@price", model.Price);
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// Method that deletes product from database.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteProduct(Guid id)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                string query = string.Format("delete [dbo].[product] where [Id]=@Id");
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@Id", id);
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }
    }
}
