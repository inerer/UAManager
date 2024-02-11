using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ClientLauncher.Enteties;

public partial class TicketCreatorContext : DbContext
{
    public TicketCreatorContext()
    {
    }

    public TicketCreatorContext(DbContextOptions<TicketCreatorContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Citizenship> Citizenships { get; set; }

    public virtual DbSet<ClientChild> ClientChildren { get; set; }

    public virtual DbSet<ClientInfo> ClientInfos { get; set; }

    public virtual DbSet<PassportInfo> PassportInfos { get; set; }

    public virtual DbSet<Rate> Rates { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<Train> Trains { get; set; }

    public virtual DbSet<TrainType> TrainTypes { get; set; }

    public virtual DbSet<TypeShipping> TypeShippings { get; set; }

    public virtual DbSet<TypeSubscription> TypeSubscriptions { get; set; }

    public virtual DbSet<Wagon> Wagons { get; set; }

    public virtual DbSet<Worker> Workers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=5.188.140.220;Port=5432;Database=ticket_creator;Username=admin;Password=admin");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Citizenship>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("citizenship_pkey");

            entity.ToTable("citizenship");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ClientChild>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("client_child_pkey");

            entity.ToTable("client_child");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClientInfoId).HasColumnName("client_info_id");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(100)
                .HasColumnName("middle_name");

            entity.HasOne(d => d.ClientInfo).WithMany(p => p.ClientChildren)
                .HasForeignKey(d => d.ClientInfoId)
                .HasConstraintName("client_child_client_info_id_fkey");
        });

        modelBuilder.Entity<ClientInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("client_info_pkey");

            entity.ToTable("client_info");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.IsChild)
                .HasDefaultValue(false)
                .HasColumnName("is_child");
            entity.Property(e => e.IsDisabled)
                .HasDefaultValue(false)
                .HasColumnName("is_disabled");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(100)
                .HasColumnName("middle_name");
            entity.Property(e => e.PassportInfoId).HasColumnName("passport_info_id");

            entity.HasOne(d => d.PassportInfo).WithMany(p => p.ClientInfos)
                .HasForeignKey(d => d.PassportInfoId)
                .HasConstraintName("client_info_passport_info_id_fkey");
        });

        modelBuilder.Entity<PassportInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("passport_info_pkey");

            entity.ToTable("passport_info");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CitizenshipId).HasColumnName("citizenship_id");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.Series).HasColumnName("series");

            entity.HasOne(d => d.Citizenship).WithMany(p => p.PassportInfos)
                .HasForeignKey(d => d.CitizenshipId)
                .HasConstraintName("passport_info_citizenship_id_fkey");
        });

        modelBuilder.Entity<Rate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("rate_pkey");

            entity.ToTable("rate");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.PricePerStation).HasColumnName("price_per_station");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_pkey");

            entity.ToTable("role");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("subscription_pkey");

            entity.ToTable("subscription");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClientInfoId).HasColumnName("client_info_id");
            entity.Property(e => e.CountStation).HasColumnName("count_station");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.RateId).HasColumnName("rate_id");

            entity.HasOne(d => d.ClientInfo).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.ClientInfoId)
                .HasConstraintName("subscription_client_info_id_fkey");

            entity.HasOne(d => d.Rate).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.RateId)
                .HasConstraintName("subscription_rate_id_fkey");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ticket_pkey");

            entity.ToTable("ticket");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClientInfoId).HasColumnName("client_info_id");
            entity.Property(e => e.CountStation).HasColumnName("count_station");
            entity.Property(e => e.DateArrival).HasColumnName("date_arrival");
            entity.Property(e => e.DateDeparture).HasColumnName("date_departure");
            entity.Property(e => e.DateRegistration)
                .HasDefaultValueSql("now()")
                .HasColumnName("date_registration");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.RateId).HasColumnName("rate_id");
            entity.Property(e => e.TrainId).HasColumnName("train_id");
            entity.Property(e => e.TypeShippingId).HasColumnName("type_shipping_id");
            entity.Property(e => e.WorkerId).HasColumnName("worker_id");

            entity.HasOne(d => d.ClientInfo).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.ClientInfoId)
                .HasConstraintName("ticket_client_info_id_fkey");

            entity.HasOne(d => d.Rate).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.RateId)
                .HasConstraintName("ticket_rate_id_fkey");

            entity.HasOne(d => d.Train).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.TrainId)
                .HasConstraintName("ticket_train_id_fkey");

            entity.HasOne(d => d.TypeShipping).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.TypeShippingId)
                .HasConstraintName("ticket_type_shipping_id_fkey");

            entity.HasOne(d => d.Worker).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.WorkerId)
                .HasConstraintName("ticket_worker_id_fkey");
        });

        modelBuilder.Entity<Train>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("train_pkey");

            entity.ToTable("train");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.Series).HasColumnName("series");
            entity.Property(e => e.TrainTypeId).HasColumnName("train_type_id");

            entity.HasOne(d => d.TrainType).WithMany(p => p.Trains)
                .HasForeignKey(d => d.TrainTypeId)
                .HasConstraintName("train_train_type_id_fkey");
        });

        modelBuilder.Entity<TrainType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("train_type_pkey");

            entity.ToTable("train_type");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<TypeShipping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("type_shipping_pkey");

            entity.ToTable("type_shipping");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<TypeSubscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("type_subscription_pkey");

            entity.ToTable("type_subscription");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Wagon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("wagon_pkey");

            entity.ToTable("wagon");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.Series).HasColumnName("series");
            entity.Property(e => e.TrainId).HasColumnName("train_id");

            entity.HasOne(d => d.Train).WithMany(p => p.Wagons)
                .HasForeignKey(d => d.TrainId)
                .HasConstraintName("wagon_train_id_fkey");
        });

        modelBuilder.Entity<Worker>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("worker_pkey");

            entity.ToTable("worker");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(100)
                .HasColumnName("middle_name");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.Workers)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("worker_role_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
