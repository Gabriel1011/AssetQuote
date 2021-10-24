﻿using AssetQuote.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssetQuote.Data.Mappings
{
    public class AssetMapping : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder.ToTable("Assets");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasColumnType("VARCHAR(200)").IsRequired();
            builder.Property(p => p.Code).HasColumnType("VARCHAR(200)").IsRequired();

            builder.HasIndex(p => p.Code).HasDatabaseName("indx_code_asset");
        }
    }
}
