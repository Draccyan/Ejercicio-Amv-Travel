using Microsoft.EntityFrameworkCore;
using ProyectoLogin.Repos;
namespace ProyectoLogin.Models;

public partial class Context : DbContext
{
    public Context()
    {
    }

    public Context(DbContextOptions<Context> options): base(options)
    {
    }

    public Context(string connectionString) : base(GetOptions(connectionString))
    { }
    private static DbContextOptions GetOptions(string connectionString)
    {
        return new DbContextOptionsBuilder().UseSqlServer(connectionString).Options;
    }

    public virtual DbSet<Usuario> Usuarios { get; set; }
    public virtual DbSet<Cargo> Cargos { get; set; } = null!;
    public virtual DbSet<Empleado> Empleados { get; set; } = null!;
    public virtual DbSet<Tour> Tour { get; set; } = null!;
    public virtual DbSet<Reserva> Reserva { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }


    public IGestorReservas GestorReservas => new GestorReservas(this);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Tour>().HasKey(k => k.Id);
        modelBuilder.Entity<Reserva>().HasKey(k => k.Id);

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__USUARIO__5B65BF97D1F49851");

            entity.ToTable("USUARIO");

            entity.Property(e => e.Clave)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(e => e.IdCargo)
                .HasName("PK__CARGO__6C985625FAABC3E6");

            entity.ToTable("CARGO");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado)
                .HasName("PK__EMPLEADO__CE6D8B9E7F9FE85C");

            entity.ToTable("EMPLEADO");

            entity.Property(e => e.Correo)
                .HasMaxLength(60)
                .IsUnicode(false);

            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(60)
                .IsUnicode(false);

            entity.Property(e => e.Telefono)
                .HasMaxLength(60)
                .IsUnicode(false);

            entity.HasOne(d => d.oCargo)
                .WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdCargo)
                .HasConstraintName("FK_Cargo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}
