using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    internal class Mapper<Tsource, Tdest>
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

            var sourceFields = typeof(Tsource).GetFields();
            foreach (FieldInfo sourceField in sourceFields)
            {
                string destFieldName = (map.ContainsKey(sourceField.Name)) ?
                    map[sourceField.Name] : sourceField.Name;

                typeof(Tdest).GetField(destFieldName).SetValue(result, sourceField.GetValue(source));
            }

            return result;
        }
    }
}
