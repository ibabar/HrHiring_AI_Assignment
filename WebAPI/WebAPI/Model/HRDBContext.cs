using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace WebAPI.Model
{
    public partial class HRDBContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public HRDBContext()
        {

        }

        public HRDBContext(DbContextOptions<HRDBContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        public virtual DbSet<Candidate> Candidates { get; set; } = null!;
        public virtual DbSet<UserLogin> UserLogins { get; set; } = null!;
        public virtual DbSet<Vacancy> Vacancies { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidate>(entity =>
            {
                entity.ToTable("Candidate");

                entity.Property(e => e.Email).HasMaxLength(250);

                entity.Property(e => e.MobileNumber).HasMaxLength(250);

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.Resume).HasMaxLength(250);

                entity.Property(e => e.VacancyId).HasColumnName("VacancyID");

                entity.HasOne(d => d.ApprovalOneNavigation)
                    .WithMany(p => p.CandidateApprovalOneNavigations)
                    .HasForeignKey(d => d.ApprovalOne)
                    .HasConstraintName("FK_Candidate_UserLogin");

                entity.HasOne(d => d.ApprovalTwoNavigation)
                    .WithMany(p => p.CandidateApprovalTwoNavigations)
                    .HasForeignKey(d => d.ApprovalTwo)
                    .HasConstraintName("FK_Candidate_UserLogin1");
            });

            modelBuilder.Entity<UserLogin>(entity =>
            {
                entity.ToTable("UserLogin");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Role).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(250);
            });

            modelBuilder.Entity<Vacancy>(entity =>
            {
                entity.ToTable("Vacancy");

                entity.HasIndex(e => e.ApprovedCandidate, "IX_Vacancy_ApprovedCandidate");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.OpenPosition).HasMaxLength(250);

                entity.HasOne(d => d.ApprovedCandidateNavigation)
                    .WithMany(p => p.Vacancies)
                    .HasForeignKey(d => d.ApprovedCandidate)
                    .HasConstraintName("FK_Vacancy_Candidate");
            });

            modelBuilder.Entity<UserLogin>().HasData(
     new UserLogin
     {
         Id = 1,
         UserName = "hrm",
         Password = "123456",
         Role = "manager"
     },
      new UserLogin
      {
          Id = 2,
          UserName = "hro",
          Password = "123456",
          Role = "officer"
      },
       new UserLogin
       {
           Id = 3,
           UserName = "hrd",
           Password = "123456",
           Role = "director"
       }
 );

            OnModelCreatingPartial(modelBuilder);
        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
