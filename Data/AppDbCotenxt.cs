﻿using Microsoft.EntityFrameworkCore;
using MiniTodo.Models;
 
namespace MiniTodo.Data
{
    public class AppDbCotenxt : DbContext
    {
        public DbSet<Todo> Todos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite("DataSource=app.db;Cache=Shared");
    }
}
