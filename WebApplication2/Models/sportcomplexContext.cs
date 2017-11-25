using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApplication2
{
    public partial class sportcomplexContext : DbContext
    {
        public virtual DbSet<Abonement> Abonement { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<Instructor> Instructor { get; set; }
        public virtual DbSet<Schedule> Schedule { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Visitor> Visitor { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=admen;Database=sportcomplex;Trusted_Connection=True;");
            }
        }
        public sportcomplexContext(DbContextOptions<sportcomplexContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Abonement>(entity =>
            {
                entity.Property(e => e.AbonementId).HasColumnName("Abonement_id");

                entity.Property(e => e.NumberOfVisits).HasColumnName("Number_of_visits");
            });

            modelBuilder.Entity<Groups>(entity =>
            {
                entity.HasKey(e => e.GroupId);

                entity.Property(e => e.GroupId).HasColumnName("Group_id");

                entity.Property(e => e.Groupname).HasMaxLength(50);

                entity.Property(e => e.InstructorId).HasColumnName("Instructor_id");

                entity.Property(e => e.NumberOfLessons).HasColumnName("Number_of_lessons");

                entity.Property(e => e.ScheduleId).HasColumnName("Schedule_id");

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.InstructorId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Groups_Instructor");

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.ScheduleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Groups_Schedule");
            });

            modelBuilder.Entity<Instructor>(entity =>
            {
                entity.Property(e => e.InstructorId).HasColumnName("Instructor_id");

                entity.Property(e => e.Education).HasMaxLength(50);

                entity.Property(e => e.Experience).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Qualification).HasMaxLength(50);

                entity.Property(e => e.Specialization).HasMaxLength(50);

                entity.Property(e => e.Surname).HasMaxLength(50);
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.Property(e => e.ScheduleId).HasColumnName("Schedule_id");

                entity.Property(e => e.DaysOfTheWeek)
                    .HasColumnName("Days_of_the_week")
                    .HasMaxLength(50);

                entity.Property(e => e.GroupId).HasColumnName("Group_id");

                entity.Property(e => e.Time).HasColumnType("date");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);
            });

            modelBuilder.Entity<Visitor>(entity =>
            {
                entity.Property(e => e.VisitorId).HasColumnName("Visitor_id");

                entity.Property(e => e.AbonementId).HasColumnName("Abonement_id");

                entity.Property(e => e.GroupId).HasColumnName("Group_id");

                entity.Property(e => e.NameV).HasMaxLength(50);

                entity.Property(e => e.SurnameV).HasMaxLength(50);

                entity.HasOne(d => d.Abonement)
                    .WithMany(p => p.Visitor)
                    .HasForeignKey(d => d.AbonementId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Visitor_Abonement");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Visitor)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Visitor_Groups");
            });
        }
    }
}
