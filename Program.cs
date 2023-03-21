using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using Todo_Assignment.API.Data.DbContexts;
using Todo_Assignment.API.Services;

namespace Todo_Assignment.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Inject Db Context
            builder.Services.AddDbContext<TodoDbContext>();

            // Inject Services
            builder.Services.AddScoped<ITaskRepository, TaskRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            // Inject Auto Mapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Inject Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Error()
                .WriteTo.Console()
                .WriteTo.File("Logs/task.txt", rollingInterval: RollingInterval.Hour)
                .CreateLogger();

            builder.Host.UseSerilog();

            // Authentication
            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new()
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = builder.Configuration["Authentication:Issuer"],
                            ValidAudience = builder.Configuration["Authentication:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
                        };
                    }
                );

            // Allow connection from client - policy
            builder.Services.AddCors(options => options.AddPolicy(name: "TodoAssignment.UI",
                policy =>
                {
                    policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
                }));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Call Policy
            app.UseCors("TodoAssignment.UI");

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}