namespace CommerceApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        firstName = c.String(),
                        lastName = c.String(),
                        jobTitle = c.String(),
                        birthDate = c.DateTime(nullable: false),
                        hireDate = c.DateTime(nullable: false),
                        daysFirstCall = c.Int(nullable: false),
                        daysSecondCall = c.Int(nullable: false),
                        Email = c.String(),
                        Employee_EmployeeID = c.Int(),
                    })
                .PrimaryKey(t => t.EmployeeID)
                .ForeignKey("dbo.Employees", t => t.Employee_EmployeeID)
                .Index(t => t.Employee_EmployeeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "Employee_EmployeeID", "dbo.Employees");
            DropIndex("dbo.Employees", new[] { "Employee_EmployeeID" });
            DropTable("dbo.Employees");
        }
    }
}
