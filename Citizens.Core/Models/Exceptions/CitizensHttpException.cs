

namespace Citizens.Core
{
    public class CitizensHttpException : CitizensException
    {
        public CitizensHttpException(string message, string responseMessage) : base(message)
        {
            this.AdditionObject = responseMessage;
        }
        public string AdditionObject { get; set; }
    }
}
