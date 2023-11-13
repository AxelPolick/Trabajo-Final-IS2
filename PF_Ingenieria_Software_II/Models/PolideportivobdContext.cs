using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PF_Ingenieria_Software_II.Models;

public partial class PolideportivobdContext : DbContext
{
    public PolideportivobdContext()
    {
    }

    public PolideportivobdContext(DbContextOptions<PolideportivobdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alumno> Alumnos { get; set; }

    public virtual DbSet<Bloque> Bloques { get; set; }

    public virtual DbSet<BloqueAlumno> BloqueAlumnos { get; set; }

    public virtual DbSet<Cargo> Cargos { get; set; }

    public virtual DbSet<Contrato> Contratos { get; set; }

    public virtual DbSet<Disciplina> Disciplinas { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Horario> Horarios { get; set; }

    public virtual DbSet<HorarioSesion> HorarioSesions { get; set; }

    public virtual DbSet<Matricula> Matriculas { get; set; }

    public virtual DbSet<MatriculaBloque> MatriculaBloques { get; set; }

    public virtual DbSet<Modulo> Modulos { get; set; }

    public virtual DbSet<ModuloRol> ModuloRols { get; set; }

    public virtual DbSet<Retiro> Retiros { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Sesion> Sesions { get; set; }

    public virtual DbSet<TipoDocumento> TipoDocumentos { get; set; }

    public virtual DbSet<Transaccion> Transaccions { get; set; }

    public virtual DbSet<Tutor> Tutors { get; set; }

    public virtual DbSet<TutorDisciplina> TutorDisciplinas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Modern_Spanish_CI_AS");

        modelBuilder.Entity<Alumno>(entity =>
        {
            entity.ToTable("Alumno");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Usuario).WithMany(p => p.Alumnos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Alumno_Usuario");
        });

        modelBuilder.Entity<Bloque>(entity =>
        {
            entity.ToTable("Bloque");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Ubicacion)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Disciplina).WithMany(p => p.Bloques)
                .HasForeignKey(d => d.DisciplinaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bloque_Disciplina");

            entity.HasOne(d => d.Horario).WithMany(p => p.Bloques)
                .HasForeignKey(d => d.HorarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bloque_Horario");

            entity.HasOne(d => d.Tutor).WithMany(p => p.Bloques)
                .HasForeignKey(d => d.TutorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bloque_Tutor");
        });

        modelBuilder.Entity<BloqueAlumno>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("BloqueAlumno");

            entity.HasOne(d => d.Alumno).WithMany()
                .HasForeignKey(d => d.AlumnoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BloqueAlumno_Alumno");

            entity.HasOne(d => d.Bloque).WithMany()
                .HasForeignKey(d => d.BloqueId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BloqueAlumno_Bloque");
        });

        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.ToTable("Cargo");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Contrato>(entity =>
        {
            entity.ToTable("Contrato");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FechaFin).HasColumnType("date");
            entity.Property(e => e.FechaInicio).HasColumnType("date");
            entity.Property(e => e.Salario).HasColumnType("money");

            entity.HasOne(d => d.Cargo).WithMany(p => p.Contratos)
                .HasForeignKey(d => d.CargoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contrato_Cargo");
        });

        modelBuilder.Entity<Disciplina>(entity =>
        {
            entity.ToTable("Disciplina");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.ToTable("Estado");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FechaActualizacion).HasColumnType("date");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Horario>(entity =>
        {
            entity.ToTable("Horario");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<HorarioSesion>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("HorarioSesion");

            entity.HasOne(d => d.Horario).WithMany()
                .HasForeignKey(d => d.HorarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HorarioSesion_Horario");

            entity.HasOne(d => d.Sesion).WithMany()
                .HasForeignKey(d => d.SesionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HorarioSesion_Sesion");
        });

        modelBuilder.Entity<Matricula>(entity =>
        {
            entity.ToTable("Matricula");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FechaFin).HasColumnType("date");
            entity.Property(e => e.FechaInicio).HasColumnType("date");

            entity.HasOne(d => d.Alumno).WithMany(p => p.Matriculas)
                .HasForeignKey(d => d.AlumnoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Matricula_Alumno");

            entity.HasOne(d => d.Estado).WithMany(p => p.Matriculas)
                .HasForeignKey(d => d.EstadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Matricula_Estado");

            entity.HasOne(d => d.Transaccion).WithMany(p => p.Matriculas)
                .HasForeignKey(d => d.TransaccionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Matricula_Transaccion");
        });

        modelBuilder.Entity<MatriculaBloque>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("MatriculaBloque");

            entity.Property(e => e.PrecioCurso).HasColumnType("money");

            entity.HasOne(d => d.Bloque).WithMany()
                .HasForeignKey(d => d.BloqueId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MatriculaBloque_Bloque");

            entity.HasOne(d => d.Estado).WithMany()
                .HasForeignKey(d => d.EstadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MatriculaBloque_Estado");

            entity.HasOne(d => d.Matricula).WithMany()
                .HasForeignKey(d => d.MatriculaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MatriculaBloque_Matricula");
        });

        modelBuilder.Entity<Modulo>(entity =>
        {
            entity.ToTable("Modulo");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ModuloRol>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ModuloRol");

            entity.HasOne(d => d.Modulo).WithMany()
                .HasForeignKey(d => d.ModuloId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ModuloRol_Modulo");

            entity.HasOne(d => d.Rol).WithMany()
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ModuloRol_Rol1");
        });

        modelBuilder.Entity<Retiro>(entity =>
        {
            entity.ToTable("Retiro");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.Motivo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Alumno).WithMany(p => p.Retiros)
                .HasForeignKey(d => d.AlumnoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Retiro_Alumno");

            entity.HasOne(d => d.Matricula).WithMany(p => p.Retiros)
                .HasForeignKey(d => d.MatriculaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Retiro_Matricula");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.ToTable("Rol");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Sesion>(entity =>
        {
            entity.ToTable("Sesion");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Dias)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HoraFin).HasColumnType("datetime");
            entity.Property(e => e.HoraInicio).HasColumnType("datetime");
        });

        modelBuilder.Entity<TipoDocumento>(entity =>
        {
            entity.ToTable("TipoDocumento");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Transaccion>(entity =>
        {
            entity.ToTable("Transaccion");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Monto).HasColumnType("money");
            entity.Property(e => e.NumeroAutenticacion).HasColumnType("numeric(18, 0)");
        });

        modelBuilder.Entity<Tutor>(entity =>
        {
            entity.ToTable("Tutor");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Contrato).WithMany(p => p.Tutors)
                .HasForeignKey(d => d.ContratoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tutor_Contrato");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Tutors)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tutor_Usuario");
        });

        modelBuilder.Entity<TutorDisciplina>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TutorDisciplina");

            entity.HasOne(d => d.Disciplina).WithMany()
                .HasForeignKey(d => d.DisciplinaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TutorDisciplina_Disciplina");

            entity.HasOne(d => d.Tutor).WithMany()
                .HasForeignKey(d => d.TutorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TutorDisciplina_Tutor");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("Usuario");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Contrasena)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombres)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NroDocumento)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Documento).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.DocumentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_TipoDocumento");

            entity.HasOne(d => d.Estado).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.EstadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Estado");

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Rol1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
