namespace DartSignalR {

    public class ConverterExtras {

        /// <summary>
        /// The imports to use in the generated file
        /// </summary>
        public string[] Imports { get; } = {
            "import 'package:flutter/foundation.dart';",
            "import 'package:signalr_core/signalr_core.dart';",
        };

        /// <summary>
        /// Extra help methods to write in the file
        /// </summary>
        public string[] Methods { get; } = {
            @"
Duration parseDuration(String s) {
    var hours = 0;
    var minutes = 0;
    var micros = 0;
    List<String> parts = s.split(':');
    if (parts.length > 2) {
        hours = int.parse(parts[parts.length - 3]);
    }
    if (parts.length > 1) {
        minutes = int.parse(parts[parts.length - 2]);
    }
    micros = (double.parse(parts[parts.length - 1]) * 1000000).round();
    return Duration(hours: hours, minutes: minutes, microseconds: micros);
}
",
        };
    }

}
