using Dapper;
using Exempted.Models;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration; // Ensure this is included for IConfiguration
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Exempted.Repositories
{
    public class CatRepository : ICatRepository
    {
        private readonly string _connectionString;

        public CatRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        public async Task<IEnumerable<Cat>> GetAllCatsAsync()
        {
            var query = "SELECT * FROM Cats";
            using (var connection = CreateConnection())
            {
                connection.Open(); // Open the connection synchronously
                return await connection.QueryAsync<Cat>(query);
            }
        }

        public async Task<Cat> GetCatByIdAsync(int id)
        {
            var query = "SELECT * FROM Cats WHERE Id = @Id";
            using (var connection = CreateConnection())
            {
                connection.Open(); 
                return await connection.QueryFirstOrDefaultAsync<Cat>(query, new { Id = id });
            }
        }

        public async Task<int> AddCatAsync(Cat cat)
        {
            var query = "INSERT INTO Cats (Name, Breed, Age) VALUES (@Name, @Breed, @Age)";
            using (var connection = CreateConnection())
            {
                connection.Open(); 
                return await connection.ExecuteAsync(query, cat);
            }
        }

        public async Task<int> UpdateCatAsync(Cat cat)
        {
            var query = "UPDATE Cats SET Name = @Name, Breed = @Breed, Age = @Age WHERE Id = @Id";
            using (var connection = CreateConnection())
            {
                connection.Open(); 
                return await connection.ExecuteAsync(query, cat);
            }
        }

        public async Task<int> DeleteCatAsync(int id)
        {
            var query = "DELETE FROM Cats WHERE Id = @Id";
            using (var connection = CreateConnection())
            {
                connection.Open(); // Open the connection synchronously
                return await connection.ExecuteAsync(query, new { Id = id });
            }
        }
    }
}
