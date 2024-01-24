using MedicineOrder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Infrastructure.EntityConfigs
{
    public class MedicineConfig : IEntityTypeConfiguration<Medicine>
    {
        public void Configure(EntityTypeBuilder<Medicine> builder)
        {
            builder.ToTable("Medicine");
            builder.HasKey(medicine => medicine.Id);
            builder.Property(medicine => medicine.Name).IsRequired().HasMaxLength(200);
            builder.Property(medicine => medicine.Price).IsRequired().HasPrecision(18,2);
            builder.Property(medicine => medicine.CreatedUtc).IsRequired();
            builder.Navigation(or => or.MedicineOrders).AutoInclude();
            builder.Property(medicine => medicine.Id).ValueGeneratedNever();
        }
    }
}
