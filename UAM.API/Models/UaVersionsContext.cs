using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UAM.API.Models;

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

    public virtual DbSet<Version> Versions { get; set; }

    public virtual DbSet<VersionDependency> VersionDependencies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ua_versions;Username=admin;Password=admin");

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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
