using System.Reactive;
using CoolThings.Business.Foundation;
using ReactiveUI;

namespace CoolThings.Business.Features.Scan
{
    public class ScanViewModel : BaseViewModel, ILoggerEnabled
    {
        public ReactiveCommand<Unit, Unit> ScanCommand { get; }
        
        public ScanViewModel()
        {
            ScanCommand = ReactiveCommand
                .Create(() => this.Logger().Debug("yep, it works"));
        }
    }
}