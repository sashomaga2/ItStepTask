namespace ItStepTask.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using ItStepTask.Data.Repositories;
    using ItStepTask.Entity;

    public class TaskData : ITaskData
    {
        private DbContext context;
        private Dictionary<Type, object> repositories;

        public TaskData()
            : this(new TaskDbContext())
        {
        }

        public TaskData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<ApplicationUser> Users
        {
            get
            {
                return this.GetRepository<ApplicationUser>();
            }
        }

        public IRepository<Post> Posts
        {
            get
            {
                return this.GetRepository<Post>();
            }
        }

        public IRepository<Item> Items
        {
            get
            {
                return this.GetRepository<Item>();
            }
        }

        public IRepository<Category> Categories
        {
            get
            {
                return this.GetRepository<Category>();
            }
        }

        public IRepository<ShoppingCart> ShoppingCart
        {
            get
            {
                return this.GetRepository<ShoppingCart>();
            }
        }

        public IRepository<Supplier> Suppliers
        {
            get
            {
                return this.GetRepository<Supplier>();
            }
        }

        public IRepository<UserData> UserData
        {
            get
            {
                return this.GetRepository<UserData>();
            }
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(Repository<T>), context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IRepository<T>) this.repositories[typeOfRepository];
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }
    }
}
