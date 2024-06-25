using System.Text;
using APBDFinalProject.Contexts;
using APBDFinalProject.Middlewares;
using APBDFinalProject.Repositories;
using APBDFinalProject.Services;
using APBDFinalProject.Services.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

public class Program
{
    
    public static void Main(string[] args)
    {
        
        var builder = WebApplication.CreateBuilder(args);
        
        //Registering services
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();
        var connectionString = builder.Configuration.GetConnectionString("MyConnectionString");
        builder.Services.AddDbContext<IncomeContext>(option => option.UseSqlServer(connectionString));
        //
        builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
        builder.Services.AddScoped<IBusinessCustomerRepository, BusinessCustomerRepository>();
        builder.Services.AddScoped<IIndividualCustomerRepository, IndividualCustomerRepository>();
        builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
        builder.Services.AddScoped<IContractRepository, ContractRepository>();
        builder.Services.AddScoped<IVersionRepository, VersionRepository>();
        builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
        
        //
        builder.Services.AddScoped<IBusinessCustomerService, BusinessCustomerService>();
        builder.Services.AddScoped<IContractService, ContractService>();
        builder.Services.AddScoped<IIndividualCustomerService, IndividualCustomerService>();
        builder.Services.AddScoped<IPaymentService, PaymentService>();
        builder.Services.AddScoped<IIncomeService, IncomeService>();
        //
        // builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();
        // builder.Services.AddScoped<IGetPatientService, GetPatientService>();
        // builder.Services.AddScoped<IUserRepository, UserRepository>();
        // builder.Services.AddScoped<IUserService, UserService>();
        
        
        // builder.Services.AddAuthentication(options =>
        // {
        //     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        // }).AddJwtBearer(opt =>
        // {
        //     opt.TokenValidationParameters = new TokenValidationParameters
        //     {
        //         ValidateIssuer = true,  
        //         ValidateAudience = true,
        //         ValidateLifetime = true,
        //         ClockSkew = TimeSpan.FromMinutes(2),
        //         ValidIssuer = "https://localhost:5181", 
        //         ValidAudience = "https://localhost:5181",
        //         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Secret"]))
        //     };
        //     opt.Events = new JwtBearerEvents
        //     {
        //         OnAuthenticationFailed = context =>
        //         {
        //             if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
        //             {
        //                 context.Response.Headers.Add("Token-expired", "true");
        //             }
        //             return Task.CompletedTask;
        //         }
        //     };
        // }).AddJwtBearer("IgnoreTokenExpirationScheme",opt =>
        // {
        //     opt.TokenValidationParameters = new TokenValidationParameters
        //     {
        //         ValidateIssuer = true,   
        //         ValidateAudience = true, 
        //         ValidateLifetime = false,
        //         ClockSkew = TimeSpan.FromMinutes(2),
        //         ValidIssuer = "https://localhost:5181", 
        //         ValidAudience = "https://localhost:5181", 
        //         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Secret"]))
        //     };
        // });
        
        var app = builder.Build();
        app.UseMiddleware<ExceptionMiddleware>();

        //Configuring the HTTP request pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.MapControllers();

        app.Run();
    }
}