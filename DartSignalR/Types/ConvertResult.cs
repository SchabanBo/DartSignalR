namespace DartSignalR.Types {

    public class ConvertResult {

        public ConvertRequest Request { get; set; }

        public string DartCode { get; set; }

        public ConvertResult(ConvertRequest request) {
            Request = request;
            DartCode = string.Empty;
        }

    }

}
