using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using Microsoft.AspNet.Identity.EntityFramework;
using Mutual.Portal.Core.Entities;
using Mutual.Portal.Core.Entities.Common;
using Mutual.Portal.Core.Entities.Nursing;

namespace Mutual.Portal.Core.Persistence
{
    public class OperationContext : IdentityDbContext, IOperationDbContext
    {
        public OperationContext() : base("name=ConnString")
        {
            //Database.SetInitializer<DbContext>(null);
            //Configuration.LazyLoadingEnabled = false;
            //Configuration.ProxyCreationEnabled = true;
        }

        public int Save()
        {
            int effrected = 0;
            try
            {
                effrected = SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        // Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                throw new ModelValidationException(dbEx.ToString());
            }
            return effrected;
        }

        public Task<int> SaveAsync()
        {
            throw new NotImplementedException();
        }

        public IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });

            modelBuilder.Entity<MyConfiguration>().HasKey(r => r.Id);

            modelBuilder.Entity<TestEntity>().HasKey(r => r.Id);
            modelBuilder.Entity<User>().HasKey(r => r.Id);
            modelBuilder.Entity<Hospital>().HasKey(r => r.Id);
            modelBuilder.Entity<Nurse>().HasKey(r => r.Id);
            modelBuilder.Entity<DreamHospital>().HasKey(r => r.Id);
        }

    }
}
