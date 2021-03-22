using System;
using System.Linq;
using System.Reflection;
using DryIoc;
using CoolThings.Business.Foundation;
using ReactiveUI;

namespace CoolThings.Foundation
{
    public class ViewBuilder : IViewBuilder
    {
        private readonly IContainer _container;

        public ViewBuilder(IContainer container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
        }
        
        public TView Build<TView, TViewModel>(object paramsObj = null)
            where TViewModel : class, IViewModel
            where TView : class
        {
            var view = _container.Resolve<IViewFor<TViewModel>>();
            
            if(!(view is TView typedView))
                throw new TypeAccessException("View type not expected");
            
            var viewModel = ResolveViewModel<TViewModel>(paramsObj);
            viewModel.Navigation = _container.Resolve<IViewModelNavigation>();
            
            view.ViewModel = viewModel;

            return typedView;
        }
        
        public TView BuildNonGeneric<TView>(
            Type viewModelType, 
            object paramsObj = null, 
            IViewModelNavigation navigation = null)
            where TView : class
            => GetType()
                .GetRuntimeMethods()
                .Single(x => x.Name.Equals(nameof(Build)))
                .MakeGenericMethod(typeof(TView), viewModelType)
                .Invoke(this, new []
                {
                    paramsObj, 
                    navigation
                }) as TView;
        
        private TViewModel ResolveViewModel<TViewModel>(object paramsObj = null)
            where TViewModel : IViewModel
        {
            if (paramsObj == null)
                return _container.Resolve<TViewModel>();
            
            ParameterSelector resolveParams = null;

            paramsObj
                .GetType()
                .GetRuntimeProperties()
                .ToList()
                .ForEach(parameter =>
                {
                    resolveParams = resolveParams == null
                        ? Parameters.Of.Name(parameter.Name, _ => parameter.GetValue(paramsObj, null))
                        : resolveParams.Name(parameter.Name, _ => parameter.GetValue(paramsObj, null));
                });

            return _container
                .WithDependencies(resolveParams)
                .Resolve<TViewModel>();
        }
    }
}