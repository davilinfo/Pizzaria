using Domain.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Persistence.Context
{
   public class Db : DbContext
   {
      private readonly IConfiguration _configuration;
      
      public DbSet<Cliente> Cliente { get; set; }
      public DbSet<Pedido> Pedido { get; set; }
      public DbSet<Pizza> Pizza { get; set; }
      public DbSet<Sabor> Sabores { get; set; }

      public Db(IConfiguration configuration, DbContextOptions<Db> options) : base(options)
      {
         _configuration = configuration;
      }

      protected override void OnConfiguring(DbContextOptionsBuilder options)
      {
         if (!options.IsConfigured)
         {
            options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
         }
      }

      protected override void OnModelCreating(ModelBuilder builder)
      {         
         base.OnModelCreating(builder);
      }
   }
}
