﻿namespace ItStepTask.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;
    using ItStepTask.Entity;

    public class TaskDbContext : IdentityDbContext
    {
        public TaskDbContext()
            : base("TaskDbConnection")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<TrackingSystemDbContext, Configuration>());
        }

        public IDbSet<ApplicationUser> Users { get; set; }

        public IDbSet<Item> Items { get; set; }

        public IDbSet<ShoppingCart> ShoppingCart { get; set; }

        public IDbSet<Supplier> Suppliers { get; set; }

        public IDbSet<Category> Categories { get; set; }

        public IDbSet<Order> Orders { get; set; }

        public IDbSet<Discount> Discounts { get; set; }

        public static TaskDbContext Create()
        {
            return new TaskDbContext();
        }
    }
}