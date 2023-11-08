using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VkFinalCase.Base.Model;

namespace VkFinalCase.Data.Domain;

[Table("Order", Schema = "dbo")]
public class Order : BaseModel
{
    public int DealerId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; }
    public decimal TotalPrice { get; set; }
    
    public int PaymentMethodId { get; set; }
    public virtual PaymentMethod PaymentMethod { get; set; }

    public virtual Dealer Dealer { get; set; }
    public virtual Product Product { get; set; }
    public virtual List<OrderPayment> OrderPayments { get; set; }
}

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(x => x.CreatedDate).IsRequired();
        builder.Property(x => x.UpdatedDate).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

        builder.Property(x => x.DealerId).IsRequired();
        builder.Property(x => x.ProductId).IsRequired();
        builder.Property(x => x.Quantity).IsRequired();
        builder.Property(x => x.OrderDate).IsRequired();
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.TotalPrice).IsRequired().HasPrecision(18, 2);
        builder.Property(x => x.PaymentMethodId).IsRequired();

        builder.HasOne(o => o.Product)
            .WithMany(p => p.Orders)
            .HasForeignKey(o => o.ProductId);

        builder.HasMany(o => o.OrderPayments)
            .WithOne(op => op.Order)
            .HasForeignKey(op => op.OrderId);

        builder.HasOne(o => o.Dealer)
            .WithMany(d => d.Orders)
            .HasForeignKey(o => o.DealerId);

        builder.HasOne(o => o.PaymentMethod)
            .WithMany(p => p.Orders)
            .HasForeignKey(o => o.PaymentMethodId);
    }
}