using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DartSignalR.Extensions;
using DartSignalR.Types;

namespace DartSignalR {

    public class ConverterRunner {

        public static ConverterSettings Settings { get; set; } = new ConverterSettings();

        public List<ConvertRequest> Requests = new List<ConvertRequest>();

        public List<ConvertResult> Results = new List<ConvertResult>();

        public static List<Type> KnownType { get; } = new List<Type>() {
            typeof(DateTime),
            typeof(DateTime?),
            typeof(TimeSpan),
            typeof(string),
            typeof(int),
            typeof(double),
            typeof(void),
            typeof(object),
            typeof(char),
            typeof(Task),
            typeof(Task<>),
            typeof(Exception),
            typeof(Guid),
            typeof(Type),
            typeof(DateTime[]),
            typeof(string[]),
            typeof(int[]),
            typeof(double[]),
            typeof(object[]),
            typeof(char[]),
            typeof(Task[]),
            typeof(Exception[]),
            typeof(TypeCode),
            typeof(Int32),
            typeof(Int32[]),
            typeof(Int64),
            typeof(Boolean),
            typeof(IFormatProvider),
            typeof(Byte)
        };

        public string Run(List<ConvertRequest> requests) {

            Requests = requests;

            var extras = new ConverterExtras();
            var result = $"// File generated at {DateTime.Now}";
            result += result.AddNewLine();
            result += string.Join(Environment.NewLine, extras.Imports);
            result += result.AddNewLine(2);

            while (Requests.Any()) {
                var request = Requests.First();
                Results.Add(Convert(request));
                result += Results.Last().DartCode;
                result += result.AddNewLine(2);
                Requests.Remove(request);
            }

            result += string.Join(Environment.NewLine, extras.Methods);

            return result;
        }

        public void AddToList(ConvertRequest parent, Type type) {

            if (KnownType.Contains(type) ||
                Requests.Select(r => r.Type).Contains(type) ||
                Results.Select(r => r.Request.Type).Contains(type)) return;

            Requests.Add(new ConvertRequest(type) { Parent = parent });
        }

        private ConvertResult Convert(ConvertRequest request) {
            if (request.Type.IsEnum) {
                return new ConvertResult(request) {
                    DartCode = new EnumGenerator(request.Type).Generate()
                };
            }

            return new ClassGenerator(request, AddToList).Convert();
        }

    }

}
