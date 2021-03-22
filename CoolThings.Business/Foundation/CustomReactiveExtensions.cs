using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using DryIoc;
using ReactiveUI;
using Serilog;

namespace CoolThings.Business.Foundation
{
    public static class CustomReactiveExtensions
    {
        public static ReactiveCommand<TParam, TResult> HandleExceptionsWith<TParam, TResult>(
            this ReactiveCommand<TParam, TResult> command, IViewModel viewModel, IScheduler scheduler = null)
        {
            command
                .ThrownExceptions
                .Select(ex => UserMessageModel.Create(ex.Message))
                .SelectMany(model => viewModel.UserMessage.SafeHandle(model))
                .Subscribe();

            return command;
        }
        
        public static IObservable<TOutput> SafeHandle<TInput, TOutput>(
            this Interaction<TInput, TOutput> interaction, 
            TInput model) where TInput : UserMessageModel
        {
            return interaction
                .Handle(model)
                .Catch<TOutput, UnhandledInteractionException<TInput, TOutput>>(ex =>
                {
                    var message = $"Unhandled interaction with message '{model.Message}'";
                    Ioc.Container.Resolve<ILogger>().Error(message);
  
                    return Observable.Return(default(TOutput));
                });
        }
    }
}