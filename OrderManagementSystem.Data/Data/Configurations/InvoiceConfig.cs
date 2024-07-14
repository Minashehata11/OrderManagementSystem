using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagementSystem.Data.Entities;

namespace OrderManagementSystem.Data.Data.Configurations
{
    public class InvoiceConfig : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasOne(I => I.Order).WithOne().HasForeignKey<Invoice>(i => i.OrderId).IsRequired(false).OnDelete(deleteBehavior: DeleteBehavior.SetNull);
            builder.Property(o => o.TotalAmount).HasColumnType("decimal(18, 2)");

        }
    }
}
