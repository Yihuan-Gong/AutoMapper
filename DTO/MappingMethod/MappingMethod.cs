using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MappingMethod
{
    internal abstract class MappingMethod
    {
        public abstract void Map(
            object sourceObject,
            ref object destObject,
            Action<object, object> Mapper = null,
            Dictionary<string, string> map = null
        );
    }
}
