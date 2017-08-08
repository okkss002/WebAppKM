namespace WebAppKM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ocsbdoc2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OcsbDoc", "Links", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OcsbDoc", "Links", c => c.String(maxLength: 400));
        }
    }
}
