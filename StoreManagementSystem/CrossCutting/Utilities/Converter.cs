using CrossCutting.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;

namespace CrossCutting.Utilities
{
    public class Converter<Source> : IConverter<Source>
        where Source : class
    {
        public virtual SqlParameter[] ConvertToSqlParameter(Source source)
        {
            var entityProperties = TypeDescriptor.GetProperties(source).Cast<PropertyDescriptor>().Where(p => p.PropertyType != (typeof(EntityStatus)));
            List<SqlParameter> results = new List<SqlParameter>();
            foreach (var property in entityProperties)
            {
                results.Add(new SqlParameter(string.Format("@{0}", property.Name), property.GetValue(source)));
            }

            return results.ToArray<SqlParameter>();
        }
    }

    public class Converter<Source, Target> : IConverter<Source, Target>
        where Source : class
        where Target : class, new()
    {
        public virtual Target Convert(Source input)
        {
            Target result = input.ConvertTo<Target>();
            return result;
        }

        public virtual SqlParameter[] ConvertToSqlParameter(Source source)
        {
            var entityProperties = TypeDescriptor.GetProperties(source).Cast<PropertyDescriptor>();
            List<SqlParameter> results = new List<SqlParameter>();
            foreach (var property in entityProperties)
            {
                results.Add(new SqlParameter(property.Name, property.GetValue(source)));
            }

            return results.ToArray<SqlParameter>();
        }
    }

    public static partial class Converter
    {
        public static object ToType(this string source, Type propertyType)
        {
            var underlyingType = Nullable.GetUnderlyingType(propertyType);
            if (underlyingType == null)
                return Convert.ChangeType(source, propertyType, CultureInfo.InvariantCulture);
            return String.IsNullOrEmpty(source)
                   ? null
                   : Convert.ChangeType(source, underlyingType, CultureInfo.InvariantCulture);
        }

        public static object ToType(this object source, Type propertyType)
        {
            var underlyingType = Nullable.GetUnderlyingType(propertyType);
            if (underlyingType == null)
                return Convert.ChangeType(source, propertyType, CultureInfo.InvariantCulture);
            return source == null
                   ? null
                   : Convert.ChangeType(source, underlyingType, CultureInfo.InvariantCulture);
        }

        public static object DbValueToType(this object source, Type propertyType)
        {
            var underlyingType = Nullable.GetUnderlyingType(propertyType);
            if (underlyingType == null)
                return Convert.ChangeType(source, propertyType, CultureInfo.InvariantCulture);
            return source == DBNull.Value
                   ? null
                   : Convert.ChangeType(source, underlyingType, CultureInfo.InvariantCulture);
        }

        public static void SetObjectProperty(this object source, string propertyName, object value)
        {
            PropertyInfo propertyInfo = source.GetType().GetProperty(propertyName);
            // make sure object has the property we are after
            if (propertyInfo != null)
            {
                if (propertyInfo.CanWrite && value != null)
                    propertyInfo.SetValue(source, value, null);
            }
        }

        public static TTarget ConvertTo<TTarget>(this object source) where TTarget : new()
        {
            var convertProperties = TypeDescriptor.GetProperties(typeof(TTarget)).Cast<PropertyDescriptor>();
            var entityProperties = TypeDescriptor.GetProperties(source).Cast<PropertyDescriptor>();

            var convert = new TTarget();

            foreach (var entityProperty in entityProperties)
            {
                var property = entityProperty;
                var convertProperty = convertProperties.FirstOrDefault(prop => prop.Name == property.Name);
                if (convertProperty != null)
                {
                    convertProperty.SetValue(convert, Convert.ChangeType(entityProperty.GetValue(source), convertProperty.PropertyType));
                }
            }

            return convert;
        }

        public static SqlParameter[] ConvertToSqlParameter(this object source)
        {
            var entityProperties = TypeDescriptor.GetProperties(source).Cast<PropertyDescriptor>();
            List<SqlParameter> results = new List<SqlParameter>();
            foreach (var property in entityProperties)
            {
                results.Add(new SqlParameter(property.Name, property.GetValue(source)));
            }

            return results.ToArray<SqlParameter>();
        }

