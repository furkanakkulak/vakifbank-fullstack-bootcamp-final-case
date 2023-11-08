using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VkFinalCase.Base.Model;

namespace VkFinalCase.Data.Domain;

[Table("Dealer", Schema = "dbo")]
public class Dealer : BaseModel
{
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public string Address { get; set; }
    public string TaxNumber { get; set; }
    public decimal CreditLimit { get; set; }
    public decimal Margin { get; set; }

    public virtual List<Order> Orders { get; set; }
}

public class DealerConfiguration : IEntityTypeConfiguration<Dealer>
{
    public void Configure(EntityTypeBuilder<Dealer> builder)
    {
        builder.Property(x => x.CreatedDate).IsRequired();
        builder.Property(x => x.UpdatedDate).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

        builder.Property(x => x.UserId).IsRequired(true);
        builder.Property(x => x.Address).IsRequired().HasMaxLength(100);
        builder.Property(x => x.TaxNumber).IsRequired().HasMaxLength(10);
        builder.Property(x => x.CreditLimit).IsRequired().HasPrecision(18, 2).HasDefaultValue(0);
        builder.Property(x => x.Margin).IsRequired().HasPrecision(18, 2).HasDefaultValue(0);

        builder.HasIndex(x => x.TaxNumber).IsUnique(true);

        builder.HasMany(x => x.Orders)
            .WithOne(o => o.Dealer)
            .HasForeignKey(o => o.DealerId);
        
    }
}
