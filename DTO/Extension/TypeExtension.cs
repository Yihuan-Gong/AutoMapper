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

            Type underlyingType = Nullable.GetUnderlyingType(type) ?? type;

            if (underlyingType == typeof(string))
                return PropertyType.Basic;

            if (underlyingType.IsArray)
                return PropertyType.Array;

            if (underlyingType.IsEnum)
                return PropertyType.Enum;

            if (underlyingType.IsClass)
                return PropertyType.Class;

            if (typeof(IEnumerable).IsAssignableFrom(underlyingType))
                return PropertyType.Collection;

            return PropertyType.Basic;
        }
    }
}
