using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public sealed class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Groups> Groups => Set<Groups>();
    public DbSet<Mentor> Mentors => Set<Mentor>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<StudentGroup> StudentGroups => Set<StudentGroup>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.ToTable("courses");
            entity.HasKey(course => course.CourseId);
            entity.Property(course => course.CourseId).HasColumnName("courseid");
            entity.Property(course => course.Title).HasColumnName("title").HasMaxLength(200).IsRequired();
            entity.Property(course => course.Description).HasColumnName("description").HasMaxLength(2000).IsRequired();
            entity.Property(course => course.DurationWeek).HasColumnName("durationweek").IsRequired();
        });

        modelBuilder.Entity<Groups>(entity =>
        {
            entity.ToTable("groups");
            entity.HasKey(group => group.GroupId);
            entity.Property(group => group.GroupId).HasColumnName("groupid");
            entity.Property(group => group.GroupName).HasColumnName("groupname").HasMaxLength(100).IsRequired();
            entity.Property(group => group.CourseId).HasColumnName("courseid");
            entity.Property(group => group.MentorId).HasColumnName("mentorid");
            entity.Property(group => group.StartDate).HasColumnName("startdate");
            entity.Property(group => group.EndDate).HasColumnName("enddate");
            entity.HasOne(group => group.Course)
                .WithMany(course => course.Groups)
                .HasForeignKey(group => group.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(group => group.Mentor)
                .WithMany(mentor => mentor.Groups)
                .HasForeignKey(group => group.MentorId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Mentor>(entity =>
        {
            entity.ToTable("mentors");
            entity.HasKey(mentor => mentor.MentorId);
            entity.Property(mentor => mentor.MentorId).HasColumnName("mentorid");
            entity.Property(mentor => mentor.FullName).HasColumnName("fullname").HasMaxLength(200).IsRequired();
            entity.Property(mentor => mentor.Email).HasColumnName("email").HasMaxLength(254).IsRequired();
            entity.Property(mentor => mentor.Phone).HasColumnName("phone").HasMaxLength(32).IsRequired();
            entity.Property(mentor => mentor.Specialization).HasColumnName("specialization").HasMaxLength(100).IsRequired();
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("students");
            entity.HasKey(student => student.StudentId);
            entity.Property(student => student.StudentId).HasColumnName("studentid");
            entity.Property(student => student.FullName).HasColumnName("fullname").HasMaxLength(200).IsRequired();
            entity.Property(student => student.Email).HasColumnName("email").HasMaxLength(254).IsRequired();
            entity.Property(student => student.Phone).HasColumnName("phone").HasMaxLength(32).IsRequired();
            entity.Property(student => student.EnrollmentDate).HasColumnName("enrollmentdate").IsRequired();
        });

        modelBuilder.Entity<StudentGroup>(entity =>
        {
            entity.ToTable("studentgroups");
            entity.HasKey(membership => membership.StudentGroupId);
            entity.Property(membership => membership.StudentGroupId).HasColumnName("studentgroupid");
            entity.Property(membership => membership.StudentId).HasColumnName("studentid");
            entity.Property(membership => membership.GroupId).HasColumnName("groupid");
            entity.Property(membership => membership.Status)
                .HasColumnName("status")
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();
            entity.HasOne(membership => membership.Student)
                .WithMany(student => student.StudentGroups)
                .HasForeignKey(membership => membership.StudentId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(membership => membership.Group)
                .WithMany(group => group.StudentGroups)
                .HasForeignKey(membership => membership.GroupId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
