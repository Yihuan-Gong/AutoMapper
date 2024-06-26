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
        //public override void Map(object sourceObject, ref object destObject, Action<object, object> Mapper = null, Dictionary<string, string> map = null)
        //{
        //    destObject = sourceObject;

        //    //destProperty.SetValue(destObject, sourceProperty.GetValue(sourceObject));
        //}
        public override object Map(object sourceObject, Type destType, Func<object, Type, object> Mapper = null, Dictionary<string, string> map = null)
        {
            return sourceObject;
        }
    }
}
