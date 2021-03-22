using System.Reactive.Disposables;
using CoolThings.Business.Foundation;
using ReactiveUI;
using ReactiveUI.XamForms;

namespace CoolThings.Foundation
{
    public abstract class BaseView<TViewModel> : ReactiveContentPage<TViewModel>, ILoggerEnabled
        where TViewModel : class, IViewModel
    {
        protected BaseView()
        {
            this.WhenActivated(disposables =>
            {
                this
                    .BindInteraction(ViewModel, vm => vm.UserMessage, async context =>
                    {
                        await DisplayAlert(
                            context.Input.Title,
                            context.Input.Message,
                            context.Input.Ok);

                        context.SetOutput(true);
                    })
                    .DisposeWith(disposables);

                this
                    .BindInteraction(ViewModel, vm => vm.UserConfirmation, async context =>
                    {
                        var answer = await DisplayAlert(
                            context.Input.Title,
                            context.Input.Message,
                            context.Input.Ok,
                            context.Input.Cancel);

                        context.SetOutput(answer);
                    })
                    .DisposeWith(disposables);
            });
        }
    }
}