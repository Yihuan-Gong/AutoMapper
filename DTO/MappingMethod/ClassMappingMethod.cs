using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MappingMethod
{
    internal class ClassMappingMethod : MappingMethod
    {
        public override void Map(PropertyInfo sourceProperty, PropertyInfo destProperty, object sourceObject, object destObject, Action<object, object> Mapper = null)
        {
            var sourceValue = sourceProperty.GetValue(sourceObject);
            var destValue = Activator.CreateInstance(destProperty.PropertyType);

            Mapper.Invoke(sourceValue, destValue);
            destProperty.SetValue(destObject, destValue);
        }
    }
}
