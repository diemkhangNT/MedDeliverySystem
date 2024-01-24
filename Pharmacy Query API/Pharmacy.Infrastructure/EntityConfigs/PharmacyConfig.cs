using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Infrastructure.EntityConfigs
{
    public class PharmacyConfig : IEntityTypeConfiguration<Domain.Entities.Pharmacy>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Pharmacy> builder)
        {
            builder.ToTable("Pharmacy");

            builder.HasKey(pharmacy => pharmacy.Id);
            builder.Property(pharmacy => pharmacy.Name).IsRequired();
            builder.Property(pharmacy => pharmacy.CreatedUtc).IsRequired();
            builder.Property(pharmacy => pharmacy.CreatedBy).IsRequired();
            builder.OwnsOne(pharmacy => pharmacy.Contact, contactBuilder =>
            {
                contactBuilder.Property(contact => contact.Email)
                              .HasColumnName("Email");
                contactBuilder.Property(contact => contact.NumberPhone)
                              .HasColumnName("NumberPhone");
            })
                .Navigation(pharmacy => pharmacy.Contact)
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
                .Navigation(pharmacy => pharmacy.Address)
                .IsRequired()
                .AutoInclude();

            builder.HasData(
                new Domain.Entities.Pharmacy(new Domain.DomainCommands.CreatePharmacyDomainCommand
                {
                    PharmacyId = 1,
                    PharmacyName = "ECO"
                }),
                new Domain.Entities.Pharmacy(new Domain.DomainCommands.CreatePharmacyDomainCommand
                {
                    PharmacyId = 2,
                    PharmacyName = "An Khang"
                }),
                new Domain.Entities.Pharmacy(new Domain.DomainCommands.CreatePharmacyDomainCommand
                {
                    PharmacyId = 3,
                    PharmacyName = "Pharmacity"
                }),
                new Domain.Entities.Pharmacy(new Domain.DomainCommands.CreatePharmacyDomainCommand
                {
                    PharmacyId = 4,
                    PharmacyName = "Long Chau"
                })
            );
            builder.OwnsOne(pharmacy => pharmacy.Contact).HasData(
                new
                {
                    PharmacyId = 1,
                    Email = "eco2020@gmail.com",
                    NumberPhone = "0979797979"
                },
                new
                {
                    PharmacyId = 2,
                    Email = "ankhang@gmail.com",
                    NumberPhone = "0989565656"
                },
                new
                {
                    PharmacyId = 3,
                    Email = "pharmacity@gmail.com",
                    NumberPhone = "0354545454"
                },
                new
                {
                    PharmacyId = 4,
                    Email = "longchau@gmail.com",
                    NumberPhone = "0875757575"
                }
            );
            builder.OwnsOne(pharmacy => pharmacy.Address).HasData(
                new
                {
                    PharmacyId = 1,
                    Address1 = "148 Hoang Hoa Tham, Ward.12, Tan Binh Dist., HCMC",
                    Address2 = "6 Nguyen van Cu, Ward.7, Tan Binh Dist., HCMC",
                    Suburb = "12 Dong Khoi, Ben Tre",
                    State = "HCMC",
                    PostCode = "19010"
                }, 
                new
                {
                    PharmacyId = 2,
                    Address1 = "142 Vuon Chuoi, Ward.5, Dist.3, HCMC",
                    Address2 = "6 Nguyen van Cu, Ward.7, Tan Binh Dist., HCMC",
                    Suburb = "12 Dong Khoi, Ben Tre",
                    State = "HCMC",
                    PostCode = "19320"
                },
                new
                {
                    PharmacyId = 3,
                    Address1 = "148 Hoang Hoa Tham, Ward.12, Dist.3, HCMC",
                    Address2 = "67 Vuon Chuoi, Ward.5, Dist.3, HCMC",
                    Suburb = "12 Dong Khoi, Ben Tre",
                    State = "HCMC",
                    PostCode = "19300"
                },
                new
                {
                    PharmacyId = 4,
                    Address1 = "457 Nguyen Dinh Chieu, Ward.5, Dist.3, HCMC",
                    Address2 = "6 Nguyen van Cu, Ward.7, Tan Binh Dist., HCMC",
                    Suburb = "12 Dong Khoi, Ben Tre",
                    State = "HCMC",
                    PostCode = "19040"
                }
            );
        }
    }
}
