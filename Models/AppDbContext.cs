using System;
using Microsoft.EntityFrameworkCore;

namespace StudentManagement.Models
{
    public class AppDbContext:DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        public DbSet<Students> students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Students>().HasData(
                new Students { Id = 1, Name = "Tang San", ClassName = ClassNameEnum.FirsGrade, Email = "TangSan@gmail.com"},
                new Students { Id = 2, Name = "Xiao Wu", ClassName = ClassNameEnum.SecondGrade, Email = "XiaoWu@gmail.com" },
                new Students { Id = 3, Name = "Rong Rong", ClassName = ClassNameEnum.GradeThree, Email = "RongRong@gmail.com" }
                );

        }
    }
}
