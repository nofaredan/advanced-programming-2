namespace WebMazeGame.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebMazeGame.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebMazeGame.Models.WebMazeGameContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebMazeGame.Models.WebMazeGameContext context)
        {
            context.Users.AddOrUpdate(x => x.Id,
           new User { Id = 1, Name = "aa", Password = "aaa", Email = "rr@gmail.com", DateRegister = DateTime.Now, Wins = 0, Losses = 0 });
        }
    }
}
