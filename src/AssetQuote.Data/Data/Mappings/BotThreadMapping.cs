using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssetQuote.Data.Data.Mappings;

public class BotThreadMapping : IEntityTypeConfiguration<BotThread>
{
    public void Configure(EntityTypeBuilder<BotThread> builder)
    {
        builder.ToTable("BotThreads");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.ChatId).IsRequired();
        builder.Property(p => p.BotStep).HasConversion<string>();

        builder
            .HasMany(p => p.Assets)
            .WithMany(p => p.BotThreads)
            .UsingEntity<Dictionary<string, object>>(
            "AssetBotThread",
            p => p.HasOne<Asset>().WithMany().HasForeignKey("AssetId"),
            p => p.HasOne<BotThread>().WithMany().HasForeignKey("BotThreadId"),
            p =>
            {
                p.Property<Guid>("Id");
                p.Property<DateTime>("CreatedAt");
                p.Property<DateTime>("updatedAt");
                p.Property<bool>("Active");
            });

        builder.HasIndex(p => p.ChatId).HasDatabaseName("indx_chat_id").IsUnique();
    }
}
