using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using Note_App.Models;
using Note_App.IRepositories;
using Note_App.DatabaseContext;

namespace Note_App.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public UserRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        private IDbConnection DbConnection() => _dbContext.CreateConnection();

        // Implement RegisterUserAsync method
        public async Task<User?> RegisterUserAsync(User user)
        {
            using var connection = DbConnection();
            var query = @"INSERT INTO Users (Username, Email, Password) 
                          VALUES (@Username, @Email, @Password);
                          SELECT CAST(SCOPE_IDENTITY() AS int);";

            var userId = await connection.ExecuteScalarAsync<int?>(query, new
            {
                user.Username,
                user.Email,
                user.Password
            });

            if (userId.HasValue)
            {
                user.Id = userId;
                return user;
            }

            return null;
        }
    }
}
