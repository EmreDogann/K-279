using System;
using System.Collections.Generic;
using System.Reflection;

namespace Utils.Reflection
{
    public static class ReflectionUtils
    {
        public static List<Type> FindAllInitializerTypes<T>(bool limitToCurrentAssembly) where T : Attribute
        {
            var calls = new List<Type>();

            Assembly[] assemblies;
            if (limitToCurrentAssembly)
            {
                assemblies = new[] { typeof(T).Assembly };
            }
            else
            {
                assemblies = AppDomain.CurrentDomain.GetAssemblies();
            }

            foreach (Assembly assembly in assemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    object[] attrs = type.GetCustomAttributes(typeof(T), false);
                    if (attrs.Length == 0)
                    {
                        continue;
                    }

                    calls.Add(type);
                }
            }

            if (calls.Exists(call => calls.Exists(call2 => call != call2)))
            {
                throw new InvalidOperationException($"Found duplicate order for attribute {nameof(T)}");
            }

            return calls;
        }
    }
}