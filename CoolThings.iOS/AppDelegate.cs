using CoolThings.Business.Foundation;
using CoolThings.Foundation;
using DryIoc;
using Foundation;
using UIKit;

namespace CoolThings.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Ioc
                .CreateIosContainer()
                .AddIos()
                .AddImmutableServices();
            
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(Ioc.Container.Resolve<App>());

            return base.FinishedLaunching(app, options);
        }
    }
}
