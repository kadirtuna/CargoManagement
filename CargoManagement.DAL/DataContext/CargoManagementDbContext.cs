using System;
using System.Collections.Generic;
using CargoManagement.DAL.Models;
using Microsoft.EntityFrameworkCore;

using CargoManagement.DAL.Models;

namespace CargoManagement.DAL.DataContext;

public partial class CargoManagementDbContext : DbContext
{
    public CargoManagementDbContext(DbContextOptions options)
        : base(options)
    {
    }
    public DbSet<Carrier> Carriers { get; set; }    
    public DbSet<Order> Orders { get; set; }    
    public DbSet<CarrierConfiguration> CarrierConfigurations { get; set; }    
    

    /*public CargoManagementDbContext()
    {
    }

    public CargoManagementDbContext(DbContextOptions<CargoManagementDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.IdEmployee).HasName("PK__EMPLOYEE__51C8DD7AC115ECFA");

            entity.ToTable("EMPLOYEE");

            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);*/
}
