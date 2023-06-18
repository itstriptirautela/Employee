using EmployeeAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Data
{
    public class employeemanagementContext : DbContext
    {
        public employeemanagementContext(DbContextOptions<employeemanagementContext> options) : base(options)
        {

        }
        public DbSet<UserModel> UserModels { get; set; }
        public DbSet<EmployeeModel> EmployeeModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeModel>().ToTable("tb1_employee");
            modelBuilder.Entity<UserModel>().ToTable("tb1_user");
        }
    }
}
//using System;
//using System.Collections.Generic;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata;
//using EmployeeAPI.Models;

//namespace EmployeeAPI.Data
//{
//    public partial class employeemanagementContext : DbContext
//    {
//        public employeemanagementContext()
//        {
//        }

//        public employeemanagementContext(DbContextOptions<employeemanagementContext> options)
//            : base(options)
//        {
//        }

//        public virtual DbSet<EmployeeModel> EmployeeModels { get; set; } = null!;
//        public virtual DbSet<UserModel> UserModels { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=LTIN284544\\SQLEXPRESS;Database=employeemanagement;Trusted_Connection=True;");
//            }
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<EmployeeModel>(entity =>
//            {
//                entity.ToTable("tb1_employee");

//                entity.Property(e => e.Id).ValueGeneratedNever();

//                entity.Property(e => e.Email).HasMaxLength(50);

//                entity.Property(e => e.FirstName).HasMaxLength(50);

//                entity.Property(e => e.LastName).HasMaxLength(50);

//                entity.Property(e => e.Mobile).HasMaxLength(50);
//            });

//            modelBuilder.Entity<UserModel>(entity =>
//            {
//                entity.ToTable("tb1_user");

//                entity.Property(e => e.Id).ValueGeneratedNever();

//                entity.Property(e => e.FullName).HasMaxLength(50);

//                entity.Property(e => e.Mobile).HasMaxLength(50);

//                entity.Property(e => e.Password).HasMaxLength(50);

//                entity.Property(e => e.UserName).HasMaxLength(50);

//                entity.Property(e => e.UserType).HasMaxLength(50);
//            });

//            OnModelCreatingPartial(modelBuilder);
//        }

//        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//    }
//}
