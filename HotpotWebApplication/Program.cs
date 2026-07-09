using System.Text;
using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.AspNetCore;
using HotpotWebApplication.Data;
using HotpotWebApplication.Middleware;
using HotpotWebApplication.Repositories.Implementations;
using HotpotWebApplication.Repositories.Interfaces;
using HotpotWebApplication.Services.Implemenations;
using HotpotWebApplication.Services.Implementations;
using HotpotWebApplication.Services.Interfaces;
using HotpotWebApplication.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace HotpotWebApplication
{
    public class Program
    {
        public static void Main(string[]args)
        {
            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(
              "Logs/hotpot-.txt",
                rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog();


            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("ReactPolicy", policy =>
                {
                    policy
                        .WithOrigins("http://localhost:5173")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter());
    });
            builder.Services.AddScoped<IJwtService, JwtService>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            builder.Services.AddScoped<IRestaurantService, RestaurantService>();

            builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
            builder.Services.AddScoped<IMenuItemService, MenuItemService>();

            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IOrderService, OrderService>();

            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();

            builder.Services.AddScoped<ICartRepository, CartRepository>();
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],

                    IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    builder.Configuration["Jwt:Key"]!))
                };

            });
            builder.Services.AddFluentValidationAutoValidation();

            builder.Services.AddValidatorsFromAssemblyContaining<CreateCategoryValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateMenuItemValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateRestaurantValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<RegisterValidator>();
            builder.Services.AddAuthorization();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
           

            var app = builder.Build();
       

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("ReactPolicy");

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
    }
}


