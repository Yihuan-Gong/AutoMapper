using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DTO.Enum;
using DTO.MappingMethod;

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
            var result = Map(source, typeof(Tdest));
            return (Tdest)result;
        }

        private object Map(object source, Type destType)
        {
            PropertyType destPropertyType = destType.GetTypeProperty();

            string methodName = $"DTO.MappingMethod.{destPropertyType}MappingMethod";
            var obj = Type.GetType(methodName);
            var mappingMethod = (MappingMethod.MappingMethod)Activator.CreateInstance(obj);
            return mappingMethod.Map(source, destType, Map, map);


            //var sourceProperties = source.GetType().GetProperties();
            //foreach (PropertyInfo sourceProperty in sourceProperties)
            //{
            //    string destPropertyName = (map.ContainsKey(sourceProperty.Name)) ?
            //        map[sourceProperty.Name] : sourceProperty.Name;

            //    if (sourceProperty.GetValue(source) == null)
            //        continue;

            //    PropertyInfo destProperty = dest.GetType().GetProperty(destPropertyName);
            //    if (destProperty == null)
            //        continue;

            //    PropertyType propertyType = destProperty.PropertyType.GetTypeProperty();

            //    string methodName = $"DTO.MappingMethod.{propertyType}MappingMethod";
            //    var obj = Type.GetType(methodName);
            //    var mappingMethod = (MappingMethod.MappingMethod)Activator.CreateInstance(obj);
            //    mappingMethod.Map(sourceProperty, destProperty, source, dest, Map);

            //}
        }
    }
}
