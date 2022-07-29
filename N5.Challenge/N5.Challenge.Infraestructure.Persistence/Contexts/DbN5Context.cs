using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using N5.Challenge.Core.Application.Interfaces;
using N5.Challenge.Core.Domain.Common;
using N5.Challenge.Core.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace N5.Challenge.Infraestructure.Persistence.Contexts
{
    public class DbN5Context : DbContext
    {
        private readonly IDateTimeService _dateTime;
        public DbN5Context(DbContextOptions<DbN5Context> options, IDateTimeService dateTime) : base(options)
        {

            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
        }

        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<PermissionTypes> PermissionTypes { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTime.NowUtc;                        
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.NowUtc;                        
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Permissions>(entity =>
            {
                entity.ToTable("Permisos");                

                entity.Property(p => p.Id)
                      .UseIdentityColumn();

                entity.Property(p => p.NombreEmpleado)
                      .IsRequired()
                      .HasMaxLength(60);

                entity.Property(p => p.ApellidoEmpleado)                
                      .IsRequired()
                      .HasMaxLength(60);

                entity.Property(p => p.FechaPermiso)
                      .IsRequired();

                entity.HasOne(o => o.PermissionTypes)
                     .WithMany(m => m.Permissions)
                     .HasForeignKey(f => f.TipoPermiso);
            });

            modelBuilder.Entity<PermissionTypes>(entity =>
            {
                entity.ToTable("TipoPermisos");

                entity.Property(p => p.Id)
                      .UseIdentityColumn();

                entity.Property(p => p.Descripcion)
                      .IsRequired()
                      .HasMaxLength(150);
            });
        }
    }
}
