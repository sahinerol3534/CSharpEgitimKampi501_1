using CSharpEgitimKampi501_1.Dtos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpEgitimKampi501_1.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString = "server = SAHINEROL; initial catalog = EgitimKampi501Db; integrated security = true";
        public async Task CreateProductAsync(CreateProductDto createProductDto)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO TblProduct(ProductName, ProductStock, ProductPrice, ProductCatagory)" +
                    "VALUES(@productName, @productStock, @productPrice, @productCatagory)";
            
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@productName", createProductDto.ProductName);
                command.Parameters.AddWithValue("@productStock", createProductDto.ProductStock);
                command.Parameters.AddWithValue("@productPrice", createProductDto.ProductPrice);
                command.Parameters.AddWithValue("@productCatagory", createProductDto.ProductCatagory);
                connection.Open();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteProductAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM TblProduct WHERE ProductId = @productId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@productId", id);
                connection.Open();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            var products = new List<ResultProductDto>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM TblProduct";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync()) 
                    {
                        products.Add(new ResultProductDto
                        {
                            ProductId = reader.GetInt32(0),
                            ProductName = reader.GetString(1),
                            ProductStock = reader.GetInt32(2),
                            ProductPrice = reader.GetDecimal(3),
                            ProductCatagory = reader.GetString(4),
                        });  
                    }
                }

            }
            return products;
        }

        public async Task<ResultProductDto> GetByProductIdAsync(int id)
        {
            ResultProductDto product = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM TblProduct WHERE ProductId = @productId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@productId", id);
                connection.Open();
                using (SqlDataReader reader = await command.ExecuteReaderAsync()) 
                {
                    if ( await reader.ReadAsync())
                    {
                        product = new ResultProductDto
                        {
                            ProductId = reader.GetInt32(0),
                            ProductName = reader.GetString(1),
                            ProductStock = reader.GetInt32(2),
                            ProductPrice = reader.GetDecimal(3),
                            ProductCatagory = reader.GetString(4)
                        };
                    }
                }

            }
            return product;
        }

        public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
        {
             using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE TblProduct SET ProductName = @productName, ProductStock = @productStock," +
                    "ProductPrice = @productPrice, ProductCatagory = @productCatagory WHERE ProductId = @productId";
                SqlCommand command = new SqlCommand(@query, connection);
                command.Parameters.AddWithValue("@productName", updateProductDto.ProductName);
                command.Parameters.AddWithValue("@productStock", updateProductDto.ProductStock);
                command.Parameters.AddWithValue("@productPrice", updateProductDto.ProductPrice);
                command.Parameters.AddWithValue("@productCatagory", updateProductDto.ProductCatagory);
                command.Parameters.AddWithValue("@productId", updateProductDto.ProductId);

                connection.Open();
                await command.ExecuteNonQueryAsync();


            }
        }

        public async Task<List<string>> GetAllCatagoriesAsycn()
        
        {
            var catagories = new List<string>();
            string query = "SELECT DISTINCT ProductCatagory FROM TblProduct";
            using (var connection = new SqlConnection(_connectionString)) 
            {
                var command = new SqlCommand(@query, connection);
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read()) 
                    {
                        catagories.Add(reader["ProductCatagory"].ToString());
                    }
                }

            }
            return catagories;
            
        }

        public async Task<int> GetTotalBookCountAsync()
        {
            string query = "SELECT COUNT(*) FROM TblProduct";
            using(var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(query, connection);
                var result = await command.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
        }

        public async Task<int> GetTotalCatagoryCount()
        {
            string query = "SELECT COUNT(DISTINCT ProductCatagory) FROM TblProduct";
            using( var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(query,connection);
                var result = await command.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            };
        }

        public async Task<ResultProductDto> GetMostExpensiveBookAsync()
        {
            string query = "SELECT TOP 1 * FROM TblProduct ORDER BY ProductPrice DESC";
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(query, connection);
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        var book = new ResultProductDto
                        {
                            ProductName = reader["ProductName"].ToString()
                        };
                        return book;
                    }
                }
            }
            return null;

        }

        Task IProductRepository.GetByProductIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
