using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medication.Infrastructure.EntityConfigs
{
    public class MedicationConfig : IEntityTypeConfiguration<Domain.Entities.Medication>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Medication> builder)
        {
            builder.ToTable("Medication");
            builder.HasKey(medicine => medicine.Id);
            builder.Property(medicine => medicine.Name).IsRequired().HasMaxLength(200);
            builder.Property(medicine => medicine.Price).IsRequired().HasPrecision(18, 2);
            builder.Property(medicine => medicine.CreatedUtc).IsRequired();
        }
    }
}
