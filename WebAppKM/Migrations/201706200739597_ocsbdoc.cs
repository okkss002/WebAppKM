namespace WebAppKM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ocsbdoc : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OcsbDoc",
                c => new
                    {
                        DocID = c.Int(nullable: false, identity: true),
                        DocType = c.String(maxLength: 4),
                        DocGroup = c.String(maxLength: 4),
                        DocName = c.String(maxLength: 400),
                        Details = c.String(),
                        Keyword = c.String(),
                        FileName = c.String(maxLength: 400),
                        Links = c.String(maxLength: 400),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DocID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OcsbDoc");
        }
    }
}
