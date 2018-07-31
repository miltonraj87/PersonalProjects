using CrossCutting.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Utilities
{
    public class Utility
    {
        public static string CurrentAssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        #region Data
        public static string GetCommand<T>(T source)
        {
            Type entityType = typeof(T);
            var entityStatusProperty = TypeDescriptor.GetProperties(source).Cast<PropertyDescriptor>().Where(p => p.PropertyType == (typeof(EntityStatus))).Single();

            string command = string.Empty;
            EntityStatus entityState = (EntityStatus)entityStatusProperty.GetValue(source);
            TableAttribute table = GetTableAttribute(entityType);
            if (table == null)
                throw new InvalidExpressionException(string.Format("Table name is not provided for the {0} entity", entityType.Name));

            switch (entityState)
            {
                case EntityStatus.Added:
                    command = string.Format(DataResource.Operation_AddCommand, table.Schema, table.Name);
                    break;
                case EntityStatus.Modified:
                    command = string.Format(DataResource.Operation_UpdateCommand, table.Schema, table.Name);
                    break;
                case EntityStatus.Deleted:
                    command = string.Format(DataResource.Operation_DeleteCommand, table.Schema, table.Name);
                    break;
                case EntityStatus.Selected:
                    command = string.Format(DataResource.Query_SelectCommand, table.Schema, table.Name);
                    break;
            }
            return command;
        }

        public static string GetCommand<T>(EntityStatus entityState)
        {
            Type entityType = typeof(T);
            string command = string.Empty;
            TableAttribute table = GetTableAttribute(entityType);
            if (table == null)
                throw new InvalidExpressionException(string.Format("Table name is not provided for the {0} entity", entityType.Name));

            switch (entityState)
            {
                case EntityStatus.Added:
                    command = string.Format(DataResource.Operation_AddCommand, table.Schema, table.Name);
                    break;
                case EntityStatus.Modified:
                    command = string.Format(DataResource.Operation_UpdateCommand, table.Schema, table.Name);
                    break;
                case EntityStatus.Deleted:
                    command = string.Format(DataResource.Operation_DeleteCommand, table.Schema, table.Name);
                    break;
                case EntityStatus.Selected:
                    command = string.Format(DataResource.Query_SelectCommand, table.Schema, table.Name);
                    break;
            }
            return command;
        } 

        public static string GetCommands(EntityStatus entityState, TableAttribute table)
        {
            string command = string.Empty;
            switch (entityState)
            {
                case EntityStatus.Added:
                    command = string.Format(DataResource.Operation_AddCommands, table.Schema, table.Name);
                    break;
                case EntityStatus.Modified:
                    command = string.Format(DataResource.Operation_UpdateCommands, table.Schema, table.Name);
                    break;
                case EntityStatus.Deleted:
                    command = string.Format(DataResource.Operation_DeleteCommands, table.Schema, table.Name);
                    break;
                case EntityStatus.Selected:
                    command = string.Format(DataResource.Query_SelectCommand, table.Schema, table.Name);
                    break;
            }
            return command;
        }

        public static string GetCommands<T>(T source)
        {
            Type entityType = typeof(T);
            var entityStatusProperty = TypeDescriptor.GetProperties(source).Cast<PropertyDescriptor>().Where(p => p.PropertyType == (typeof(EntityStatus))).Single();

            string command = string.Empty;
            EntityStatus entityState = (EntityStatus)entityStatusProperty.GetValue(source);
            TableAttribute table = GetTableAttribute(entityType);
            if (table == null)
                throw new InvalidExpressionException(string.Format("Table name is not provided for the {0} entity", entityType.Name));

            switch (entityState)
            {
                case EntityStatus.Added:
                    command = string.Format(DataResource.Operation_AddCommands, table.Schema, table.Name);
                    break;
                case EntityStatus.Modified:
                    command = string.Format(DataResource.Operation_UpdateCommands, table.Schema, table.Name);
                    break;
                case EntityStatus.Deleted:
                    command = string.Format(DataResource.Operation_DeleteCommands, table.Schema, table.Name);
                    break;
                case EntityStatus.Selected:
                    command = string.Format(DataResource.Query_SelectCommand, table.Schema, table.Name);
                    break;
            }
            return command;
        }

        public static TableAttribute GetTableAttribute(Type source)
        {
            return (TableAttribute)Attribute.GetCustomAttribute(source, typeof(TableAttribute));
        }
        #endregion Data
    }
}
