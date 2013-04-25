
using Microsoft.Xna.Framework.Content;
using System;
using System.IO;
#if WINRT
using System.Reflection;
#endif

namespace Microsoft.Xna.Framework.Utilities
{
    internal static class ReflectionHelpers
    {
#if WINRT
        public static char notSeparator = '/';
        public static char separator = '\\';
#else
        public static char notSeparator = '\\';
        public static char separator = Path.DirectorySeparatorChar;
#endif

        public static bool IsValueType(Type targetType)
        {
            if (targetType == null)
            {
                throw new NullReferenceException("Must supply the targetType parameter");
            }
#if WINRT
            return !targetType.GetTypeInfo().IsValueType;
#else
            return !targetType.IsValueType;
#endif
           
        }

        //GetBase Type
        public static Type GetBaseTpye(Type targetType)
        {
            if (targetType == null)
            {
                throw new NullReferenceException("Must supply the targetType parameter");
            }
#if WINRT
            var type = targetType.GetTypeInfo().BaseType;
#else
            var type = targetType.BaseType;
#endif
            return type;
        }

//Test isAbstract
        public static bool IsAbstractClass(Type t)
        {
            if (t == null)
            {
                throw new NullReferenceException("Must supply the t (type) parameter");
            }
#if WINRT
            var ti = t.GetTypeInfo();
            if (ti.IsClass && !ti.IsAbstract)
                return true;
#else
            if (t.IsClass && !t.IsAbstract)
                return true;
#endif
            return false;
            }

//?? Reflective Methods
        public static MethodInfo GetPropertyMethod(PropertyInfo property, string method)
        {
            if (property == null)
            {
                throw new NullReferenceException("Must supply the property parameter");
            }

            MethodInfo methodInfo;
#if WINRT
            if(method == "get")
                methodInfo = property.GetMethod;
            else
                methodInfo = property.SetMethod;
#else
            if(method == "get")
                method = property.GetGetMethod();
            else
                method = property.GetSetMethod();
#endif
            return methodInfo;

        }

// Get custom type from member
        public static Attribute GetCustomAttribute(MemberInfo member, Type memberType)
        {
            if (member == null)
            {
                throw new NullReferenceException("Must supply the member parameter");
            }
            if (memberType == null)
            {
                throw new NullReferenceException("Must supply the memberType parameter");
            }
#if WINRT
            Attribute attr = member.GetCustomAttribute(memberType);
#else
            Attribute attr = Attribute.GetCustomAttribute(member, typeof(memberType));
#endif
            return attr;
        }

//Check for public methods
        public static bool HasPublicProperties(PropertyInfo property)
        {
            if (property == null)
            {
                throw new NullReferenceException("Must supply the property parameter");
            }
#if WINRT
            if ( property.GetMethod != null && !property.GetMethod.IsPublic )
                return true;
            if ( property.SetMethod != null && !property.SetMethod.IsPublic )
                return true;
#else
            foreach (MethodInfo info in property.GetAccessors(true))
            {
                if (info.IsPublic == false)
                    return true;
            }
#endif
            return false;
        }

    }
}
