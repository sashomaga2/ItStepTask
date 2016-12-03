using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItStepTask.Common.Caching
{
    public interface ICacheService
    {
        T Get<T>(string itemName, Func<T> getData, int durationInSeconds);

       // TResult Get<T, TResult>(string itemName, Func<T, TResult> getData, T param, int durationInSeconds);
    }
}
