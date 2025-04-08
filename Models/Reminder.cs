namespace ReminderSchedulerApp.Models
{
    public class Reminder
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int DelayMinutes { get; set; } // Delay in minutes for Hangfire
        public DateTime CreatedAt { get; set; }
    }
}
