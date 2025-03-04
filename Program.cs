using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Bitacora.Data;
using Bitacora.Auth;


namespace Bitacora
{
    public class Program
    {
        public static void Main(string[] args)
        {
			var builder = WebApplication.CreateBuilder(args);

			//Inyección de dependencias.

			builder.Services.AddDbContext<BitacoraDb>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BitacoraDb")));

			builder.Services.AddIdentity<AutenticacionUsuario, IdentityRole<int>>().AddEntityFrameworkStores<BitacoraDb>();

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			var app = builder.Build();

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


			app.Run();
		}
    }
}
