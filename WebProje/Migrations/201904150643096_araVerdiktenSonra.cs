namespace WebProje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class araVerdiktenSonra : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Races", "Coordinates", c => c.String());
            AddColumn("dbo.Races", "Location", c => c.String());
            AddColumn("dbo.Races", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Races", "Description");
            DropColumn("dbo.Races", "Location");
            DropColumn("dbo.Races", "Coordinates");
        }
    }
}
