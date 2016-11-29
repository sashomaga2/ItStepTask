using ItStepTask.Entity;
using ItStepTask.Data.Repositories;

namespace ItStepTask.Data
{
    public interface ITaskData
    {
        IRepository<ApplicationUser> Users { get; }
        IRepository<Post> Posts { get; }

        IRepository<Item> Items { get; }

        IRepository<Category> Categories { get; }

        IRepository<ShoppingCart> ShoppingCart { get; }

        IRepository<Supplier> Suppliers { get; }

        IRepository<UserData> UserData { get; }


        IRepository<T> GetRepository<T>() where T:class;
    }
}
