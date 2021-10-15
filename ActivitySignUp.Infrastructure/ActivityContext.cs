using ActivitySignUp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace ActivitySignUp.Infrastructure
{
    public class ActivityContext : DbContext
    {
        // private readonly IConfiguration _Config;
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Subscription> Subscription { get; set; }

        public ActivityContext() { }
        public ActivityContext(DbContextOptions<ActivityContext> options) : base(options) { }

        //public ActivityContext(DbContextOptions<ActivityContext> options, IConfiguration config) : base(options) 
        //{ 
        //    _Config = config; 
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseSqlServer(_Config.GetConnectionString("ActivityContextDb"));

            // Hard-coding connection string to test the context
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\ProjectsV13;Initial Catalog=ActivitySignUpDb;Integrated Security=True;Connect Timeout=30;MultipleActiveResultSets=True;");
            
        }
    }
}
