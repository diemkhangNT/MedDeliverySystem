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
    public class MedicineOrderConfig : IEntityTypeConfiguration<Domain.Entities.MedicineOrder>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.MedicineOrder> builder)
        {
            builder.HasKey(m => new { m.OrderID, m.MedicineID });
            builder.HasOne(m => m.Order).WithMany(o => o.MedicineOrders).HasForeignKey(m => m.OrderID);
            builder.HasOne(m => m.Medicine).WithMany(o => o.MedicineOrders).HasForeignKey(m => m.MedicineID);
            builder.Property(m => m.Quantity).IsRequired();
            builder.HasCheckConstraint("CK_DetailsOrder_Quantity", "[Quantity] > = 0");
        }
    }
}
