using Application.Interface;
using Application.Service;
using Domain.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Persistence.Context;
using Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzaria
{
   public class Startup
   {
      public Startup(IConfiguration configuration)
      {
         Configuration = configuration;
      }

      public IConfiguration Configuration { get; }

      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services)
      {

         services.AddControllers();

         services.AddScoped<IPedidoService, PedidoService>();
         services.AddScoped<IRepositoryPedido, RepositoryPedido>();
         services.AddScoped<IRepositoryCliente, RepositoryCliente>();
         services.AddScoped<IRepositoryPizza, RepositoryPizza>();
         services.AddScoped<IRepositorySabor, RepositorySabor>();

         services.AddAutoMapper(System.Reflection.Assembly.Load("Application"));
         //services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
         services.AddDbContext<Db>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), options=> options.EnableRetryOnFailure()));
         services.AddHttpClient();

         services.AddSwaggerGen(c =>
         {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pizzaria", Version = "v1" });
            //c.IncludeXmlComments($@"Pedido.xml");
         });         
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pizzaria v1"));
         }

         app.UseHttpsRedirection();

         app.UseCors(c =>
         {
            c.AllowAnyHeader();
            c.AllowAnyMethod();
            c.AllowAnyOrigin();
         });         

         app.UseRouting();

         app.UseAuthorization();

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
         });

         using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
         {
            var context = serviceScope.ServiceProvider.GetRequiredService<Db>();
            context.Database.Migrate();
         }
      }
   }
}
