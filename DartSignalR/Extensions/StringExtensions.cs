using System;
using System.Text;

namespace DartSignalR.Extensions {
    public static class StringExtensions {

        public static string AddSpace(this string value) => " ";


        public static string AddTab(this string value, int count = 1, int tabLength = 3) => " ".PadRight(count * tabLength);

        public static void AppendTab(this StringBuilder builder, int count = 1, int tabLength = 3) => builder.Append(" ".PadRight(count * tabLength));

        public static string ToDartName(this string name) => $"{name[0].ToString().ToLower()}{name[1..]}";

        public static string AddNewLine(this string value, int count = 1) {
            var result = "";
            for (int i = 0; i < count; i++) {
                result += Environment.NewLine;
            }
            return result;
        }

        public static void AppendLine(this StringBuilder builder, int count) {
            for (var i = 0; i < count; i++) {
                builder.AppendLine();
            }
        }

    }

}
