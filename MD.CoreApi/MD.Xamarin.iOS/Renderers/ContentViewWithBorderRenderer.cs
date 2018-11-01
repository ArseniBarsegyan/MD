using MD.Xamarin.Elements;
using MD.Xamarin.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContentViewWithBorder), typeof(ContentViewWithBorderRenderer))]
namespace MD.Xamarin.iOS.Renderers
{
    public class ContentViewWithBorderRenderer : VisualElementRenderer<ContentViewWithBorder>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ContentViewWithBorder> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
                return;

            Layer.CornerRadius = (float)Element.CornerRadius;
            Layer.BorderColor = Element.BorderColor.ToCGColor();
            Layer.BorderWidth = (System.nfloat)Element.BorderWidth;
            Layer.MasksToBounds = true;
        }
    }
}