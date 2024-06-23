using APBDFinalProject.Models;
using APBDFinalProject.Models.Configs;
using Microsoft.EntityFrameworkCore;

namespace APBDFinalProject.Contexts;

public class IncomeContext : DbContext
{
    public DbSet<IndividualCustomer> IndividualCustomers { get; set; }
    public DbSet<BusinessCustomer> BusinessCustomers { get; set; }
    protected IncomeContext()
    {
    }

    public IncomeContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BusinessCustomerEFConfiguration).Assembly);
    }
}