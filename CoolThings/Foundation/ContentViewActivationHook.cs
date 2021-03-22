using System;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using Xamarin.Forms;

namespace CoolThings.Foundation
{
    public class ContentViewActivationHook : ContentView, IActivatableView, ICanActivate
    {
        public IObservable<Unit> Activated { get; }
        public IObservable<Unit> Deactivated { get; }
        
        protected ContentViewActivationHook()
        {
            var activatedOnce = false;
            
            Deactivated = this
                .WhenAnyValue(x => x.BindingContext, x => x.Parent, x => x.Content)
                .Select(tuple => tuple.Item1 == null || tuple.Item2 == null || tuple.Item3 == null)
                .Where(deactivated => deactivated)
                .TakeWhile(_ => activatedOnce)
                .Select(_ => Unit.Default);
            
            Activated = this
                .WhenAnyValue(x => x.Parent, x => x.Content)
                .Select(tuple => tuple.Item1 != null && tuple.Item2 != null)
                .Where(activated => activated)
                .TakeUntil(Deactivated)
                .Do(_ => activatedOnce = true)
                .Select(_ => Unit.Default);
        }
    }
}