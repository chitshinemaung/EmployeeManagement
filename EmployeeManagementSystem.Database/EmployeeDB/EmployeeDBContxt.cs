using System;
using System.Collections.Generic;
using EmployeeManagementSystem.Database.EmployeementModel;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Database.EmployeeDB;

public partial class EmployeeDBContxt : DbContext
{
    public EmployeeDBContxt()
    {
    }

    public EmployeeDBContxt(DbContextOptions<EmployeeDBContxt> options)
        : base(options)
    {
    }

    public virtual DbSet<TblEmployee> TblEmployees { get; set; }


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
