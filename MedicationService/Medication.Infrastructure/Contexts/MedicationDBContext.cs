using Medication.Domain.Interfaces;
using Medication.Infrastructure.EntityConfigs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medication.Infrastructure.Contexts
{
    public class MedicationDBContext : DbContext, IUnitOfWork
    {
        public MedicationDBContext(DbContextOptionsBuilder<MedicationDBContext> builder) : base(builder.Options)
        {
        }

        public async Task SaveEntityChangesAsync()
        {
            await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MedicationConfig());
        }
    }
}
