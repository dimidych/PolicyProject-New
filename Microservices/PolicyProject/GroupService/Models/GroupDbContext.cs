﻿using BaseDbContext;
using InitialConstantsStore;
using Microsoft.EntityFrameworkCore;

namespace GroupService
{
    public class GroupDbContext : BaseDatabaseContext, IGroupDbContext
    {
        public GroupDbContext(DbContextOptions contextOptions) : base(
            contextOptions, "GroupDb")
        {
        }

        public DbSet<Group> Groups { get; set; }

        public static void InitDbContext(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>().HasData(new Group
            {
                GroupId = ConstStore.AdminGroupIdGuid,
                GroupName = ConstStore.AdminLogIn
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            InitDbContext(modelBuilder);
        }
    }
}