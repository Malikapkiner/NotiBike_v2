namespace WebProje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userForeigni : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Races", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.Races", name: "IX_User_Id", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Races", name: "IX_UserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Races", name: "UserId", newName: "User_Id");
        }
    }
}
