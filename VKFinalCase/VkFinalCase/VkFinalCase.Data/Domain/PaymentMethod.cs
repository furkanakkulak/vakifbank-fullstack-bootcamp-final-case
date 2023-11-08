using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VkFinalCase.Base.Model;

namespace VkFinalCase.Data.Domain;

[Table("PaymentMethod", Schema = "dbo")]
public class PaymentMethod : BaseModel
{
    public string Name { get; set; }
    public virtual List<Order> Orders { get; set; }
}

public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
{
    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        builder.Property(x => x.CreatedDate).IsRequired();
        builder.Property(x => x.UpdatedDate).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(255);

        // PaymentMethod sınıfındaki Orders özelliği ile Order sınıfındaki PaymentMethod özelliğini ilişkilendirme
        builder.HasMany(x => x.Orders)
            .WithOne(o => o.PaymentMethod)
            .HasForeignKey(o => o.PaymentMethodId);
        
    }
}