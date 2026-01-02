using System;
using System.Collections.Generic;
using EmployeeManagementSystem.BlogModel;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.BlogDB;

public partial class BlogDBContext : DbContext
{
    public BlogDBContext()
    {
    }

    public BlogDBContext(DbContextOptions<BlogDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblEmployee> TblEmployees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=CHITSHINE\\SQLEXPRESS;Integrated Security=True;Database=EmployeeManagementDB;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblEmployee>(entity =>
        {
            entity.HasKey(e => e.EmployeesId).HasName("PK_Tbl_Employee_1");

            entity.ToTable("Tbl_Employee");

            entity.Property(e => e.EmployeesId).HasMaxLength(50);
            entity.Property(e => e.EmployeeDepartment).HasMaxLength(50);
            entity.Property(e => e.EmployeeEmail).HasMaxLength(50);
            entity.Property(e => e.EmployeeFirstName).HasMaxLength(50);
            entity.Property(e => e.EmployeeFullName).HasMaxLength(80);
            entity.Property(e => e.EmployeeHireDate).HasColumnType("datetime");
            entity.Property(e => e.EmployeePhone).HasMaxLength(20);
            entity.Property(e => e.EmployeePosition).HasMaxLength(50);
            entity.Property(e => e.EmployeeProfilePicture).HasMaxLength(50);
            entity.Property(e => e.EmployeeSalary).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.EmployeesLastName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
