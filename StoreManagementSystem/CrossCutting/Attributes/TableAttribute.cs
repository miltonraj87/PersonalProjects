using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class TableAttribute : Attribute
    {
        public string Schema { get; set; }
        public string Name { get; set; }

        public TableAttribute() { }

        public TableAttribute(string Name)
        {
            this.Name = Name;
        }

        public TableAttribute(string Schema, string Name)
        {
            this.Schema = Schema;
            this.Name = Name;
        }
    }
}
