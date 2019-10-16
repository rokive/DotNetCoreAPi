
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Repositorys.DBContext
{
    public class S3InnovationContextFactory : IDesignTimeDbContextFactory<S3InnovationDbContext>
    {
        public S3InnovationContextFactory()
        {
        }


        public S3InnovationDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../S3InnovationAPI"))
            .AddJsonFile("appsettings.json")
            .Build();
            var builder = new DbContextOptionsBuilder<S3InnovationDbContext>();
            builder.UseSqlServer(configuration.GetConnectionString("S3InnovationDB"));
            return new S3InnovationDbContext(builder.Options);
        }
        
    }
}
