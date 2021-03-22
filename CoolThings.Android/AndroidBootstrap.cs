using DryIoc;
using Serilog;
using Serilog.Core;

namespace CoolThings.Droid
{
    public static class AndroidBootstrap
    {
        public static IContainer AddAndroid(this IContainer container)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.WithProperty(Constants.SourceContextPropertyName, "CoolThings") //Sets the Tag field.
                .Enrich.FromLogContext()
                .WriteTo.AndroidLog()
                .WriteTo.Debug(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();

            container.RegisterInstance(Log.Logger);
            
            return container;
        }
    }
}