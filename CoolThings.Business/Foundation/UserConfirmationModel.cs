using CoolThings.Resources;

namespace CoolThings.Business.Foundation
{
    public class UserConfirmationModel : UserMessageModel
    {
        public string Cancel { get; protected set; }

        public static UserConfirmationModel Create(string message, string title = null, string ok = null, string cancel = null)
        {
            return new UserConfirmationModel
            {
                Title = title,
                Message = message,
                Ok = ok ?? Strings.Alert_yes,
                Cancel = cancel ?? Strings.Alert_no
            };
        }
    }
}