using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Util
{
    public static class EnumExtensions
    {
        public static List<T> ToListElements<T>(this T enumType) where T : Enum
        {
            return Enum.GetValues(enumType.GetType()).Cast<T>().ToList();
        }
    }
}
