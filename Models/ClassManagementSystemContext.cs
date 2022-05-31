using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Class_Management_System.Models
{
    public partial class ClassManagementSystemContext : DbContext
    {
        public ClassManagementSystemContext()
        {
        }

        public ClassManagementSystemContext(DbContextOptions<ClassManagementSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Mark> Marks { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=Class Management System;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.Property(e => e.Adminid)
                    .ValueGeneratedNever()
                    .HasColumnName("adminID");

                entity.Property(e => e.AdminName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.AdminPassword)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("Class");

                entity.Property(e => e.ClassID)
                    .ValueGeneratedNever()
                    .HasColumnName("ClassID");

                entity.Property(e => e.ClassName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Mark>(entity =>
            {
                entity.HasKey(e => e.MarksId)
                    .HasName("PK__Marks__3B6168DA8BBC7F80");

                entity.Property(e => e.MarksId)
                    .ValueGeneratedNever()
                    .HasColumnName("MarksID");

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

                entity.Property(e => e.TermName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.StudentId)
                    .ValueGeneratedNever()
                    .HasColumnName("StudentID");

                entity.Property(e => e.ClassID).HasColumnName("ClassID");

                entity.Property(e => e.PreviousSchoolName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StudentAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StudentDob)
                    .HasColumnType("date")
                    .HasColumnName("StudentDOB");

                entity.Property(e => e.StudentGender)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StudentName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StudentPassword)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.Property(e => e.SubjectId)
                    .ValueGeneratedNever()
                    .HasColumnName("SubjectID");

                entity.Property(e => e.ClassID).HasColumnName("ClassID");

                entity.Property(e => e.SubjectName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("Teacher");

                entity.Property(e => e.TeacherId)
                    .ValueGeneratedNever()
                    .HasColumnName("TeacherID");

                entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

                entity.Property(e => e.TeacherAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TeacherGender)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TeacherName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TeacherPassword)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
