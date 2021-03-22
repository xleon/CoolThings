using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoolThings.Features.Main.HandyTabs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HandyMenu
    {
        public IList<View> Items => ButtonsGrid.Children;

        // public ObservableCollection<HandyButton> Items { get; }
        public HandyMenu()
        {
            // Items = new ObservableCollection<HandyButton>();

            InitializeComponent();
            
            this.WhenActivated(disposables =>
            {
                this
                    .WhenAnyValue(x => x.Items)
                    .WhereNotNull()
                    .Where(items => items.Any())
                    .Select(items => items.Cast<HandyButton>().ToList())
                    .Subscribe(buttons => SetupButtons(buttons, disposables));
            });
        }

        private void SetupButtons(List<HandyButton> buttons, CompositeDisposable disposables)
        {
            for (var i = 0; i < buttons.Count; i++)
            {
                var button = buttons[i];

                if(ButtonsGrid.ColumnDefinitions.Count < i + 1)
                {
                    ButtonsGrid
                        .ColumnDefinitions
                        .Add(new ColumnDefinition {Width = GridLength.Star});
                }
                            
                Grid.SetColumn(button, i);
                    
                button
                    .Tap
                    .Events()
                    .Tapped
                    .Subscribe(_ => Select(button))
                    .DisposeWith(disposables);
            }
                        
            buttons
                .First()
                .WhenAnyValue(x => x.Width) // the width of the button is not available right away
                .Where(x => x > 0)
                .Take(1)
                .Subscribe(_ => Select(buttons.First()))
                .DisposeWith(disposables);
        }

        private void Select(HandyButton button)
        {
            var currentActive = Items.Cast<HandyButton>().FirstOrDefault(b => b.Selected);
            if (currentActive != null)
                currentActive.Selected = false;

            button.Selected = true;

            var x = button.X + button.Width * .5 - (ThePath.Width * .5);
            BarShape.TranslateTo(x, ThePath.Y, 480U, Easing.CubicOut);
        }
    }
}