using DartSignalR.Extensions;
using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DartSignalR {

    public partial class ClassGenerator {

        public string GetJsonMethods() {

            if (_request.IsSignalRHub) {
                return string.Empty;
            }

            var from = new StringBuilder("".AddTab());
            var to = new StringBuilder("".AddTab());
            from.Append(ToDartType(_request.Type));
            from.AppendLine(".fromJson(Map<String, dynamic> json):");
            to.AppendLine("Map<String, dynamic> toJson() => {");

            if (_request.KeepRawJson) {
                from.AppendTab(2);
                from.AppendLine("rawJson = json,");
            }

            foreach (var property in _request.Properties) {
                from.AppendTab(2);
                to.AppendTab(2);
                ConvertProperty(property, from, to);
                if (_request.Properties.Last() != property) {
                    from.AppendLine(",");
                    to.AppendLine(",");
                    continue;
                }
                from.AppendLine(";");
                to.AppendLine();
                to.AppendTab();
                to.Append("};");
            }

            var result = from.ToString();
            result += result.AddNewLine(2);
            result += to;
            result += result.AddNewLine();
            return result;
        }

        private void ConvertProperty(PropertyInfo property, StringBuilder from, StringBuilder to) {
            var name = property.Name.ToDartName();
            var type = ToDartType(property.PropertyType);

            if (type.Contains("List")) {
                var subType = property.PropertyType.GetGenericArguments().FirstOrDefault() ?? property.PropertyType.GetElementType();
                var subTypeName = ToDartType(subType);
                from.Append(
                    $" {name} = json['{name}'] == null ? <{subTypeName}>[] : {type}.from(json['{name}'].map((x) =>");
                to.Append($"'{name}': List<dynamic>.from({name}.map((x) =>");

                if (subType is { IsEnum: true }) {
                    from.Append($"{subTypeName}.values.firstWhere((f)=> describeEnum(f.toString()) == x.toString())) as Iterable)");
                    to.Append("describeEnum(x)))");
                    return;
                }

                if (_standardTypes.Contains(subTypeName)) {
                    from.Append("x) as Iterable)");
                    to.Append("x))");
                    return;
                }

                from.Append($"{subTypeName}.fromJson(x as Map<String, dynamic>)) as Iterable)");
                to.Append(" x.toJson()))");
                return;
            }


            if (property.PropertyType.IsEnum) {
                if (ConverterRunner.Settings.MapEnumToJsonAsString) {
                    from.Append($" {name} = {type}.values.firstWhere((f)=> describeEnum(f.toString()) == (json['{name}'] as String))");
                    to.Append($"'{name}': describeEnum({name})");
                    return;
                }
                from.Append($" {name} = {type}.values[json['{name}'] as int]");
                to.Append($"'{name}': {name}.index");
                return;
            }

            if (property.PropertyType == typeof(double)) {
                from.Append($" {name} = json['{name}'] == null ? null : double.parse(json['{name}'].toString())");
                to.Append($"'{name}': {name}");
                return;
            }

            if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?)) {
                from.Append($" {name} = json['{name}'] == null ? null : DateTime.parse(json['{name}'].toString()).toLocal()");
                to.Append($"'{name}': {name}?.toUtc().toIso8601String()");
                return;
            }

            if (property.PropertyType == typeof(TimeSpan)) {
                from.Append($" {name} = json['{name}'] == null ? null : parseDuration(json['{name}'].toString())");
                to.Append($"'{name}': {name}.toString()");
                return;
            }

            if (_standardTypes.Contains(type)) {
                from.Append($" {name} = json['{name}'] == null ? null : json['{name}'] as {type}");
                to.Append($"'{name}': {name}");
                return;
            }

            from.Append($" {name} = json['{name}'] == null ? null : {type}.fromJson(json['{name}'] as Map<String,dynamic>)");
            to.Append($"'{name}': {name}?.toJson()");
        }

    }

}