        public static TTarget CloneObject<TTarget>(this object source) where TTarget : class, new()
        {
            Type t = source.GetType();
            PropertyInfo[] properties = t.GetProperties();

            object p = t.InvokeMember("", System.Reflection.BindingFlags.CreateInstance,
                null, source, null);

            foreach (PropertyInfo pi in properties)
            {
                if (pi.CanWrite)
                {
                    pi.SetValue(p, pi.GetValue(source, null), null);
                }
            }

            return (TTarget)p;
        }

        /// <summary>
        /// Converts String to Any Other Type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static T? ConvertTo<T>(this string input) where T : struct
        {
            T? ret = null;

            if (!string.IsNullOrEmpty(input))
            {
                ret = (T)Convert.ChangeType(input, typeof(T));
            }

            return ret;
        }
        /// <summary>
        /// Converts String to Any Other Type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input">The input.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        public static T? ConvertTo<T>(this string input, IFormatProvider provider) where T : struct
        {
            T? ret = null;
            if (!string.IsNullOrEmpty(input))
            {
                ret = (T)Convert.ChangeType(input, typeof(T), provider);
            }
            return ret;
        }

        public static string ToString(this char? input)
        {
            return input.HasValue ? input.Value.ToString() : String.Empty;
        }

        public static char? ToNullableChar(this string input)
        {
            if (input.Trim().Length == 0)
                return new char?();
            else if (input.Trim().Length > 1)
                throw new ArgumentException("Cannot convert string(" + input.Trim().Length + ") to char?");
            else
                return input[0];
        }

        public static T GetPropertyAttributeFrom<T>(this object instance, string propertyName) where T : Attribute
        {
            var attrType = typeof(T);
            var property = instance.GetType().GetProperty(propertyName);
            return (T)property.GetCustomAttributes(attrType, false).First();
        }

        public static T GetAttribute<T>(Type source) where T : Attribute
        {
            return (T)Attribute.GetCustomAttribute(source, typeof(T));
        }

        public static T StringToEnum<T>(string name)
        {
            return (T)Enum.Parse(typeof(T), name);
        }

        public static string ToDescriptionString(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static IEnumerable<T> EnumToList<T>()
        {
            Type enumType = typeof(T);

            // Can't use generic type constraints on value types,
            // so have to do check like this
            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException("T must be of type System.Enum");

            Array enumValArray = Enum.GetValues(enumType);
            List<T> enumValList = new List<T>(enumValArray.Length);

            foreach (int val in enumValArray)
            {
                enumValList.Add((T)Enum.Parse(enumType, val.ToString()));
            }

            return enumValList;
        }

        public static DataTable ToDataTable<T>(this List<T> iList)
        {
            DataTable dataTable = new DataTable();
            PropertyDescriptorCollection propertyDescriptorCollection =
                TypeDescriptor.GetProperties(typeof(T));
            for (int i = 0; i < propertyDescriptorCollection.Count; i++)
            {
                PropertyDescriptor propertyDescriptor = propertyDescriptorCollection[i];
                Type type = propertyDescriptor.PropertyType;

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    type = Nullable.GetUnderlyingType(type);


                dataTable.Columns.Add(propertyDescriptor.Name, type);
            }
            object[] values = new object[propertyDescriptorCollection.Count];
            foreach (T iListItem in iList)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = propertyDescriptorCollection[i].GetValue(iListItem);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        /// <summary>
        /// Create the data table to be sent up to SQL Server
        /// </summary>
        /// <typeparam name="T">Type of object to be created</typeparam>
        /// <param name="sprocParamObjects">The data to be sent in the table param to SQL Server</param>
        /// <returns></returns>
        public static DataTable CreateDataTable<T>(this IEnumerable<T> sprocParamObjects)
        {
            DataTable table = new DataTable();

            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo property in properties)
            {
                table.Columns.Add(property.Name, property.PropertyType);
            }

            foreach (var sprocParamObject in sprocParamObjects)
            {
                var propertyValues = new List<object>();
                foreach (PropertyInfo property in properties)
                {
                    propertyValues.Add(property.GetValue(sprocParamObject, null));
                }
                table.Rows.Add(propertyValues.ToArray());

                Console.WriteLine(table);
            }
            return table;
        }
    }
}