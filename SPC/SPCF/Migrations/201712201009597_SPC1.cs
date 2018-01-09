namespace SecondProjectCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SPC1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comments", "Comment", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comments", "Comment", c => c.String(nullable: false, maxLength: 500));
        }
    }
}
