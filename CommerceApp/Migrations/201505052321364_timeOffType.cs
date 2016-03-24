namespace CommerceApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class timeOffType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeOffs", "timeOffType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TimeOffs", "timeOffType");
        }
    }
}
