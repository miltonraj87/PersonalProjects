using CrossCutting.Attributes;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;

namespace CrossCutting.Utilities.Ioc
{
    public class AutowireResolverStrategy : IBuilderStrategy
    {
        private Dictionary<string, object> _instanceCache = new Dictionary<string, object>();

        public void PostBuildUp(IBuilderContext context)
        {
            var baseType = context.Existing.GetType();
            while (baseType != null)
            {
                baseType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).ForEach(field =>
                {
                    try
                    {
                        object[] attrs = field.GetCustomAttributes(GetAttributeType(), true);
                        if (attrs != null && attrs.Length > 0)
                        {
                            object value = Resolve(attrs[0], field, context);
                            field.SetValue(context.Existing, value);
                        }
                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine("Error while resolving property.\r\n" + e.StackTrace);
                        throw;
                    }
                });
                baseType = baseType.BaseType;
            }
        }

        public void PostTearDown(IBuilderContext context)
        {
        }

        public void PreBuildUp(IBuilderContext context)
        {
        }

        public void PreTearDown(IBuilderContext context)
        {
        }

        #region Private Methods
        private Type GetAttributeType()
        {
            return typeof(AutowireAttribute);
        }

        private object Resolve(object attr, FieldInfo field, IBuilderContext context)
        {
            Trace.WriteLine(string.Format("Resolving Autowired property - {0} of type {1}", field.Name, context.Existing.GetType()));
            object returnval = null;
            AutowireAttribute autowireAttr = attr as AutowireAttribute;
            var ignoreCache = context.Existing.GetType().GetCustomAttribute<IgnoreAutowireCacheAttribute>();
            if (ignoreCache == null)
            {
                var interfaces = context.Existing.GetType().GetInterfaces();
                foreach (var iface in interfaces)
                {
                    var currentObjectCacheKey = iface.FullName + "|" + (context.BuildKey.Name ?? "");
                    if (!_instanceCache.ContainsKey(currentObjectCacheKey)) _instanceCache[currentObjectCacheKey] = context.Existing;
                }
            }

            string cacheKey = field.FieldType.FullName + "|" + (autowireAttr.ScopeName ?? "");
            Trace.WriteLine("Cache key = {0}", cacheKey);
            if (_instanceCache.ContainsKey(cacheKey)) return _instanceCache[cacheKey];
            Trace.WriteLine("Autowired instance Not found in cache");

            if (string.IsNullOrWhiteSpace(autowireAttr.ScopeName))
            {
                try
                {
                    Trace.WriteLine("Resolving in default scope");
                    returnval = context.NewBuildUp(new NamedTypeBuildKey(field.FieldType));
                }
                catch (Exception)
                {
                    Trace.WriteLine("Could not get concrete object");
                    throw;
                }
            }
            else
            {
                Trace.WriteLine(string.Format("Resolving in {0} scope", autowireAttr.ScopeName));
                returnval = context.NewBuildUp(new NamedTypeBuildKey(field.FieldType, autowireAttr.ScopeName));
            }

            if (returnval != null)
            {
                ignoreCache = returnval.GetType().GetCustomAttribute<IgnoreAutowireCacheAttribute>();
                if (ignoreCache == null) _instanceCache[cacheKey] = returnval;
            }
            return returnval;
        }
        #endregion Private Methods
    }
}