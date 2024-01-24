using MedicineOrder.Domain.Interfaces;
using MedicineOrder.Infrastructure.EntityConfigs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Infrastructure.Contexts
{
    public class MedicineOrderDbContext : DbContext, IUnitOfWork
    {
        public MedicineOrderDbContext(DbContextOptionsBuilder<MedicineOrderDbContext> builder) : base(builder.Options)
        {
        }

        public async Task SaveEntityChangesAsync()
        {
            await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MedicineConfig());
            modelBuilder.ApplyConfiguration(new MedicineOrderConfig());
            modelBuilder.ApplyConfiguration(new OrderConfig());
            modelBuilder.ApplyConfiguration(new PharmacyConfig());
        }
    }
}
