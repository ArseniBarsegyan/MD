using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace MD.WPF.Pages
{
    /// <summary>
    /// Interaction logic for NotesPage.xaml
    /// </summary>
    public partial class NotesPage : Page
    {
        public NotesPage()
        {
            InitializeComponent();
            ViewModel.GetNotesCommand.Execute(null);
        }

        private void AddNoteButton_OnClick(object sender, RoutedEventArgs e)
        {
            var createNotePage = new CreateNotePage();
            NavigationService.Navigate(createNotePage);
        }
    }
}
