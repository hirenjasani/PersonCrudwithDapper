using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using PersonCrudwithDapper.DTO;
using PersonCrudwithDapper.IRepository;
using PersonCrudwithDapper.Models;
using PersonCrudwithDapper.Repository;
using PersonCrudwithDapper.Service;

namespace PersonCrudwithDapper
{
    public class Program
    {
        public static IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddAutoMapper(typeof(Program));
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.CreateMap<Person, PersonViewModel>();
            });
            IMapper mapper = mappingConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            // Add the PersonRepository implementation to the dependency injection container
            builder.Services.AddTransient<IPersonRepository>(provider =>
                new PersonRepository(configuration.GetConnectionString("DefaultConnection")));

            // Add the PersonService implementation to the dependency injection container
            builder.Services.AddTransient<PersonService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Person}/{action=Index}/{id?}");

            app.Run();
        }
    }
}