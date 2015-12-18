namespace FindIt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDb : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ProductSuppliers", newName: "SupplierProducts");
            DropIndex("dbo.CommandeClients", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.CommandeSuppliers", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.CommandeClients", "ClientId");
            DropColumn("dbo.CommandeSuppliers", "EmployeeId");
            RenameColumn(table: "dbo.CommandeClients", name: "ApplicationUser_Id", newName: "ClientId");
            RenameColumn(table: "dbo.CommandeSuppliers", name: "ApplicationUser_Id", newName: "EmployeeId");
            DropPrimaryKey("dbo.SupplierProducts");
            AddColumn("dbo.CommandeClients", "isTrack", c => c.Boolean(nullable: false));
            AlterColumn("dbo.CommandeClients", "ClientId", c => c.String(maxLength: 128));
            AlterColumn("dbo.CommandeSuppliers", "EmployeeId", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.SupplierProducts", new[] { "Supplier_Id", "Product_Id" });
            CreateIndex("dbo.CommandeClients", "ClientId");
            CreateIndex("dbo.CommandeSuppliers", "EmployeeId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.CommandeSuppliers", new[] { "EmployeeId" });
            DropIndex("dbo.CommandeClients", new[] { "ClientId" });
            DropPrimaryKey("dbo.SupplierProducts");
            AlterColumn("dbo.CommandeSuppliers", "EmployeeId", c => c.String());
            AlterColumn("dbo.CommandeClients", "ClientId", c => c.String());
            DropColumn("dbo.CommandeClients", "isTrack");
            AddPrimaryKey("dbo.SupplierProducts", new[] { "Product_Id", "Supplier_Id" });
            RenameColumn(table: "dbo.CommandeSuppliers", name: "EmployeeId", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.CommandeClients", name: "ClientId", newName: "ApplicationUser_Id");
            AddColumn("dbo.CommandeSuppliers", "EmployeeId", c => c.String());
            AddColumn("dbo.CommandeClients", "ClientId", c => c.String());
            CreateIndex("dbo.CommandeSuppliers", "ApplicationUser_Id");
            CreateIndex("dbo.CommandeClients", "ApplicationUser_Id");
            RenameTable(name: "dbo.SupplierProducts", newName: "ProductSuppliers");
        }
    }
}
