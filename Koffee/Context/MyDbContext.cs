using System;
using System.Collections.Generic;
using Koffee.Models;
using Microsoft.EntityFrameworkCore;

namespace Koffee.Context;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Dish> Dishes { get; set; }

    public virtual DbSet<DishList> DishLists { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Server=localhost;port=5432;user id=postgres;password=toor;database=Koffe");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dish>(entity =>
        {
            entity.HasKey(e => e.IdDish).HasName("Dishes_pk");

            entity.Property(e => e.IdDish).UseIdentityAlwaysColumn();
            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<DishList>(entity =>
        {
            entity.HasKey(e => e.IdList).HasName("DishLists_pk");

            entity.Property(e => e.IdList).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.IdDishNavigation).WithMany(p => p.DishLists)
                .HasForeignKey(d => d.IdDish)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DishLists_Dishes_IdDish_fk");

            entity.HasOne(d => d.IdOrderNavigation).WithMany(p => p.DishLists)
                .HasForeignKey(d => d.IdOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DishLists_Orders_IdOrder_fk");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrder).HasName("Orders_pk");

            entity.Property(e => e.IdOrder).UseIdentityAlwaysColumn();
            entity.Property(e => e.Date).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Orders_Users_IdUser_fk");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("Users_pk");

            entity.Property(e => e.IdUser).UseIdentityAlwaysColumn();
            entity.Property(e => e.Login).HasMaxLength(20);
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Password).HasMaxLength(20);
            entity.Property(e => e.Patronymic).HasMaxLength(30);
            entity.Property(e => e.Post).HasMaxLength(20);
            entity.Property(e => e.Surename).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
