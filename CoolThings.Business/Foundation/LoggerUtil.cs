using DryIoc;
using Serilog;

namespace CoolThings.Business.Foundation
{
    public static class LoggerUtil
    {
        public static ILogger Logger(this ILoggerEnabled source) 
            => Ioc
                .Container
                .Resolve<ILogger>()
                .ForContext(source.GetType());
    }
}