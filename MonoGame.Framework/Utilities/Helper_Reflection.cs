
using Microsoft.Xna.Framework.Content;
using System;
using System.IO;
#if WINRT
using System.Reflection;
#endif

namespace Microsoft.Xna.Framework.Utilities
{
    internal static class Helper_Reflection
    {
#if WINRT
        internal static char notSeparator = '/';
        internal static char separator = '\\';
#else
        internal static char notSeparator = '\\';
        internal static char separator = Path.DirectorySeparatorChar;
#endif

        internal static bool IsValueType(ContentTypeReader typeReader)
        {
#if WINRT
            return !typeReader.TargetType.GetTypeInfo().IsValueType;
#else
            return !typeReader.TargetType.IsValueType;
#endif
           
        }

    }
}
