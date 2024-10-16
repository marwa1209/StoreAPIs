
using Microsoft.EntityFrameworkCore;
using Store.Repository.Data.Contexts;

namespace StoreAPIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbContext>(option=>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            var app = builder.Build();
            using var scop=app.Services.CreateScope();
            var services=scop.ServiceProvider;
            var context=services.GetRequiredService<StoreDbContext>();
            var loggerFactory=services.GetRequiredService<ILoggerFactory>();

            try
            {
                await context.Database.MigrateAsync();

            }
            catch (Exception ex) {
             var logger=   loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, ex.Message);
            }
            await context.Database.MigrateAsync();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
