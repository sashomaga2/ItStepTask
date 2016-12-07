using ItStepTask.Entity;
using ItStepTask.Data.Repositories;

namespace ItStepTask.Data
{
    public interface ITaskData
    {
        IRepository<ApplicationUser> Users { get; }

        IRepository<Item> Items { get; }

        IRepository<Category> Categories { get; }

        IRepository<ShoppingCart> ShoppingCart { get; }

        IRepository<Supplier> Suppliers { get; }

        IRepository<Order> Orders { get; }


        IRepository<T> GetRepository<T>() where T:class;
    }
}
