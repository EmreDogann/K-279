using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Utils.Reflection
{
    public static class FastInvoke
    {
        public static Func<T, U> BuildUntypedGetter<T, U>(MemberInfo memberInfo)
        {
            Type targetType = memberInfo.DeclaringType;
            ParameterExpression exInstance = Expression.Parameter(targetType, "t");

            MemberExpression exMemberAccess = Expression.MakeMemberAccess(exInstance, memberInfo); // t.PropertyName
            UnaryExpression
                exConvertToObject =
                    Expression.Convert(exMemberAccess, typeof(U)); // Convert(t.PropertyName, typeof(U))
            var lambda = Expression.Lambda<Func<T, U>>(exConvertToObject, exInstance);

            var action = lambda.Compile();
            return action;
        }

        public static Action<T, object> BuildUntypedSetter<T>(MemberInfo memberInfo)
        {
            Type targetType = memberInfo.DeclaringType;
            ParameterExpression exInstance = Expression.Parameter(targetType, "t");

            MemberExpression exMemberAccess = Expression.MakeMemberAccess(exInstance, memberInfo);

            // t.PropertValue(Convert(p))
            ParameterExpression exValue = Expression.Parameter(typeof(object), "p");
            UnaryExpression exConvertedValue = Expression.Convert(exValue, GetUnderlyingType(memberInfo));
            BinaryExpression exBody = Expression.Assign(exMemberAccess, exConvertedValue);

            var lambda = Expression.Lambda<Action<T, object>>(exBody, exInstance, exValue);
            var action = lambda.Compile();
            return action;
        }

        public static Func<object, T> BuildMemberGetterStaticInstance<T>(Type type, string instanceGetterName,
            string memberName)
        {
            // Entry of the delegate
            ParameterExpression instanceParam = Expression.Parameter(typeof(object), "t");

            // Cast the instance from "object" to the correct type. We don't need it here as we are getting a static instance.
            // UnaryExpression instanceExpr = Expression.TypeAs(instanceParam, type);

            // Get the property's value
            PropertyInfo property = type.GetProperty(instanceGetterName);
            MemberExpression propertyExpr = Expression.Property(null, property);
            MemberExpression fieldExpr = Expression.Field(propertyExpr, type, memberName);
            // UnaryExpression
            //     fieldConvertToInt =
            //         Expression.Convert(fieldExpr, typeof(T)); // Convert(t.PropertyName, typeof(T))

            // Create delegate
            var lambda = Expression.Lambda<Func<object, T>>(fieldExpr, instanceParam);
            return lambda.Compile();
        }

        public static Func<object, T> BuildStaticMemberGetter<T>(Type type, string memberName)
        {
            // Entry of the delegate
            ParameterExpression instanceParam = Expression.Parameter(typeof(object), "t");

            // Get the property's value
            MemberExpression fieldExpr = Expression.Field(null, type, memberName);

            // Create delegate
            var lambda = Expression.Lambda<Func<object, T>>(fieldExpr, instanceParam);
            return lambda.Compile();
        }


        public static Func<object, T> BuildMethodGetterStaticInstance<T>(Type type, string instanceGetterName,
            string methodName)
        {
            // Entry of the delegate
            ParameterExpression instanceParam = Expression.Parameter(typeof(object), "t");

            // Cast the instance from "object" to the correct type. We don't need it here as we are getting a static instance.
            // UnaryExpression instanceExpr = Expression.TypeAs(instanceParam, type);

            // Get the property's value
            PropertyInfo property = type.GetProperty(instanceGetterName);
            MethodInfo method = type.GetMethod(methodName);
            MemberExpression propertyExpr = Expression.Property(null, property);
            MethodCallExpression methodExpr = Expression.Call(propertyExpr, method);
            UnaryExpression
                fieldConvertToInt =
                    Expression.Convert(methodExpr, typeof(T)); // Convert(t.PropertyName, typeof(int))

            // Create delegate
            var lambda = Expression.Lambda<Func<object, T>>(fieldConvertToInt, instanceParam);
            return lambda.Compile();
        }

        private static Type GetUnderlyingType(this MemberInfo member)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Event:
                    return ((EventInfo)member).EventHandlerType;
                case MemberTypes.Field:
                    return ((FieldInfo)member).FieldType;
                case MemberTypes.Method:
                    return ((MethodInfo)member).ReturnType;
                case MemberTypes.Property:
                    return ((PropertyInfo)member).PropertyType;
                default:
                    throw new ArgumentException
                    (
                        "Input MemberInfo must be if type EventInfo, FieldInfo, MethodInfo, or PropertyInfo"
                    );
            }
        }
    }
}