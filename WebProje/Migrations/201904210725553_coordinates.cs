namespace WebProje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class coordinates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Races", "Coor_X", c => c.String(nullable: false));
            AddColumn("dbo.Races", "Coor_Y", c => c.String(nullable: false));
            DropColumn("dbo.Races", "Coordinates");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Races", "Coordinates", c => c.String());
            DropColumn("dbo.Races", "Coor_Y");
            DropColumn("dbo.Races", "Coor_X");
        }
    }
}
