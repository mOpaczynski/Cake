namespace CakeDemo.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddChocolate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chocolates",
                c => new
                    {
                        SpecialId = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Cacao = c.String(),
                    })
                .PrimaryKey(t => t.SpecialId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Chocolates");
        }
    }
}
