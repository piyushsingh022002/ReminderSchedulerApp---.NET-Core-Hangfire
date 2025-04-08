using ReminderSchedulerApp.Models;

namespace ReminderSchedulerApp.Data
{
    public interface IReminderRepository
    {
        Task AddAsync(Reminder reminder);
        Task<IEnumerable<Reminder>> GetAllAsync();
    }
}
