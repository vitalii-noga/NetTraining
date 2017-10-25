namespace Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConfigurations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "Status", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Status", c => c.Int(nullable: false));
        }
    }
}
