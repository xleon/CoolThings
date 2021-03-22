using CoolThings.Business.Features.App;
using CoolThings.Business.Features.Main;
using CoolThings.Features.Main;
using CoolThings.Foundation;

namespace CoolThings
{
    public partial class App
    {
        private readonly AppViewModel _appViewModel;

        public App(AppViewModel appViewModel, IViewBuilder viewBuilder)
        {
            _appViewModel = appViewModel;
            
            InitializeComponent();
            
            Sharpnado.Shades.Initializer.Initialize(loggerEnable: true);
            
            MainPage = viewBuilder.Build<MainPage, MainViewModel>();
        }

        protected override void OnStart()
        {
            _appViewModel.CurrentState = AppCycleState.Foreground;
        }

        protected override void OnSleep()
        {
            _appViewModel.CurrentState = AppCycleState.Background;
        }

        protected override void OnResume()
        {
            _appViewModel.CurrentState = AppCycleState.Foreground;
        }
    }
}
