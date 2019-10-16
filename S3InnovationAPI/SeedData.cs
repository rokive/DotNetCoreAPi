using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repositorys.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S3InnovationAPI
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<S3InnovationDbContext>();
            context.Database.EnsureCreated();
            if (!context.Trades.Any())
            {
                List<Level> levels = new List<Level> { new Level() {LevelName = "Level 1" }, new Level() { LevelName = "Level 2" },
                new Level() {LevelName = "Level 3" }};
                context.Trades.Add(entity: new Trade() { TradeName = "Trade One",Level=levels });

                levels = new List<Level> { new Level() { LevelName = "Level 4" }, new Level() { LevelName = "Level 5" } };
                context.Trades.Add(entity: new Trade() {TradeName = "Trade Two",Level=levels  });

                levels = new List<Level> { new Level() {LevelName = "Level 6" }, new Level() { LevelName = "Level 7" },
                new Level() {LevelName = "Level 8" },new Level() {LevelName = "Level 9" },new Level() {LevelName = "Level 10" } };
                context.Trades.Add(entity: new Trade() {TradeName = "Trade Three",Level=levels });

                context.SaveChanges();
                
            }
           
            if (!context.Languages.Any())
            {
                context.Languages.Add(entity: new Language() { LanguageName = "English",ShortName="EN" });
                context.Languages.Add(entity: new Language() { LanguageName = "Chinese", ShortName = "CH" });
                context.Languages.Add(entity: new Language() { LanguageName = "Thai", ShortName = "TH" });
                context.Languages.Add(entity: new Language() { LanguageName = "Tamil", ShortName = "TM" });
                context.Languages.Add(entity: new Language() { LanguageName = "Korean", ShortName = "KR" });
                context.Languages.Add(entity: new Language() { LanguageName = "Burmese", ShortName = "BR" });
                context.SaveChanges();
                
            }
        }
    }
}