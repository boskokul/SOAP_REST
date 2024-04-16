namespace ProjekatSoapRest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        JMBG = c.Long(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        FormattedDateTime = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        DeservesRaise = c.Boolean(nullable: false),
                        Company_Id = c.Long(),
                    })
                .PrimaryKey(t => t.JMBG)
                .ForeignKey("dbo.Companies", t => t.Company_Id)
                .Index(t => t.Company_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "Company_Id", "dbo.Companies");
            DropIndex("dbo.Employees", new[] { "Company_Id" });
            DropTable("dbo.Employees");
            DropTable("dbo.Companies");
        }
    }
}
