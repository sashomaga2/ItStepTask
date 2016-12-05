using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ItStepTask.Web.Tests
{
    public static class ObjectExtensions
    {
        public static object GetReflectedProperty(this object obj, string propertyName)
        {
            //obj.ThrowIfNull("obj");
            //propertyName.ThrowIfNull("propertyName");

            PropertyInfo property = obj.GetType().GetProperty(propertyName);

            if (property == null)
            {
                return null;
            }

            return property.GetValue(obj, null);
        }
    }
}
