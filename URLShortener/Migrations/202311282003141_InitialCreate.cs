namespace URLShortener.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.URLRecords",
                c => new
                    {
                        URLRecordId = c.Int(nullable: false, identity: true),
                        OriginalURL = c.String(),
                        ShortenedURL = c.String(),
                    })
                .PrimaryKey(t => t.URLRecordId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.URLRecords");
        }
    }
}
