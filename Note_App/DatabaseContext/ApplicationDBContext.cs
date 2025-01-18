using Microsoft.Data.SqlClient;
using System.Data;

namespace Note_App.DatabaseContext
{
    public class ApplicationDBContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public ApplicationDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection")
                ?? throw new ArgumentNullException(nameof(_connectionString), "Connection string is null");
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}
