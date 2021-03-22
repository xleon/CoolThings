using DryIoc;
using CoolThings.Business.Features.App;
using CoolThings.Business.Features.Main;
using CoolThings.Business.Features.Scan;
using CoolThings.Business.Foundation;
using CoolThings.Features.Main;
using CoolThings.Features.Scan;

namespace CoolThings.Foundation
{
    public static class AppBootstrap
    {
        public static IContainer AddImmutableServices(this IContainer container)
        {
            container.Register<IViewModelNavigation, XamarinFormsNavigationProxy>(Reuse.Singleton);
            container.Register<IViewBuilder, ViewBuilder>(Reuse.Singleton);
            
            container.Register<App>(Reuse.Singleton);
            container.Register<AppViewModel>(Reuse.Singleton);

            container.RegisterViewModelForView<ScanViewModel, ScanPage>();
            container.RegisterViewModelForView<MainViewModel, MainPage>();
            
            return container;
        }
    }
}