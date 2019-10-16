
using Entity;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.DBContext
{
    public class S3InnovationDbContext : DbContext
    {
        public S3InnovationDbContext(DbContextOptions<S3InnovationDbContext> options) :
            base(options)
        {

        }

        public S3InnovationDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../S3InnovationAPI"))
           .AddJsonFile("appsettings.json")
           .Build();
            var builder = new DbContextOptionsBuilder<S3InnovationDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("S3InnovationDB"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           
        }

        public DbSet<Trade> Trades { get; set; }

        public DbSet<Level> Levels { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<TradeDetails> TradeDetails { get; set; }

    }
}
