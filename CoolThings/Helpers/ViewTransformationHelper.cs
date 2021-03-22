using Xamarin.Forms;

namespace CoolThings.Helpers
{
    public static class ViewTransformationHelper
    {
        public static Color TransformColor(double t, Color fromColor, Color toColor) =>
            Color.FromRgba(fromColor.R + t * (toColor.R - fromColor.R),
                fromColor.G + t * (toColor.G - fromColor.G),
                fromColor.B + t * (toColor.B - fromColor.B),
                fromColor.A + t * (toColor.A - fromColor.A));
    }
}