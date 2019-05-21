namespace WebProje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class racename_düzeltildi : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Races", "RaceName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Races", "RaceName", c => c.String());
        }
    }
}
