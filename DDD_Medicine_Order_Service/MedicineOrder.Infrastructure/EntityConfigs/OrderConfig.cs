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
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            builder.HasKey(order => order.Id);
            builder.Property(o => o.TotalPrice).IsRequired().HasPrecision(18, 2);
            builder.Property(o => o.OrderStatus).HasDefaultValue(false);
            builder.HasOne(o => o.Pharmacy).WithMany(p => p.Orders).HasForeignKey(o => o.PharmacyID);
            builder.Navigation(or => or.MedicineOrders).AutoInclude();
        }
    }
}
