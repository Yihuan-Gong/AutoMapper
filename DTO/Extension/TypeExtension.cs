using DTO.Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MappingMethod
{
    internal static class TypeExtension
    {
        public static PropertyType GetTypeProperty(this Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            if (type == typeof(string))
                return PropertyType.Basic;

            if (type.IsArray)
                return PropertyType.Array;

            if (type.IsEnum)
                return PropertyType.Enum;

            if (type.IsClass)
                return PropertyType.Class;

            if (typeof(IEnumerable).IsAssignableFrom(type))
                return PropertyType.Collection;

            return PropertyType.Basic;
        }
    }
}
