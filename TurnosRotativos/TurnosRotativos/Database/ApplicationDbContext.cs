using Microsoft.EntityFrameworkCore;
using TurnosRotativos.Models;

namespace TurnosRotativos.Database
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Jornada> Jornadas { get; set; }
        public DbSet<Concepto> Conceptos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Empleado>(empleado =>
            {
                empleado.HasKey(empleado => empleado.Id);

                empleado.Property(empleado => empleado.NroDocumento).IsRequired();
                empleado.Property(empleado => empleado.Nombre).IsRequired();
                empleado.Property(empleado => empleado.Apellido).IsRequired();
                empleado.Property(empleado => empleado.Email).IsRequired();
                empleado.Property(empleado => empleado.FechaNacimiento).IsRequired();
                empleado.Property(empleado => empleado.FechaIngreso).IsRequired();

            });

            modelBuilder.Entity<Concepto>(concepto =>
            {
                concepto.HasKey(concepto => concepto.Id);
            });

            modelBuilder.Entity<Jornada>(jornada =>
            {
                jornada.HasKey(jornada => jornada.Id);

                jornada.HasOne(jornada => jornada.Empleado)
                .WithMany(empleado => empleado.Jornadas)
                .HasForeignKey(jornada => jornada.IdEmpleado);

                jornada.HasOne(jornada => jornada.Concepto)
                .WithMany()
                .HasForeignKey(jornada => jornada.IdConcepto);

            });
        }
    }
}
