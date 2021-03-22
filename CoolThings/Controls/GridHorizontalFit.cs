using Xamarin.Forms;

namespace CoolThings.Controls
{
    public class GridHorizontalFit : Layout<View>
    {
        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            throw new System.NotImplementedException();
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            return base.OnMeasure(widthConstraint, heightConstraint);
        }

        protected override void InvalidateLayout()
        {
            base.InvalidateLayout();
        }
        
        
    }
}