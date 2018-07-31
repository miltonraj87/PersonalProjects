using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrossCutting.Attributes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public class AutowireAttribute : Attribute
    {
        public string ScopeName { get; private set; }

        public AutowireAttribute() { }

        public AutowireAttribute(string scope)
        {
            this.ScopeName = scope;
        }
    }
}