using ItStepTask.Entity;
using ItStepTask.Data.Repositories;

namespace ItStepTask.Data
{
    public interface ITaskData
    {
        IRepository<ApplicationUser> Users
        {
            get;
        }

        IRepository<Post> Posts
        {
            get;
        }

        IRepository<T> GetRepository<T>() where T:class;
    }
}
