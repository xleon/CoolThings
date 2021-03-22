using DryIoc;

namespace CoolThings.Business.Foundation
{
    public static class Ioc
    {
        public static IContainer Container { get; private set; }

        public static IContainer CreateAndroidContainer()
        {
            Container?.Dispose();
            Container = new Container(rules => rules
                .WithDefaultIfAlreadyRegistered(IfAlreadyRegistered.Keep)
                .WithoutThrowOnRegisteringDisposableTransient());
            
            return Container;
        }

        public static IContainer CreateIosContainer()
        {
            Container?.Dispose();
            Container = new Container(rules => rules
                .WithDefaultIfAlreadyRegistered(IfAlreadyRegistered.Keep)
                .WithUseInterpretation());
            return Container;
        }
        
        public static IContainer CreateTestContainer()
        {
            Container?.Dispose();
            Container = new Container(rules => rules
                .WithDefaultIfAlreadyRegistered(IfAlreadyRegistered.Replace)
                .WithoutThrowOnRegisteringDisposableTransient());
            return Container;
        }
    }
}