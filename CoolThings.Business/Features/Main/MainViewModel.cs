using System.Reactive;
using CoolThings.Business.Features.Scan;
using CoolThings.Business.Foundation;
using ReactiveUI;

namespace CoolThings.Business.Features.Main
{
    public class MainViewModel : BaseViewModel
    {
        public ReactiveCommand<Unit, Unit> ScanCommand { get; }

        public MainViewModel()
        {
            ScanCommand = ReactiveCommand
                .CreateFromTask(() => Navigation.Push<ScanViewModel>())
                .HandleExceptionsWith(this);
        }
    }
}