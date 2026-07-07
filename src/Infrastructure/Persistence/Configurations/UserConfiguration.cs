using Domain.Entities;
using Infrastructure.Persistence.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.TotalSpent)
            .HasColumnType("decimal(18,2)");
            
        builder.HasData(UserSeedData.GetUsers());
    }
}
