namespace Domain
{
    [Serializable]
    public class BusinessRuleException : Exception
    {
        public int Code { get; set; }
        public new string Message { get; set; }
        public string UserMessage { get; set; }

        public BusinessRuleException()
        {
        }

        public BusinessRuleException(int code, string message, string userMessage)
            : base(message)
        {
            this.Code = code;
            this.Message = message;
            this.UserMessage = userMessage;
        }

        protected BusinessRuleException(string message)
            : base(message)
        {
        }
    }
}
