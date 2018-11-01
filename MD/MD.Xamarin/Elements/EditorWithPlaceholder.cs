using Xamarin.Forms;

namespace MD.Xamarin.Elements
{
    /// <inheritdoc />
    /// <summary>
    /// Custom editor with placeholder of grey color and border.
    /// <para />
    /// Add EditorWithPlaceholderBehavior to this class in XAML.
    /// </summary>
    public class EditorWithPlaceholder : Editor
    {
        public static BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder),
            typeof(string), typeof(EditorWithPlaceholder), string.Empty, BindingMode.TwoWay);

        public static BindableProperty PlaceholderColorProperty = BindableProperty.Create(nameof(PlaceholderColor),
            typeof(Color), typeof(EditorWithPlaceholder), Color.Gray, BindingMode.TwoWay);

        /// <summary>
        /// Placeholder text
        /// </summary>
        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        /// <summary>
        /// Color of placeholder in hex
        /// </summary>
        public Color PlaceholderColor
        {
            get => (Color)GetValue(PlaceholderColorProperty);
            set => SetValue(PlaceholderColorProperty, value);
        }
    }
}