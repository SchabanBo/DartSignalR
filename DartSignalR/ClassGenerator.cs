using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DartSignalR.Extensions;
using DartSignalR.Types;

namespace DartSignalR {

    public partial class ClassGenerator {

        private readonly ConvertRequest _request;
        private readonly ConvertResult _result;
        private readonly Action<ConvertRequest,Type> _addToList;
        private readonly List<string> _standardTypes = new List<string>{
            "DateTime",
            "DateTime?",
            "String",
            "int",
            "double",
            "bool",
            "void",
            "Object",
        };

        public ClassGenerator(ConvertRequest request, Action<ConvertRequest, Type> addToList) {
            _request = request;
            _addToList = addToList;
            _result = new ConvertResult(request);
        }

        public ConvertResult Convert() {
            var result = $"class {ToDartType(_request.Type)}" + "{";
            result += result.AddNewLine();
            result += GetConstructor();
            result += result.AddNewLine();
            result += GetMethods();
            result += result.AddNewLine();
            result += GetJsonMethods();
            result += result.AddNewLine();
            _result.DartCode = result + "}";
            return _result;
        }

        private string ToDartType(Type type) {
            if (type == typeof(Task) && _request.IsSignalRHub) {
                return "void";
            }
            if (type.BaseType == typeof(Task) && type.GetGenericArguments().Any()) {
                return $"Future<{ToDartType(type.GetGenericArguments().First())}>";
            }
            if (_standardTypes.Contains(type.Name)) {
                return type.Name;
            }
            if (type.IsNullableType()) {
                return type.GetTypeOfNullable().ToDartType();
            }
            if (type.IsArray) {
                // if result byte[] return it as base64
                if (type.GetElementType() == typeof(byte)) {
                    return "String";
                }
                return $"List<{ToDartType(type.GetElementType()!)}>";
            }
            if (type.IsListType() || type.IsEnumerableType()) {
                return $"List<{ToDartType(type.GetGenericArguments().First())}>";
            }

            _addToList(_request, type);
            if (type.IsGenericType) {
                var result = string.Empty;
                while (type.IsGenericType) {
                    result += type.ToDartType();
                    type = type.GetGenericArguments().First();
                }
                result += type.ToDartType();
                return result;
            }

            return type.ToDartType();
        }

    }

}
