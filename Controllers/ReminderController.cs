using Microsoft.AspNetCore.Mvc;
using ReminderSchedulerApp.Data;
using ReminderSchedulerApp.Models;
using Hangfire;

namespace ReminderSchedulerApp.Controllers
{
    public class ReminderController : Controller
    {
        private readonly IReminderRepository _repo;

        public ReminderController(IReminderRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var reminders = await _repo.GetAllAsync();
            return View(reminders);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Reminder reminder)
        {
            if (!ModelState.IsValid)
                return View(reminder);

            reminder.CreatedAt = DateTime.Now;
            await _repo.AddAsync(reminder);

            // Hangfire: Run job after DelayMinutes
            // BackgroundJob.Schedule(() =>
            //     Console.WriteLine($"[Hangfire] Reminder: {reminder.Message}"),
            //     TimeSpan.FromMinutes(reminder.DelayMinutes)
            // );
            BackgroundJob.Schedule<EmailService>(email =>
    email.SendEmailAsync("b221037@iiit-bh.ac.in", "New Reminder!", $"Your reminder: {reminder.Message}"),
    TimeSpan.FromMinutes(reminder.DelayMinutes));


            return RedirectToAction("Index");
        }
    }
}
