namespace Domain
{
    public static class ApplicationMessage
    {
        public static int UnhandledError = -1;
        public static int TimeoutOccurred = 1;
        public static int UnExpectedHttpResponseReceived = 2;
        public static int InvalidParameter = 3;


        private static readonly Dictionary<int, string> ErrorMessages =
            new Dictionary<int, string>()
            {                
                {UnhandledError, "Unhandled exception."},
                {TimeoutOccurred, "Timeout oluştu."},
                {UnExpectedHttpResponseReceived, "Beklenmedik bir httpCode ile response alındı."},
                {InvalidParameter, "Geçersiz parametre"}


            };

        private static readonly Dictionary<int, string> UserMessages =
            new Dictionary<int, string>()
            { 
                {UnhandledError, "Unhandled exception."},
                {TimeoutOccurred, "Timeout oluştu."},
                {UnExpectedHttpResponseReceived, "Beklenmedik bir httpCode ile response alındı."},
                {InvalidParameter, "Geçersiz parametre"}

            };

        public static string Message(this int code)
        {
            ErrorMessages.TryGetValue(code, out var errorMessage);
            return errorMessage;
        }

        public static string UserMessage(this int code)
        {
            UserMessages.TryGetValue(code, out var errorMessage);
            return errorMessage;
        }
    }
}
