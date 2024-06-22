using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MappingMethod
{
    internal class BasicMappingMethod : MappingMethod
    {
        public override void Map(PropertyInfo sourceProperty, PropertyInfo destProperty, object sourceObject, object destObject, Action<object, object> Mapper = null)
        {
            destProperty.SetValue(destObject, sourceProperty.GetValue(sourceObject));
        }
    }
}
