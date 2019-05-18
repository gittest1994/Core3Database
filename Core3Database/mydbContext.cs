using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Database
{
    public class mydbContext : DbContext
    {
        public DbSet<tbl_Users> tbl_Users { get; set; }

        public mydbContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = @"Data source = |DataDirectory|\db.db";
            var builder = new SqliteConnectionStringBuilder(connectionString);
            builder.DataSource = builder.DataSource.Replace(@"|DataDirectory|\", AppDomain.CurrentDomain.GetData("DataDirectory") as string);
            connectionString = builder.ToString();

            base.OnConfiguring(optionsBuilder.UseSqlite(connectionString));

        }
    }
}
