using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using VkFinalCase.Base.Model;

namespace VkFinalCase.Data.Domain;


[Table("User", Schema = "dbo")]
public class User : BaseModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public string Email { get; set; }

    public virtual Dealer Dealer { get; set; }
    public virtual List<Message> SentMessages { get; set; }
    public virtual List<Message> ReceivedMessages { get; set; }
}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.CreatedDate).IsRequired();
        builder.Property(x => x.UpdatedDate).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

        builder.Property(x => x.Username).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.Password).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Role).IsRequired().HasMaxLength(10);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(50);

        builder.HasIndex(x => x.Email).IsUnique(true);

        builder.HasOne(x => x.Dealer)
            .WithOne(d => d.User)
            .HasForeignKey<Dealer>(d => d.UserId);
        
    }
}
