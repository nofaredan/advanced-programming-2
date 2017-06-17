namespace WebMazeGame.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Wins", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "Losses", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "DateRegister", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "DateRegister");
            DropColumn("dbo.Users", "Losses");
            DropColumn("dbo.Users", "Wins");
        }
    }
}
