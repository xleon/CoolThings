using System.Threading.Tasks;

namespace CoolThings.Business.Foundation
{
    public interface IViewModelNavigation
    {
        Task Push<TViewModel>(object paramsObj = null)
            where TViewModel : class, IViewModel;
        
        Task PushModal<TViewModel>(object paramsObj = null)
            where TViewModel : class, IViewModel;
    }
}