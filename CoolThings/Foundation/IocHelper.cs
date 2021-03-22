using DryIoc;
using ReactiveUI;

namespace CoolThings.Foundation
{
    public static class IocHelper
    {
        public static IContainer RegisterViewModelForView<TViewModel, TView>(this IContainer container)
            where TViewModel : class
            where TView : IViewFor<TViewModel>
        {
            container.Register<TViewModel>();
            container.Register<IViewFor<TViewModel>, TView>();

            return container;
        }
    }
}