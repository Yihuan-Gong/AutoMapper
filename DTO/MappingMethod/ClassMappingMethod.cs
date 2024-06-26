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
        public override object Map(object sourceObject, Type destType, Func<object, Type, object> Mapper = null, Dictionary<string, string> map = null)
        {
            if (Mapper == null)
                return new object();

            var sourceChildProperties = sourceObject.GetType().GetProperties();
            var destObject = Activator.CreateInstance(destType);
            foreach (PropertyInfo sourceChildProperty in sourceChildProperties)
            {
                string destChildPropertyName = (map.ContainsKey(sourceChildProperty.Name)) ?
                    map[sourceChildProperty.Name] : sourceChildProperty.Name;

                if (sourceChildProperty.GetValue(sourceObject) == null)
                    continue;

                PropertyInfo destChildProperty = destType.GetProperty(destChildPropertyName);
                if (destChildProperty == null)
                    continue;

                object sourceChildObject = sourceChildProperty.GetValue(sourceObject);
                object destChildObject = Mapper.Invoke(sourceChildObject, destChildProperty.PropertyType);
                destChildProperty.SetValue(destObject, destChildObject);
            }

            return destObject;
        }
    }
}
