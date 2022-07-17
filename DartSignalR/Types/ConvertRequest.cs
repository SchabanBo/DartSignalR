using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DartSignalR.Types {

    public class ConvertRequest {

        public Type Type { get; set; }
        public ConvertRequest? Parent { get; set; }
        public List<MethodInfo> Methods { get; }
        public List<PropertyInfo> Properties { get; }

        public bool IsSignalRHub { get; set; }

        /// <summary>
        /// Set this property to <see langword="true"/> to add new property to the dart class that contains the raw json of the class.
        /// This could help if you have abstract class and you need to keep the original request from the hub.
        /// </summary>
        public bool KeepRawJson { get; set; }

        /// <summary>
        /// Set this to <see langword="true"/> so the properties of the dart class will not have the final word and could be changed in the run time
        /// </summary>
        public bool EditableClass { get; set; }

        public ConvertRequest(
            Type type,
            List<string>? skipMethods = null,
            List<string>? skipProperties = null,
            bool isSignalRHub = false,
            bool keepRawJson = false,
            bool editableClass = false
            ) {
            Type = type;
            IsSignalRHub = isSignalRHub;
            Methods = Type.GetMethods().ToList();
            Properties = Type.GetProperties().ToList();
            skipMethods ??= new List<string>();
            skipProperties ??= new List<string>();
            if (IsSignalRHub) {
                skipProperties.AddRange(new[] { "Clients", "Context", "Groups" });
                skipMethods.AddRange(new[] { "Return", "OnConnectedAsync", "OnDisconnectedAsync", "Dispose" });
            }
            skipMethods.AddRange(new[] { "Equals", "ToString", "GetHashCode", "GetType" });
            var propertiesGetters = Properties.Select(p => $"get_{p.Name}").ToArray();
            var propertiesSetters = Properties.Select(p => $"set_{p.Name}").ToArray();
            Properties.RemoveAll(p => skipProperties.Contains(p.Name));
            Methods.RemoveAll(m => skipMethods.Contains(m.Name));
            Methods.RemoveAll(m => propertiesGetters.Contains(m.Name));
            Methods.RemoveAll(m => propertiesSetters.Contains(m.Name));
            Methods.RemoveAll(m => ConverterRunner.Settings.SkipMethods.Contains(m.Name));
            KeepRawJson = keepRawJson;
            EditableClass = editableClass;
        }
        
        public static ConvertRequest For<T>(
            List<string>? skipMethods = null,
            List<string>? skipProperties = null,
            bool isSignalRHub = false,
            bool keepRawJson = false,
            bool editableClass = false
            ) => new ConvertRequest(typeof(T), skipMethods, skipProperties, isSignalRHub, keepRawJson, editableClass);

    }

}
