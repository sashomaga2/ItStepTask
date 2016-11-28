namespace ItStepTask.Data
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

        public IDbSet<ApplicationUser> Users
        {
            get;
            set;
        }

        public IDbSet<Post> Posts
        {
            get;
            set;
        }

        public static TaskDbContext Create()
        {
            return new TaskDbContext();
        }

        public System.Data.Entity.DbSet<ItStepTask.Entity.Supplier> Suppliers { get; set; }

    }
}
