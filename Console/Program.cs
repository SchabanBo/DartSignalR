using System.Diagnostics;
using System.Text;
using ConsoleApp.ExampleHub;
using DartSignalR;
using DartSignalR.Types;

// Start code generator
var runner = new ConverterRunner();
var requests = new List<ConvertRequest> {
    ConvertRequest.For<ExampleHub>(isSignalRHub: true),
};
var code = runner.Run(requests);
Console.Write(code);

// Write result to a file
var filename = "dart_signalR.g.dart";
File.WriteAllText(filename, code);
Console.WriteLine();

// Write result report
foreach (var convertResult in runner.Results) {
    var map = new StringBuilder(convertResult.Request.Type.Name);
    map.Append(": ");
    var parent = convertResult.Request.Parent;
    while (parent != null) {
        map.Append(parent.Type.Name);
        map.Append(".");
        parent = parent.Parent;
    }
    Console.WriteLine(map.ToString());
}
Console.WriteLine($@"{runner.Results.Count} Type generated");
Console.WriteLine();

// Ask to open the generated file
Console.WriteLine(@"Press o to open the file");
if (Console.ReadLine() == "o") {
    using var process = new Process();
    ProcessStartInfo startInfo = new() {
        WindowStyle = ProcessWindowStyle.Hidden,
        FileName = "cmd.exe",
        Arguments = $"/c code {filename}"
    };
    process.StartInfo = startInfo;
    _ = process.Start();
}
