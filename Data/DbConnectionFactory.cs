using Microsoft.Data.SqlClient;
using System.Data;

namespace ReminderSchedulerApp.Data
{
    public class DbConnectionFactory
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public DbConnectionFactory(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
