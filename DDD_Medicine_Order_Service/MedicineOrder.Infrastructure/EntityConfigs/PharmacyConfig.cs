using MedicineOrder.Domain.Entities;
using MedicineOrder.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Infrastructure.EntityConfigs
{
    public class PharmacyConfig : IEntityTypeConfiguration<Pharmacy>
    {
        public void Configure(EntityTypeBuilder<Pharmacy> builder)
        {
            builder.ToTable("Pharmacy");
            builder.HasKey(pharmacy => pharmacy.Id);

            builder.OwnsOne(pharmacy => pharmacy.Contact, contactBuilder =>
            {
                contactBuilder.Property(contact => contact.Email).HasColumnName("Email");
                contactBuilder.Property(contact => contact.NumberPhone).HasColumnName("PhoneNumber");
            })
                .Navigation(pharmcy => pharmcy.Contact)
                .IsRequired()
                .AutoInclude();
            builder.OwnsOne(pharmacy => pharmacy.Address, addressBuilder =>
            {
                addressBuilder.Property(address => address.Address1)
                              .HasColumnName("Address1");

                addressBuilder.Property(address => address.Address2)
                              .HasColumnName("Address2");

                addressBuilder.Property(address => address.Suburb)
                              .HasColumnName("Suburb");

                addressBuilder.Property(address => address.PostCode)
                              .HasColumnName("PostCode");

                addressBuilder.Property(address => address.State)
                              .HasColumnName("State");
            })
           .Navigation(instructor => instructor.Address)
           .IsRequired()
           .AutoInclude();

            builder.Property(pharmacy => pharmacy.Name).IsRequired();
            builder.Property(pharmacy => pharmacy.CreatedUtc).IsRequired();
            builder.Property(pharmacy => pharmacy.Id).ValueGeneratedNever();
        }
    }
}
