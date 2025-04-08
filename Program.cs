using Hangfire;
using Hangfire.SqlServer;
using ReminderSchedulerApp.Data;
using ReminderSchedulerApp.Models;
using ReminderSchedulerApp.Services;
using Microsoft.Data.SqlClient; // For SqlConnection
using System.Data;              // For IDbConnection



var builder = WebApplication.CreateBuilder(args);

// Hangfire configuration
builder.Services.AddHangfire(config =>
{
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddHangfireServer();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ReminderGeneratorService>();

builder.Services.AddSingleton<DbConnectionFactory>();
builder.Services.AddScoped<IReminderRepository, ReminderRepository>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));





var app = builder.Build();


// Hangfire dashboard endpoint
app.UseHangfireDashboard("/hangfire");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


RecurringJob.AddOrUpdate<ReminderGeneratorService>(
    service => service.InsertRandomReminderAsync(),
    "*/2 * * * *" // Cron expression for every 2 minutes
);


app.Run();
