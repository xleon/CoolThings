using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using CoolThings.Helpers;
using ReactiveUI;
using Sharpnado.Shades;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;

namespace CoolThings.Features.Main.HandyTabs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HandyButton
    {
        public static readonly BindableProperty SelectedProperty = BindableProperty.Create(
            nameof(Selected),
            typeof(bool),
            typeof(HandyButton));

        public bool Selected
        {
            get => (bool) GetValue(SelectedProperty);
            set => SetValue(SelectedProperty, value);
        }
        
        public static readonly BindableProperty IconPathDataProperty = BindableProperty.Create(
            nameof(IconPathData),
            typeof(Geometry),
            typeof(HandyButton));

        [TypeConverter(typeof(PathGeometryConverter))]
        public Geometry IconPathData
        {
            get => (Geometry) GetValue(IconPathDataProperty);
            set => SetValue(IconPathDataProperty, value);
        }

        public static readonly BindableProperty SelectedColorProperty = BindableProperty.Create(
            nameof(SelectedColor),
            typeof(Color),
            typeof(HandyButton),
            Color.FromHex("#000000"));

        public Color SelectedColor
        {
            get => (Color) GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        public static readonly BindableProperty UnselectedColorProperty = BindableProperty.Create(
            nameof(UnselectedColor),
            typeof(Color),
            typeof(HandyButton),
            Color.FromHex("#B2B2B2"));

        public Color UnselectedColor
        {
            get => (Color) GetValue(UnselectedColorProperty);
            set => SetValue(UnselectedColorProperty, value);
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(HandyButton));

        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public HandyButton()
        {
            InitializeComponent();
            
            // we don't want a default Shade on init
            SetupShades(false);

            this.WhenActivated(disposables =>
            {
                this
                    .WhenAnyValue(x => x.Selected)
                    .Skip(1)
                    .DistinctUntilChanged()
                    .SubscribeOn(RxApp.MainThreadScheduler)
                    .SelectMany(Animate)
                    .Subscribe()
                    .DisposeWith(disposables);
            });
        }

        private Task<bool> Animate(bool selected)
        {
            SetupShades(selected);
            
            OutterCircleEffect.Opacity = 1;
            InnerCircleEffect.Opacity = 0.6;
            
            var (fromColor, toColor) = selected 
                ? (UnselectedColor, SelectedColor) 
                : (SelectedColor, UnselectedColor);
            
            var animation = new Animation
            {
                {0, 1, new Animation(v => FloatingEllipse.TranslationY = v, FloatingEllipse.TranslationY, selected ? -23 : 0, Easing.SpringOut)},
                {0, 0.5, new Animation(v => Label.Opacity = v, Label.Opacity, selected ? 0D : 1.0D)},
                {0, 0.6, new Animation(v => OutterCircleEffect.Scale = v, OutterCircleEffect.Scale, selected ? 1.7 : 1)},
                {0.6, 0.8, new Animation(v => OutterCircleEffect.Opacity = v, OutterCircleEffect.Opacity, 0)},
                {0, 0.6, new Animation(v => InnerCircleEffect.Scale = v, InnerCircleEffect.Scale, selected ? 4 : 1)},
                {0.6, 0.8, new Animation(v => InnerCircleEffect.Opacity = v, InnerCircleEffect.Opacity, 0)},
                {0, 0.9, new Animation(v =>
                {
                    var currentValue = ViewTransformationHelper.TransformColor(v, fromColor, toColor);
                    IconPath.Fill = new SolidColorBrush(currentValue);
                } )}
            };
            
            var tcs = new TaskCompletionSource<bool>();
            
            animation.Commit(this, "selection", 16U, 680, null, (v, finished) => tcs.SetResult(finished));

            return tcs.Task;
        }

        private void SetupShades(bool selected)
        {
            if (selected)
                FloatingEllipseShadows.Shades = new[]
                {
                    new Shade
                    {
                        Color = Color.Black,
                        BlurRadius = 4,
                        Offset = new Point(0,-4),
                        Opacity = 0.06D
                    }
                };
            else
                FloatingEllipseShadows.Shades = new List<Shade>();
        }
    }
}