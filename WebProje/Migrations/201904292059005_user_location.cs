namespace WebProje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user_location : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserDetails", "Location", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserDetails", "Location");
        }
    }
}
