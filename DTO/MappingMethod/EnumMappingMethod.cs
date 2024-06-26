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
        //public override void Map(PropertyInfo sourceProperty, PropertyInfo destProperty, object sourceObject, object destObject, Action<object, object> Mapper = null)
        //{
        //    var enumType = Nullable.GetUnderlyingType(destProperty.PropertyType) ?? destProperty.PropertyType;
        //    var parsedEnum = System.Enum.Parse(enumType, sourceProperty.GetValue(sourceObject).ToString());
        //    var enumInstance = Convert.ChangeType(parsedEnum, enumType);
        //    destProperty.SetValue(destObject, enumInstance);
        //}

        public override object Map(object sourceObject, Type destType, Func<object, Type, object> Mapper = null, Dictionary<string, string> map = null)
        {
            var enumType = Nullable.GetUnderlyingType(destType) ?? destType;
            var parsedEnum = System.Enum.Parse(enumType, sourceObject.ToString());
            var enumInstance = Convert.ChangeType(parsedEnum, enumType);
            return enumInstance;
        }
    }
}
