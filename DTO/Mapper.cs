using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Mapper<Tsource, Tdest>
        where Tsource : new()
        where Tdest : new()
    {
        private readonly Dictionary<string, string> map;

        public Mapper()
        {
            map = new Dictionary<string, string>();
        }

        public Mapper<Tsource, Tdest> ForMember<Tmember>(
            Expression<Func<Tsource, Tmember>> sourceMemberSelector,
            Expression<Func<Tdest, Tmember>> destMemberSelector)
        {
            var sourceMemberExpression = sourceMemberSelector.Body as MemberExpression;
            var destMemberExpression = destMemberSelector.Body as MemberExpression;
            map.Add(sourceMemberExpression.Member.Name, destMemberExpression.Member.Name);

            return this;
        }



        public Tdest Map(Tsource source)
        {
            Tdest result = new Tdest();
            Map(source, result);
            return result;
        }

        private void Map(object source, object dest)
        {

            var sourceProperties = source.GetType().GetProperties();
            foreach (PropertyInfo sourceProperty in sourceProperties)
            {
                string destPropertyName = (map.ContainsKey(sourceProperty.Name)) ?
                    map[sourceProperty.Name] : sourceProperty.Name;

                PropertyInfo destProperty = dest.GetType().GetProperty(destPropertyName);
                if (destProperty == null)
                    continue;
                else if (sourceProperty.PropertyType == destProperty.PropertyType)
                    destProperty.SetValue(dest, sourceProperty.GetValue(source));
                else if (IsEnumerableType(sourceProperty.PropertyType) && IsEnumerableType(destProperty.PropertyType))
                    MapIEnumerableProperty(sourceProperty, destProperty, source, dest);
                else if (IsComplexType(sourceProperty.PropertyType) && IsComplexType(destProperty.PropertyType))
                    MapComplexProperty(sourceProperty, destProperty, source, dest);
            }
        }

        private void MapComplexProperty(PropertyInfo sourceProperty, PropertyInfo destProperty, object source, object dest)
        {
            var sourceValue = sourceProperty.GetValue(source);
            var destValue = Activator.CreateInstance(destProperty.PropertyType);

            Map(sourceValue, destValue);
        }

        private void MapIEnumerableProperty(PropertyInfo sourceProperty, PropertyInfo destProperty, object source, object dest)
        {
            var sourceElementType = GetElementType(sourceProperty.PropertyType);
            var destElementType = GetElementType(destProperty.PropertyType);

            var destListType = typeof(List<>).MakeGenericType(destElementType);
            var destList = (IList)Activator.CreateInstance(destListType);

            var sourceValue = (IEnumerable)sourceProperty.GetValue(source);
            foreach (var item in sourceValue)
            {
                var destElementValue = Activator.CreateInstance(destElementType);
                Map(item, destElementValue);

                destList.Add(destElementValue);
            }

            //  Array array = Array.CreateInstance(elementType, lengths);

            var destInstance = ConvertToTargetCollectionType(destProperty.PropertyType, destList, destElementType);
            destProperty.SetValue(dest, destInstance);
        }


        private bool IsComplexType(Type type)
        {
            return type.IsClass && type != typeof(string);
        }

        private bool IsEnumerableType(Type type)
        {
            return typeof(IEnumerable).IsAssignableFrom(type) && type != typeof(string);
        }

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

        private object ConvertToTargetCollectionType(Type targetType, IList sourceList, Type elementType)
        {
            if (targetType.IsArray)
            {
                var array = Array.CreateInstance(elementType, sourceList.Count);
                sourceList.CopyTo(array, 0);
                return array;
            }

            if (typeof(IList).IsAssignableFrom(targetType))
            {
                var list = (IList)Activator.CreateInstance(targetType);
                foreach (var item in sourceList)
                {
                    list.Add(item);
                }
                return list;
            }

            if (typeof(IEnumerable).IsAssignableFrom(targetType))
            {
                var enumerableType = typeof(Enumerable);
                var toListMethod = enumerableType.GetMethod("ToList").MakeGenericMethod(elementType);
                return toListMethod.Invoke(null, new object[] { sourceList });
            }

            throw new InvalidOperationException("Unsupported target collection type: " + targetType.FullName);
        }
    }
}
