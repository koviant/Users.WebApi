namespace Users.DAL.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 30),
                        Value = c.String(nullable: false, maxLength: 15),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Birthdate = c.DateTime(nullable: false, storeType: "date"),
                        LoginName = c.String(nullable: false, maxLength: 15),
                        Password = c.String(nullable: false, maxLength: 15),
                        LastName = c.String(nullable: false, maxLength: 30),
                        FirstName = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.LoginName, unique: true, name: "Index");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.addresses", "User_Id", "dbo.users");
            DropIndex("dbo.users", "Index");
            DropIndex("dbo.addresses", new[] { "User_Id" });
            DropTable("dbo.users");
            DropTable("dbo.addresses");
        }
    }
}
