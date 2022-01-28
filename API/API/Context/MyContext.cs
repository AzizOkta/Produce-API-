using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Context
{
    public class MyContext: DbContext 
    { 
        public MyContext(DbContextOptions<MyContext> options): base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Profiling> Profilings { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(a => a.Account)
                .WithOne(b => b.Employee)
                .HasForeignKey<Account>(b => b.NIK);

            modelBuilder.Entity<Account>()
                .HasOne(a => a.Profiling)
                .WithOne(b => b.Account)
                .HasForeignKey<Profiling>(b => b.NIK);

            modelBuilder.Entity<Profiling>()
            .HasOne(e => e.Education)
            .WithMany(c => c.Profilings);
          
            modelBuilder.Entity<Education>()
            .HasOne(e => e.University)
            .WithMany(c => c.Educations);

          modelBuilder.Entity<AccountRole>()
        .HasKey(ac => new { ac.Id_Account, ac.Id_Role });
         modelBuilder.Entity<AccountRole>()
                .HasOne(ac => ac.Account)
                .WithMany(rl =>rl.AccountRoles)
                .HasForeignKey(bc => bc.Id_Account);
            modelBuilder.Entity<AccountRole>()
                .HasOne(bc => bc.Role)
                .WithMany(c => c.AccountRole)
                .HasForeignKey(bc => bc.Id_Role);


        }


    }
}
