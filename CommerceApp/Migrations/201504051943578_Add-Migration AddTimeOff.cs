namespace CommerceApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMigrationAddTimeOff : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TimeOffs",
                c => new
                    {
                        TimeOffID = c.Int(nullable: false, identity: true),
                        FirstDay = c.DateTime(nullable: false),
                        LastDay = c.DateTime(nullable: false),
                        Employee_EmployeeID = c.Int(),
                    })
                .PrimaryKey(t => t.TimeOffID)
                .ForeignKey("dbo.Employees", t => t.Employee_EmployeeID)
                .Index(t => t.Employee_EmployeeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeOffs", "Employee_EmployeeID", "dbo.Employees");
            DropIndex("dbo.TimeOffs", new[] { "Employee_EmployeeID" });
            DropTable("dbo.TimeOffs");
        }
    }
}
