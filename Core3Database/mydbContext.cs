using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Database
{
    public class mydbContext : DbContext
    {
        public DbSet<tbl_Login> tbl_Logins { get; set; }
        public DbSet<tbl_Users> tbl_Users { get; set; }

        public mydbContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder.UseSqlite("Data source = db.db"));
        }
    }
}
