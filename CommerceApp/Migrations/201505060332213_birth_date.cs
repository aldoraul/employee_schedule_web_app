namespace CommerceApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class birth_date : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Employees", "birthDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "birthDate", c => c.DateTime(nullable: false));
        }
    }
}
