using Android.Content;
using Android.Text;
using MD.Xamarin.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Editor), typeof(EditorWithoutUnderlineRenderer))]
namespace MD.Xamarin.Droid.Renderers
{
    public class EditorWithoutUnderlineRenderer : EditorRenderer
    {
        public EditorWithoutUnderlineRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = null;
                Control.InputType = InputTypes.ClassText | InputTypes.TextFlagCapSentences | InputTypes.TextFlagMultiLine;
            }
        }
    }
}