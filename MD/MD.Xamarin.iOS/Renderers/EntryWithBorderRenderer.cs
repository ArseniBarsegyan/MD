using MD.Xamarin.Elements;
using MD.Xamarin.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(EntryWithBorder), typeof(EntryWithBorderRenderer))]
namespace MD.Xamarin.iOS.Renderers
{
    /// <summary>
    /// Renderer for <see cref="EntryWithBorder"/>
    /// </summary>
    public class EntryWithBorderRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (Element is EntryWithBorder view)
                {
                    view.HeightRequest = 40;
                    Control.Layer.BorderColor = view.BorderColor.ToCGColor();
                    Control.Layer.BorderWidth = 1;
                    Control.Layer.CornerRadius = 5;
                    Control.ClipsToBounds = true;
                }
            }
        }
    }
}