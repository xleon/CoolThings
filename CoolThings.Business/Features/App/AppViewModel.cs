using System;
using CoolThings.Business.Foundation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace CoolThings.Business.Features.App
{
    public class AppViewModel : ReactiveObject, ILoggerEnabled
    {
        [Reactive] public AppCycleState CurrentState { get; set; }

        public AppViewModel()
        {
            this
                .WhenAnyValue(x => x.CurrentState)
                .Subscribe(state => this.Logger().Debug($"{nameof(AppCycleState)}: {state}"));
        }
    }
}