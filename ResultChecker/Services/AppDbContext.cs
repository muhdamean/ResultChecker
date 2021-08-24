using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ResultChecker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResultChecker.Services
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<CourseLecturer> CourseLecturers { get; set; }
        //public DbSet<HoD> HoDs { get; set; }
        public DbSet<SessionSemester> SessionSemesters { get; set; }
        public DbSet<StateLga> StateLgas { get; set; }
        public DbSet<CourseReg> CourseRegs { get; set; }
        //public DbSet<CourseRegDetails> CourseRegDetails { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<ResultUpload> ResultUploads { get; set; }
        public DbSet<vwUploadedResults> vwUploadedResults { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<vwUploadedResults>()
                .ToView(nameof(vwUploadedResults))
                .HasNoKey();

            //modelBuilder.Entity<vwSubjects>()
            //    .ToView(nameof(vwSubjects))
            //    .HasNoKey();

            //prevent deleting data with foreign key references
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
