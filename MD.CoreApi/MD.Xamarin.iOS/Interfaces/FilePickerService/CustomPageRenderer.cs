using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Page), typeof(MD.Xamarin.iOS.Interfaces.FilePickerService.CustomPageRenderer))]
namespace MD.Xamarin.iOS.Interfaces.FilePickerService
{
    /// <summary>
    /// Required for PlatfomDocumentPicker for iOS.
    /// </summary>
    public class CustomPageRenderer : PageRenderer
    {
        /// <summary>
        /// Table mapping pages to their renderers.
        /// </summary>
        internal static ConditionalWeakTable<Page, CustomPageRenderer> Renderers = new ConditionalWeakTable<Page, CustomPageRenderer>();

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            if (e.OldElement is Page oldPage)
            {
                Renderers.Remove(oldPage);
            }
            if (e.NewElement is Page newPage)
            {
                Renderers.Add(newPage, this);
            }
        }
    }
}