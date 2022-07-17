using System;
using System.Collections.Generic;
using System.Linq;
using DartSignalR.Extensions;

namespace DartSignalR {

    public partial class ClassGenerator {

        private string GetConstructor() {
            var result = "";

            if (_request.IsSignalRHub) {
                result += result.AddTab();
                result += "final HubConnection connection;";
                result += result.AddNewLine();
                result += result.AddTab();
                result += "const ";
                result += ToDartType(_request.Type);
                result += "({ required this.connection});";
                result += result.AddNewLine();
                return result;
            }

            if (!_request.Properties.Any()) return result;

            var definitions = new List<string>();

            if (_request.KeepRawJson) {
                result += "final Map<String,dynamic>? rawJson;";
                result += result.AddNewLine();
                definitions.Add("this.rawJson");
            }

            foreach (var p in _request.Properties) {
                var type = ToDartType(p.PropertyType);
                var name = p.Name.ToDartName();
                var definition = $"this.{name}";
                result += result.AddTab();
                if (!_request.EditableClass) result += "final ";
                result += $"{type}";

                if (p.PropertyType.IsEnum) {
                    definition += $" = {type}.{Enum.GetNames(p.PropertyType).First()}";
                } else if (type.Contains("List")) {
                    definition += " = const []";
                } else {
                    result += "?"; // Make property nullable if there is not default value for it
                }

                result += $" {name};";
                result += result.AddNewLine();
                definitions.Add(definition);
            }

            result += result.AddNewLine();
            result += result.AddTab();
            if (!_request.EditableClass) result += "const ";
            result += ToDartType(_request.Type);
            result += "({";
            result += string.Join(", ", definitions);
            result += "});";
            return result;
        }

    }

}
