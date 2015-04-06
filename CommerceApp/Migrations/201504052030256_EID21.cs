namespace CommerceApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EID21 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TimeOffs", "Employee_EmployeeID", "dbo.Employees");
            DropIndex("dbo.TimeOffs", new[] { "Employee_EmployeeID" });
            RenameColumn(table: "dbo.TimeOffs", name: "Employee_EmployeeID", newName: "EmployeeID");
            AlterColumn("dbo.TimeOffs", "EmployeeID", c => c.Int(nullable: false));
            CreateIndex("dbo.TimeOffs", "EmployeeID");
            AddForeignKey("dbo.TimeOffs", "EmployeeID", "dbo.Employees", "EmployeeID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeOffs", "EmployeeID", "dbo.Employees");
            DropIndex("dbo.TimeOffs", new[] { "EmployeeID" });
            AlterColumn("dbo.TimeOffs", "EmployeeID", c => c.Int());
            RenameColumn(table: "dbo.TimeOffs", name: "EmployeeID", newName: "Employee_EmployeeID");
            CreateIndex("dbo.TimeOffs", "Employee_EmployeeID");
            AddForeignKey("dbo.TimeOffs", "Employee_EmployeeID", "dbo.Employees", "EmployeeID");
        }
    }
}
