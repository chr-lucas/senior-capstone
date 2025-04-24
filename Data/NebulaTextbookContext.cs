using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NebulaTextbook.Models;

namespace NebulaTextbook.Data
{
    public class NebulaTextbookContext : DbContext
    {
        public NebulaTextbookContext (DbContextOptions<NebulaTextbookContext> options)
            : base(options)
        {
        }

        public DbSet<NebulaTextbook.Models.User> User { get; set; } = default!;
        public DbSet<NebulaTextbook.Models.Enrollment> Enrollment { get; set; } = default!;
        public DbSet<NebulaTextbook.Models.Chapter> Chapter { get; set; } = default!;
        public DbSet<NebulaTextbook.Models.Textbook> Textbook { get; set; } = default!;
        public DbSet<NebulaTextbook.Models.Role> Role { get; set; } = default!;
        public DbSet<NebulaTextbook.Models.UserRole> UserRole { get; set; } = default!;
        public DbSet<NebulaTextbook.Models.Video> Video { get; set; } = default!;
        public DbSet<NebulaTextbook.Models.Course> Course { get; set; } = default!;
        public DbSet<NebulaTextbook.Models.Organization> Organization { get; set; } = default!;
        public DbSet<NebulaTextbook.Models.Question> Question { get; set; } = default!;
        public DbSet<NebulaTextbook.Models.StudentQuizScore> StudentQuizScore { get; set; } = default!;
        public DbSet<NebulaTextbook.Models.Quiz> Quiz { get; set; } = default!;

    }
}
