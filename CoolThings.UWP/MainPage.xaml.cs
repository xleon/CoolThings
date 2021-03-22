using CoolThings.Business.Foundation;
using CoolThings.Foundation;
using DryIoc;

namespace CoolThings.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            
            Ioc
                .CreateAndroidContainer()
                .AddImmutableServices();

            LoadApplication(Ioc.Container.Resolve<CoolThings.App>());
        }
    }
}
