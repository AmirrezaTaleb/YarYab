using YarYab.Common.Helper;
using YarYab.Common.Utilities;
using YarYab.Domain;
using YarYab.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace YarYab.Data
{

    public class YarYabContext : DbContext
    {
        public YarYabContext(DbContextOptions<YarYabContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Gender).IsRequired().HasMaxLength(10);
                entity.Property(e => e.ChanelId).HasMaxLength(100);
                entity.Property(e => e.IsDeleted).IsRequired();

                entity.HasOne(e => e.City)
                      .WithMany(c => c.Users)
                      .HasForeignKey(e => e.CityId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.IsDeleted).IsRequired();
                entity.Property(e => e.ParentId).IsRequired(false); // Make ParentId nullable
                entity.HasOne(e => e.Parent)
                      .WithMany(p => p.Children)
                      .HasForeignKey(e => e.ParentId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Status).IsRequired().HasMaxLength(50);
                entity.Property(e => e.RequestMessage).HasMaxLength(500);
                entity.Property(e => e.IsDeleted).IsRequired();

                entity.HasOne(e => e.Sender)
                      .WithMany(u => u.RequestsSend)
                      .HasForeignKey(e => e.SenderId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(e => e.Receiver)
                      .WithMany(u => u.RequestsRecive)
                      .HasForeignKey(e => e.ReceiverId)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UserId).IsRequired() ;
                entity.Property(e => e.FriendId).IsRequired() ;
                 entity.Property(e => e.IsDeleted).IsRequired();

                entity.HasOne(e => e.User)
                      .WithMany(u => u.Contacts)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.NoAction);

             });
            FilterConfigurationDeleted(modelBuilder);
            var entitiesAssembly = typeof(IEntity).Assembly;
            modelBuilder.RegisterAllEntities<IEntity>(entitiesAssembly);
            modelBuilder.RegisterEntityTypeConfiguration(entitiesAssembly);
            //modelBuilder.AddRestrictDeleteBehaviorConvention();
            //modelBuilder.AddSequentialGuidForIdConvention();
            modelBuilder.AddPluralizingTableNameConvention();
            base.OnModelCreating(modelBuilder);
        }
        public override int SaveChanges()
        {
            _cleanString();
            return base.SaveChanges();
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            _cleanString();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            _cleanString();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
        private void _cleanString()
        {
            var changedEntities = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
            foreach (var item in changedEntities)
            {
                if (item.Entity == null)
                    continue;

                var properties = item.Entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string));

                foreach (var property in properties)
                {
                    var propName = property.Name;
                    var val = (string)property.GetValue(item.Entity, null);

                    if (val.HasValue())
                    {
                        var newVal = val.Fa2En().FixPersianChars();
                        if (newVal == val)
                            continue;
                        property.SetValue(item.Entity, newVal, null);
                    }
                }
            }
        }

        internal void FilterConfigurationDeleted(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Request>().HasQueryFilter(p => p.IsDeleted == false);
            modelBuilder.Entity<User>().HasQueryFilter(p => p.IsDeleted == false);
            modelBuilder.Entity<City>().HasQueryFilter(p => p.IsDeleted == false);
            modelBuilder.Entity<Contact>().HasQueryFilter(p => p.IsDeleted == false);

        }

    }
}
