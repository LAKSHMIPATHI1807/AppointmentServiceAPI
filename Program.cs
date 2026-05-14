using AppointmentServiceAPI.Data;
using AppointmentServiceAPI.Repositories;
using AppointmentServiceAPI.Services;
using AppointmentServiceAPI.Services.ExternalModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AppointmentServiceAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddHttpClient("DoctorService", client =>
            {
                client.BaseAddress = new Uri("https://doctorapi-bmazacbtbyh3fqaq.centralus-01.azurewebsites.net/api/Doctor/");
            });
            builder.Services.AddScoped<IDoctorServiceClient, DoctorServiceClient>();
            builder.Services.AddHttpClient("PatientService", client =>
            {
                client.BaseAddress = new Uri("https://patientapi-fbdpg4gnc9fdcad5.centralus-01.azurewebsites.net/api/Patient/");
            });
            builder.Services.AddScoped<IPatientServiceClient, PatientServiceClient>();
            var connectionstring = builder.Configuration.
               GetConnectionString("AppointmentDbConnection");
            builder.Services.AddDbContext<AppointmentDbContext>
                (options => options.UseSqlServer(connectionstring));
            builder.Services.AddTransient<IAppointmentRepository, AppointmentRepository>();
            builder.Services.AddTransient<IAppointmentService, AppointmentService>();

            // AutoMapper configuration
            builder.Services.AddAutoMapper(cfg => { }, AppDomain.CurrentDomain.GetAssemblies());

            var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
