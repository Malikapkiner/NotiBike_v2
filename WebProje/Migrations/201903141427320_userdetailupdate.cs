namespace WebProje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userdetailupdate : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.UserDetails", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.UserDetails", name: "IX_User_Id", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.UserDetails", name: "IX_UserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.UserDetails", name: "UserId", newName: "User_Id");
        }
    }
}
