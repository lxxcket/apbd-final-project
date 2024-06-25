using APBDFinalProject.Models;
using Microsoft.EntityFrameworkCore;
using Version = APBDFinalProject.Models.Version;

namespace APBDFinalProject.Contexts;

public class IncomeContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<IndividualCustomer> IndividualCustomers { get; set; }
    public DbSet<BusinessCustomer> BusinessCustomers { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Software> Softwares { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Version> Versions { get; set; }
    protected IncomeContext()
    {
    }

    public IncomeContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Customer>().ToTable("Customers");
        modelBuilder.Entity<IndividualCustomer>().ToTable("Individual_Customers");
        modelBuilder.Entity<BusinessCustomer>().ToTable("Business_Customers");
        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Customer)
            .WithMany()
            .HasForeignKey(p => p.IdCustomer)
            .OnDelete(DeleteBehavior.NoAction);
            
        
        modelBuilder.Entity<IndividualCustomer>(entity =>
            entity.
                HasIndex(e => e.PESEL).
                IsUnique());
        modelBuilder.Entity<BusinessCustomer>(entity =>
            entity.
                HasIndex(e => e.KRS).
                IsUnique());
    }
}