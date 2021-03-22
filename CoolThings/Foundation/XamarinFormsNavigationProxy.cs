using System;
using System.Threading.Tasks;
using CoolThings.Business.Foundation;
using Xamarin.Forms;

namespace CoolThings.Foundation
{
    public class XamarinFormsNavigationProxy : IViewModelNavigation
    {
        private readonly IViewBuilder _viewBuilder;

        public XamarinFormsNavigationProxy(IViewBuilder viewBuilder)
        {
            _viewBuilder = viewBuilder ?? throw new ArgumentNullException(nameof(viewBuilder));
        }
        
        public Task Push<TViewModel>(object paramsObj = null) 
            where TViewModel : class, IViewModel
        {
            var page = _viewBuilder.Build<Page, TViewModel>(paramsObj);
            return GetNavigation().PushAsync(page, true);
        }

        public Task PushModal<TViewModel>(object paramsObj = null) 
            where TViewModel : class, IViewModel
        {
            var page = _viewBuilder.Build<Page, TViewModel>(paramsObj);
            return GetNavigation().PushModalAsync(page, true);
        }

        private static INavigation GetNavigation()
        {
            // TODO proper implementation supporting current navigation instead of the root
            return Application.Current.MainPage.Navigation;
        }
    }
}