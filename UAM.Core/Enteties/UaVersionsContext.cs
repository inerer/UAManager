using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UAM.Core.Enteties;

public partial class UaVersionsContext : DbContext
{
    public UaVersionsContext()
    {
    }

    public UaVersionsContext(DbContextOptions<UaVersionsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Dependency> Dependencies { get; set; }

    public virtual DbSet<Priority> Priorities { get; set; }

    public virtual DbSet<Problem> Problems { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Version> Versions { get; set; }

    public virtual DbSet<VersionDependency> VersionDependencies { get; set; }

    public virtual DbSet<Worker> Workers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=5.188.140.220;Port=5432;Database=ua_versions;Username=admin;Password=admin");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dependency>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("dependencies_pkey");

            entity.ToTable("dependencies");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Version).HasColumnName("version");
        });

        modelBuilder.Entity<Priority>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("priority_pkey");

            entity.ToTable("priority");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Problem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("problem_pk");

            entity.ToTable("problem");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedTime)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_time");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.PriorityId).HasColumnName("priority_id");
            entity.Property(e => e.ProblemText).HasColumnName("problem_text");
            entity.Property(e => e.Solution).HasColumnName("solution");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.WorkerId).HasColumnName("worker_id");

            entity.HasOne(d => d.Priority).WithMany(p => p.Problems)
                .HasForeignKey(d => d.PriorityId)
                .HasConstraintName("problem_priority_id_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Problems)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("problem_status_id_fkey");

            entity.HasOne(d => d.Worker).WithMany(p => p.Problems)
                .HasForeignKey(d => d.WorkerId)
                .HasConstraintName("problem_worker_id_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_pkey");

            entity.ToTable("role");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("status_pkey");

            entity.ToTable("status");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Version>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("versions_pkey");

            entity.ToTable("versions");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Build).HasColumnName("build");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Path).HasColumnName("path");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("now()")
                .HasColumnName("timestamp");
            entity.Property(e => e.Type).HasColumnName("type");
        });

        modelBuilder.Entity<VersionDependency>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("version_dependencies_pkey");

            entity.ToTable("version_dependencies");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DependenciesId).HasColumnName("dependencies_id");
            entity.Property(e => e.VersionId).HasColumnName("version_id");

            entity.HasOne(d => d.Dependencies).WithMany(p => p.VersionDependencies)
                .HasForeignKey(d => d.DependenciesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("version_dependencies_dependencies_id_fkey");

            entity.HasOne(d => d.Version).WithMany(p => p.VersionDependencies)
                .HasForeignKey(d => d.VersionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("version_dependencies_version_id_fkey");
        });

        modelBuilder.Entity<Worker>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("worker_pkey");

            entity.ToTable("worker");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FullName).HasColumnName("full_name");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Workers)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("worker_role_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
