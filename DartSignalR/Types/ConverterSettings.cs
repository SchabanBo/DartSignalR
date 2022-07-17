using System.Collections.Generic;

namespace DartSignalR.Types {

    public class ConverterSettings {
        
        public bool MapEnumToJsonAsString { get; set; } = true;

        public List<string> SkipMethods { get; set; } = new List<string> {"Mapping"};

    }

}
