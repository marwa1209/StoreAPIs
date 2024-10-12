using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Dmain.Etities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Data.Configurations
{
    internal class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p=>p.Name).HasMaxLength(200).IsRequired();
            builder.Property(p=>p.Description).IsRequired(true);
            builder.Property(p => p.Price).HasColumnType("decimal(8,12)");
            builder.HasOne(p => p.Brand).WithMany().HasForeignKey(p => p.BrandId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(p => p.Type).WithMany().HasForeignKey(p => p.TypeId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
