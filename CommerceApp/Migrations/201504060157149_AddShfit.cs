namespace CommerceApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddShfit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Shifts",
                c => new
                    {
                        ShiftID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.Int(nullable: false),
                        ShiftDate = c.DateTime(nullable: false),
                        ShiftPrimary = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ShiftID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID, cascadeDelete: true)
                .Index(t => t.EmployeeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shifts", "EmployeeID", "dbo.Employees");
            DropIndex("dbo.Shifts", new[] { "EmployeeID" });
            DropTable("dbo.Shifts");
        }
    }
}
