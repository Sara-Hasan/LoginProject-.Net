﻿using Microsoft.EntityFrameworkCore;

namespace UserApi.Model
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> users { get; set; }
    }
}
