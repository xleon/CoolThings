using System;
using CoolThings.Business.Foundation;

namespace CoolThings.Foundation
{
    public interface IViewBuilder
    {
        TView Build<TView, TViewModel>(object paramsObj = null)
            where TViewModel : class, IViewModel
            where TView : class;

        TView BuildNonGeneric<TView>(
            Type viewModelType, 
            object paramsObj = null,
            IViewModelNavigation navigation = null)
            where TView : class;
    }
}