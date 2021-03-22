using DryIoc;
using Serilog;
using Serilog.Events;

namespace CoolThings.iOS
{
    public static class IosBootstrap
    {
        public static IContainer AddIos(this IContainer container)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.NSLog(LogEventLevel.Warning)
                .CreateLogger();
            
            container.RegisterInstance(Log.Logger);
            
            return container;
        }
    }
}