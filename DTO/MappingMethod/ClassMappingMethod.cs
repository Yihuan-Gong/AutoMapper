using DTO.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MappingMethod
{
    internal class ClassMappingMethod : MappingMethod
    {
        //public override void Map(PropertyInfo sourceProperty, PropertyInfo destProperty, object sourceObject, object destObject, Action<object, object> Mapper = null)
        //{
        //    var sourceValue = sourceProperty.GetValue(sourceObject);
        //    var destValue = Activator.CreateInstance(destProperty.PropertyType);

        //    Mapper.Invoke(sourceValue, destValue);
        //    destProperty.SetValue(destObject, destValue);
        //}
        public override void Map(object sourceObject, ref object destObject, Action<object, object> Mapper = null, Dictionary<string, string> map = null)
        {
            if (Mapper == null)
                return;

            var sourceChildProperties = sourceObject.GetType().GetProperties();
            foreach (PropertyInfo sourceChildProperty in sourceChildProperties)
            {
                string destChildPropertyName = (map.ContainsKey(sourceChildProperty.Name)) ?
                    map[sourceChildProperty.Name] : sourceChildProperty.Name;

                if (sourceChildProperty.GetValue(sourceObject) == null)
                    continue;

                PropertyInfo destChildProperty = destObject.GetType().GetProperty(destChildPropertyName);
                if (destChildProperty == null)
                    continue;

                object sourceChildObject = sourceChildProperty.GetValue(sourceObject);
                object destChildObject = destChildProperty.PropertyType == typeof(string) ?
                    "" : Activator.CreateInstance(destChildProperty.PropertyType);
                Mapper.Invoke(sourceChildObject, destChildObject);
            }
        }
    }
}
