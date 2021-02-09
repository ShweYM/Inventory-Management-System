using Inventory_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.DB
{
    public class InventoryManagementSystemContext : DbContext
    {
        protected IConfiguration configuration;

        public InventoryManagementSystemContext(DbContextOptions<InventoryManagementSystemContext> options): base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            
        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            
            model.Entity<InventoryTransaction>().HasIndex(model => model.ITCode).IsUnique();
            
            model.Entity<CollectionPoint>(b =>
            {
                b.HasKey(e => e.Id);
                b.Property(e => e.Id).ValueGeneratedOnAdd();
            });
            model.Entity<DelegationForm>(b =>
            {
                b.HasKey(e => e.Id);
                b.Property(e => e.Id).ValueGeneratedOnAdd();
            });
                
        }
        public DbSet<CollectionPoint> CollectionPoints { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DelegationForm> DelegationForms { get; set; }
        public DbSet<DisbursementForm> DisbursementForms { get; set; }
        public DbSet<DisbursementFormProduct> DisbursementFormProducts { get; set; }
        public DbSet<DisbursementFormRequisitionForm> DisbursementFormRequisitionForms { get; set; }
        public DbSet<DisbursementFormRequisitionFormProduct> DisbursementFormRequisitionFormProducts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<RequisitionForm> RequisitionForms { get; set; }
        public DbSet<RequisitionFormsProduct> RequisitionFormsProducts { get; set; }
        public DbSet<StationeryRetrieval> StationeryRetrievals { get; set; }
        public DbSet<StationeryRetrievalProduct> StationeryRetrievalProduct { get; set; }
        public DbSet<StationeryRetrievalRequisitionForm> StationeryRetrievalRequisitionForms { get; set; }
        public DbSet<StationeryRetrievalRequisitionFormProduct> StationeryRetrievalRequisitionFormProducts { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierProduct> SupplierProducts { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderSupplierProduct> PurchaseOrderSupplierProducts { get; set; }
        public DbSet<DeliveryOrder> DeliveryOrders { get; set; }
        public DbSet<DeliveryOrderSupplierProduct> DeliveryOrderSupplierProducts { get; set; }
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }





    }
}
