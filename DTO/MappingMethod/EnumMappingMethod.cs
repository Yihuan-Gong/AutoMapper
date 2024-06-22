using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MappingMethod
{
    internal class EnumMappingMethod : MappingMethod
    {
        public override void Map(PropertyInfo sourceProperty, PropertyInfo destProperty, object sourceObject, object destObject, Action<object, object> Mapper = null)
        {
            var enumType = destProperty.PropertyType;
            var parsedEnum = System.Enum.Parse(enumType, sourceProperty.GetValue(sourceObject).ToString());
            var enumInstance = Convert.ChangeType(parsedEnum, enumType);
            destProperty.SetValue(destObject, enumInstance);
        }
    }
}
