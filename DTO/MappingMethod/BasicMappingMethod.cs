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
        public override object Map(object sourceObject, Type destType, Func<object, Type, object> Mapper = null, Dictionary<string, string> map = null)
        {
            return sourceObject;
        }
    }
}
