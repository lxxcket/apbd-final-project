using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APBDFinalProject.Models.Configs;

public class IndividualCustomerEFConfiguration : IEntityTypeConfiguration<IndividualCustomer>
{
    public void Configure(EntityTypeBuilder<IndividualCustomer> builder)
    {
        builder.HasKey(e => e.Id).HasName("IndividualCustomer_pk");
        builder.Property(e => e.Id).UseIdentityColumn();
        
        builder.HasIndex(e => e.PESEL).IsUnique();
        builder.Property(e => e.PESEL).HasMaxLength(11).IsRequired();
        
        builder.Property(e => e.Address).HasMaxLength(50).IsRequired();
        builder.Property(e => e.Email).HasMaxLength(50).IsRequired();
        builder.Property(e => e.PhoneNumber).HasMaxLength(9).IsRequired();

        builder.ToTable("Individual_Customer");
    }
}