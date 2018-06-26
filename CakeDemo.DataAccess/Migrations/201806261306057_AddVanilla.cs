namespace CakeDemo.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVanilla : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Vanillas",
                c => new
                    {
                        SpecialId = c.Int(nullable: false, identity: true),
                        Orchid = c.String(),
                        Harvest = c.String(),
                    })
                .PrimaryKey(t => t.SpecialId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Vanillas");
        }
    }
}
