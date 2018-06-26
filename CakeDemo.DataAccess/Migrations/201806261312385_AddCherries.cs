namespace CakeDemo.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCherries : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cherries",
                c => new
                    {
                        SpecialId = c.Int(nullable: false, identity: true),
                        Redness = c.String(),
                        Tastiness = c.String(),
                    })
                .PrimaryKey(t => t.SpecialId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Cherries");
        }
    }
}
