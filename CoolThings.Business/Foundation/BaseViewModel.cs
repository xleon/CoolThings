using System.Reactive.Concurrency;
using ReactiveUI;

namespace CoolThings.Business.Foundation
{
    public abstract class BaseViewModel : ReactiveObject, IViewModel
    {
        public IViewModelNavigation Navigation { get; set; }
        public Interaction<UserMessageModel, bool> UserMessage { get; set; }
        public Interaction<UserConfirmationModel, bool> UserConfirmation { get; }
        public Interaction<UserConfirmationModel, bool> UserRetryConfirmation { get; }

        protected readonly IScheduler Scheduler;
        
        protected BaseViewModel(IScheduler scheduler = null)
        {
            Scheduler = scheduler ?? RxApp.MainThreadScheduler;
            UserMessage = new Interaction<UserMessageModel, bool>();
            UserConfirmation = new Interaction<UserConfirmationModel, bool>();
            UserRetryConfirmation = new Interaction<UserConfirmationModel, bool>();
        }
    }
}