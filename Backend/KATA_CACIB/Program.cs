using KATA_CACIB.DATA;
using KATA_CACIB.Models;
using KATA_CACIB.ServiceRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace KATA_CACIB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Register ServiceMock as a dependency for the controller
            builder.Services.AddScoped<ServiceMock>();
            builder.Services.AddScoped<ServiceManager>();
            builder.Services.AddScoped<WSocketManager>();
            builder.Services.AddMemoryCache();
            builder.Services.AddSingleton<ServiceManager>();
            builder.Services.AddSingleton<WSocketManager>();
            
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:3000" , "http://localhost:3001")
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                                  });
            });

            //----Configure JWT 
            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
                };
            });

            builder.Services.AddAuthorization();
            builder.Services.AddAuthorization();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseAuthentication();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthorization();
            
            // Enable WebSockets
            app.UseWebSockets();
            app.MapControllers();
            app.UseHttpsRedirection();

                     

            app.Run();
        }
    }
}
