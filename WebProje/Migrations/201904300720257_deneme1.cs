namespace WebProje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deneme1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Races", "Coor_X", c => c.String());
            AlterColumn("dbo.Races", "Coor_Y", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Races", "Coor_Y", c => c.String(nullable: false));
            AlterColumn("dbo.Races", "Coor_X", c => c.String(nullable: false));
        }
    }
}
