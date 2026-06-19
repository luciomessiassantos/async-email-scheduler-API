using System.Text.Json.Serialization;
using EmailScheduler.src.Modules.EmailScheduler.Infrastructure;
using EmailScheduler.src.Shared.Jobs;
using EmailScheduler.src.Shared.Services.Implementations;
using EmailScheduler.src.Shared.Services.Interfaces;
using EmailScheduler.src.Shared.Services.Settings;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

const string QUARTZ_GROUP = "EmailScheduleGroup";


builder.Services.AddOpenApi();

builder.Services.Configure<SmtpSettings>(
    builder.Configuration.GetSection("SmtpSettings")
);

builder.Services.AddScoped<IEmailService, EmailService>();


string connectionString = builder.Configuration.GetConnectionString("Default") 
    ?? throw new InvalidOperationException("Connection string not found.");


builder.Services.AddDbContext<EmailSchedulerDbContext>(opt => 
    opt.UseNpgsql(connectionString));

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddQuartz(config =>
{

    config.UsePersistentStore(storeOptions =>
    {

        storeOptions.UseNewtonsoftJsonSerializer();

        storeOptions.UsePostgres(postgresOptions =>
        {
            postgresOptions.ConnectionString = connectionString;
        });


        storeOptions.UseClustering(clusterOptions =>
        {
            clusterOptions.CheckinInterval = TimeSpan.FromSeconds(20);
            clusterOptions.CheckinMisfireThreshold = TimeSpan.FromSeconds(20);
        });
    });

    var key = new JobKey("HealthCheckJob", QUARTZ_GROUP);

    config.AddJob<HealthCheckJob>(opts => opts.WithIdentity(key));

    config.AddTrigger(opts => opts
    .ForJob(key)
    .WithIdentity("HeathCheckTrigger", QUARTZ_GROUP)
    .WithCronSchedule("0 0/5 * * * ?"));

});

builder.Services.AddQuartzHostedService(opts =>
{
    opts.WaitForJobsToComplete = true;
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
    
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.MapControllers();
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
