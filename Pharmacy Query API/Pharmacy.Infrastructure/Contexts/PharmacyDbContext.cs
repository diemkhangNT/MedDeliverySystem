using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Infrastructure.EntityConfigs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Infrastructure.Contexts
{
    public class PharmacyDbContext : DbContext, IUnitOfWork
    {
        public PharmacyDbContext(DbContextOptionsBuilder<PharmacyDbContext> builder) : base(builder.Options)
        {
        }

        public async Task SaveEntityChangesAsync()
        {
            await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PharmacyConfig());
            modelBuilder.ApplyConfiguration(new MedicationConfig());
        }
    }
}
