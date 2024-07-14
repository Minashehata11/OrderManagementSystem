using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagementSystem.Data.Entities;

namespace OrderManagementSystem.Data.Data.Configurations
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasMany(o => o.orderItems).WithOne().HasForeignKey(OI => OI.OrderId).IsRequired(false).OnDelete(deleteBehavior:DeleteBehavior.SetNull);
            builder.HasOne(o => o.Customer).WithMany().HasForeignKey(o => o.CustomerId).IsRequired(false).OnDelete(deleteBehavior: DeleteBehavior.SetNull);
            builder.Property(o=>o.Status).HasConversion(status => status.ToString(),status=>((orderPaymentStatus)Enum.Parse(typeof(orderPaymentStatus),status)));
            builder.Property(o=>o.PaymentMethod).HasConversion(status => status.ToString(),status=>((PaymentMethod)Enum.Parse(typeof(PaymentMethod),status)));
            builder.Property(o => o.TotalAmount).HasColumnType("decimal(18, 2)");
        }
    }
}
