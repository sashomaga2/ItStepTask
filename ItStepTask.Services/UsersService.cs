using ItStepTask.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItStepTask.Entity;
using ItStepTask.Data;

namespace ItStepTask.Services
{
    public class UsersService :BaseService<ApplicationUser>, IUsersService
    {
        public UsersService(ITaskData data)
            :base(data)
        {
        }

    }
}
