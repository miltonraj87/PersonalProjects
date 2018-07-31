using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Attributes
{
    /// <summary>
    /// This indicates to the unity resolvers that the class is not thread safe and autowiring
    /// should create a new instance everytime
    /// </summary>
    //[ProvideAspectRole(StandardRoles.Caching)]
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class IgnoreAutowireCacheAttribute: Attribute
    {
    }
}
