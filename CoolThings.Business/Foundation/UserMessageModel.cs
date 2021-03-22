using CoolThings.Resources;

namespace CoolThings.Business.Foundation
{
    public class UserMessageModel
    {
        public string Title { get; protected set; }
        public string Message { get; protected set; }
        public string Ok { get; protected set; }
        
        public static UserMessageModel Create(string message, string title = null, string ok = null)
        {
            return new UserMessageModel
            {
                Title = title,
                Message = message,
                Ok = ok ?? Strings.Alert_ok
            };
        }
    }
}