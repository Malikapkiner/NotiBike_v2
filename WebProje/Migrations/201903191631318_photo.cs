namespace WebProje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class photo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserDetails", "PhotoUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserDetails", "PhotoUrl");
        }
    }
}
