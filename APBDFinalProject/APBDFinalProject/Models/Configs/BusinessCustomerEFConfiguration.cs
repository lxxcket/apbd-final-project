using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APBDFinalProject.Models.Configs;

public class BusinessCustomerEFConfiguration : IEntityTypeConfiguration<BusinessCustomer>
{
    public void Configure(EntityTypeBuilder<BusinessCustomer> builder)
    {
        builder.HasKey(e => e.Id).HasName("BusinessCustomer_pk");
        builder.Property(e => e.Id).UseIdentityColumn();
            
        builder.HasIndex(e => e.KRS).IsUnique();
        builder.Property(e => e.KRS).HasMaxLength(10).IsRequired();
        
        builder.Property(e => e.BusinessName).HasMaxLength(50).IsRequired();
        builder.Property(e => e.Address).HasMaxLength(50).IsRequired();
        builder.Property(e => e.Email).HasMaxLength(50).IsRequired();
        builder.Property(e => e.PhoneNumber).HasMaxLength(9).IsRequired();

        builder.ToTable("Business_Customer");
    }
}