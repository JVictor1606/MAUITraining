﻿using AppTask.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTask.DataBase
{
    public class AppTaskContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
       public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<SubTaskModel> SubTasks {  get; set; }

        public AppTaskContext()
        {
            Database.Migrate();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "apptask.db");

            optionsBuilder.UseSqlite($"Filename={databasePath}");
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
    }


    
}
