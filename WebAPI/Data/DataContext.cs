using Data.Configuration;
using Data.Entities;
using Data.Extensions;
using Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Data
{
    public class DataContext : IdentityDbContext<User, Role, Guid, IdentityUserClaim<Guid>,
        UserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }

        public DbSet<Grade> Grades { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<DetailReward> DetailRewards { get; set; }
        public DbSet<DetailDiscipline> DetailDisciplines { get; set; }
        public DbSet<StudentScore> StudentScores { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.AddConfiguration(new UserRoleConfiguration());
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            IEnumerable<EntityEntry> entities = ChangeTracker.Entries().Where(x => x.State == EntityState.Added ||
                                                                                    x.State == EntityState.Modified);

            foreach (EntityEntry item in entities)
            {
                IDateTracking changedOrAddedItem = item.Entity as IDateTracking;
                if (changedOrAddedItem != null)
                {
                    if(item.State == EntityState.Added)
                    {
                        changedOrAddedItem.CreatedDate = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
                    }
                    else
                    {
                        changedOrAddedItem.ModifiedDate = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
                    }
                }
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
