namespace WebProje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class about : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserDetails", "About", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserDetails", "About");
        }
    }
}
