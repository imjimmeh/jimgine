using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jimgine.Core.Helpers.ExtensionMethods
{
    public static class ArrayExtensions
    {
        public static int GetFirstEmptyIndex(this object[] a)
        {
            for(var x = 0; x < a.Length; x++)
            {
                if (a[x] == null)
                    return x;
            }

            return -1;
        }

        public static int GetFirstEmptyIndexAndCreateIfNone(this object[] a)
        {
            var firstIndex = a.GetFirstEmptyIndex();
            if(firstIndex == -1)
            {
                Array.Resize(ref a, a.Length * 2);
                firstIndex = a.GetFirstEmptyIndex();
            }

            return firstIndex;
        }
    }
}
