using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MappingMethod
{
    internal class ArrayMappingMethod : MappingMethod
    {
        public override object Map(object sourceObject, Type destType, Func<object, Type, object> Mapper = null, Dictionary<string, string> map = null)
        {
            if (Mapper == null)
                return new object();

            var destElementType = GetElementType(destType);

            var destListType = typeof(List<>).MakeGenericType(destElementType);
            var destList = (IList)Activator.CreateInstance(destListType);

            var sourceValue = (IEnumerable)sourceObject;
            if (sourceValue == null)
                return new object();
            foreach (var sourceElementValue in sourceValue)
            {
                var destElementValue = Mapper.Invoke(sourceElementValue, destElementType);
                destList.Add(destElementValue);
            }

            var array = Array.CreateInstance(destElementType, destList.Count);
            destList.CopyTo(array, 0);
            return array;
        }


        //public override void Map(PropertyInfo sourceProperty, PropertyInfo destProperty, object sourceObject, object destObject, Action<object, object> Mapper = null)
        //{
        //    if (Mapper == null)
        //        return;

        //    var destElementType = GetElementType(destProperty.PropertyType);

        //    var destListType = typeof(List<>).MakeGenericType(destElementType);
        //    var destList = (IList)Activator.CreateInstance(destListType);

        //    var sourceValue = (IEnumerable)sourceProperty.GetValue(sourceObject);
        //    if (sourceValue == null)
        //        return;
        //    foreach (var sourceElementValue in sourceValue)
        //    {
        //        var destElementValue = Activator.CreateInstance(destElementType);
        //        Mapper.Invoke(sourceElementValue, destElementValue);

        //        destList.Add(destElementValue);
        //    }

        //    var array = Array.CreateInstance(destElementType, destList.Count);
        //    destList.CopyTo(array, 0);
        //    destProperty.SetValue(destObject, array);
        //}

        private Type GetElementType(Type type)
        {
            if (type.HasElementType)
            {
                return type.GetElementType();
            }

            if (type.IsGenericType)
            {
                return type.GetGenericArguments().First();
            }

            return typeof(object);
        }
    }
}
