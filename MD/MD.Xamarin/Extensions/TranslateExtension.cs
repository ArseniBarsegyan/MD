using Plugin.Multilingual;
using System;
using System.Reflection;
using System.Resources;
using MD.Xamarin.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MD.Xamarin.Extensions
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        private static readonly Lazy<ResourceManager> Resmgr = new Lazy<ResourceManager>(
            () => new ResourceManager(ConstantsHelper.TranslationResourcePath, typeof(TranslateExtension).GetTypeInfo().Assembly));

        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
            {
                return string.Empty;
            }

            var ci = CrossMultilingual.Current.CurrentCultureInfo;

            var translation = Resmgr.Value.GetString(Text, ci);

            if (translation == null)
            {

#if DEBUG
                throw new ArgumentException(
                    $"Key '{Text}' was not found in resources '{ConstantsHelper.TranslationResourcePath}' for culture '{ci.Name}'.",
                    "Text");
#else
				translation = Text; // returns the key, which GETS DISPLAYED TO THE USER
#endif
            }
            return translation;
        }
    }
}