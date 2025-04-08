using Dapper;
using ReminderSchedulerApp.Models;

namespace ReminderSchedulerApp.Data
{
    public class ReminderRepository : IReminderRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public ReminderRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task AddAsync(Reminder reminder)
        {
            var sql = @"INSERT INTO Reminders (Message, DelayMinutes, CreatedAt)
                        VALUES (@Message, @DelayMinutes, @CreatedAt)";

            using var connection = _connectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, reminder);
        }

        public async Task<IEnumerable<Reminder>> GetAllAsync()
        {
            var sql = "SELECT * FROM Reminders ORDER BY CreatedAt DESC";

            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryAsync<Reminder>(sql);
        }
    }
}
