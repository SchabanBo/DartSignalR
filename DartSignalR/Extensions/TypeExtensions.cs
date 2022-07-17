using System;
using System.Collections;

namespace DartSignalR.Extensions {

    public static class TypeExtensions {

        public static string ToDartType(this Type type) => type.Name switch {
            "Int32" => "int",
            "Void" => "void",
            "Double" => "double",
            "Boolean" => "bool",
            "TimeSpan" => "Duration",
            _ => type.Name.Contains("`") ? type.Name.Remove(type.Name.IndexOf('`')) : type.Name
        };

        public static bool IsNullableType(this Type type) => type.IsGenericType(typeof(Nullable<>));

        public static Type GetTypeOfNullable(this Type type) => type.GenericTypeArguments[0];

        public static bool IsEnumerableType(this Type type) => typeof(IEnumerable).IsAssignableFrom(type);

        public static bool IsListType(this Type type) => typeof(IList).IsAssignableFrom(type);

        public static bool IsGenericType(this Type type, Type genericType) => type.IsGenericType && type.GetGenericTypeDefinition() == genericType;

    }

}
