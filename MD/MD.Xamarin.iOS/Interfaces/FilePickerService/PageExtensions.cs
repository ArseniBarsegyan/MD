using Xamarin.Forms;

namespace MD.Xamarin.iOS.Interfaces.FilePickerService
{
    /// <summary>
    /// Required for PlatformDocumentPicker for iOS.
    /// <para>
    /// Get Renderer for page.
    /// </para>
    /// </summary>
    public static class PageExtensions
    {
        /// <summary>
        /// Get renderer associated with this page, if any.
        /// </summary>
        public static CustomPageRenderer GetRenderer(this Page page)
        {
            CustomPageRenderer pageRenderer;
            if (CustomPageRenderer.Renderers.TryGetValue(page, out pageRenderer))
            {
                return pageRenderer;
            }
            return null;
        }
    }
}