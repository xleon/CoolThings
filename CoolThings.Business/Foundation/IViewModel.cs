using ReactiveUI;

namespace CoolThings.Business.Foundation
{
    public interface IViewModel : ILoggerEnabled
    {
        IViewModelNavigation Navigation { get; set; }
        Interaction<UserMessageModel, bool> UserMessage { get; set; }
        Interaction<UserConfirmationModel, bool> UserConfirmation { get; }
        Interaction<UserConfirmationModel, bool> UserRetryConfirmation { get; }
    }
}