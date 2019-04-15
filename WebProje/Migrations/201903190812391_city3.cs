namespace WebProje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class city3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserDetails", "CityId", c => c.Int());
            CreateIndex("dbo.UserDetails", "CityId");
            AddForeignKey("dbo.UserDetails", "CityId", "dbo.Cities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserDetails", "CityId", "dbo.Cities");
            DropIndex("dbo.UserDetails", new[] { "CityId" });
            DropColumn("dbo.UserDetails", "CityId");
        }
    }
}
