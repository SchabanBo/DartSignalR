using System.Collections.Generic;
using System.Linq;
using DartSignalR.Extensions;

namespace DartSignalR {

    public partial class ClassGenerator {

        private string GetMethods() {
            if (!_request.IsSignalRHub) {
                return string.Empty;
            }

            var result = string.Empty;
            foreach (var method in _request.Methods) {
                if (method.IsStatic) continue;

                result += result.AddTab();
                result += ToDartType(method.ReturnType);
                result += result.AddSpace();
                result += method.Name;
                result += "(";
                var args = new List<string>();
                foreach (var parameterInfo in method.GetParameters()) {
                    result += ToDartType(parameterInfo.ParameterType);
                    result += result.AddSpace();
                    result += parameterInfo.Name.ToDartName();
                    args.Add(parameterInfo.Name);
                    if (method.GetParameters().Last() != parameterInfo) {
                        result += ", ";
                    }
                }
                result += ")";

                var hasResult = method.ReturnType.GetGenericArguments().Any();
                if (hasResult) result += " async ";
                result += " => ";
                result += result.AddNewLine();
                result += result.AddTab(2);

                var argsString = args.Any() ? $" ,args: [{ string.Join(", ", args)}]" : string.Empty;
                if (!hasResult) {
                    result += $"connection.send(methodName: '{method.Name}' {argsString});";
                    result += result.AddNewLine(2);
                    continue;
                }

                var request = $"await connection.invoke('{method.Name}' {argsString})";
                var generic = method.ReturnType.GetGenericArguments().First();
                var typeGeneric = ToDartType(generic);
                if (generic.IsListType()) {
                    typeGeneric = ToDartType(generic.GetGenericArguments()[0]);
                    result +=
                        $"List<{typeGeneric}>.from(({request}).map((x)=> {typeGeneric}.fromJson(x as Map<String,dynamic>)) as  Iterable);";
                } else if (_standardTypes.Contains(typeGeneric)) {
                    result += request;
                    result += $" as {ToDartType(generic)};";
                } else {
                    result += $"{typeGeneric}.fromJson({request} as Map<String,dynamic>);";
                }

                result += result.AddNewLine(2);
            }

            return result;
        }

    }

}
