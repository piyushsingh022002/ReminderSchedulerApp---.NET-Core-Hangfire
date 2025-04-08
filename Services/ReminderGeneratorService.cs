using ReminderSchedulerApp.Models;
using Dapper;
using System.Data;

namespace ReminderSchedulerApp.Services
{

public class ReminderGeneratorService
{
    private readonly IDbConnection _connection;

    public ReminderGeneratorService(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task InsertRandomReminderAsync()
    {
        var reminder = new Reminder
        {
            Message = $"Auto-Reminder at {DateTime.Now:HH:mm:ss}",
            CreatedAt = DateTime.Now,
            DelayMinutes = 0
        };

        var query = "INSERT INTO Reminders (Message, CreatedAt, DelayMinutes) VALUES (@Message, @CreatedAt, @DelayMinutes)";
        await _connection.ExecuteAsync(query, reminder);
        
        Console.WriteLine("✅ New reminder inserted: " + reminder.Message);
    }
}
}