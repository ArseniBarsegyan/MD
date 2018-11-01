using System.Windows;
using MD.WPF.Properties;

namespace MD.WPF.Pages
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(Settings.Default.UserName))
            {
                UserName.Content = Settings.Default.UserName;
            }           
        }
    }
}
