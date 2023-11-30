namespace URLShortener.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class users_admin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsAdmin", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "IsAdmin");
        }
    }
}
