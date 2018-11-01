using System;
using MD.Xamarin.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MD.Xamarin.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteDetailPage : ContentPage
    {
        public NoteDetailPage(NoteViewModel viewModel)
        {
            BindingContext = viewModel;
            InitializeComponent();
        }

        private async void EditNoteButton_OnClicked(object sender, EventArgs e)
        {
            if (BindingContext is NoteViewModel viewModel)
            {
                viewModel.UpdateCommand.Execute(null);
                await Navigation.PopAsync();
            }
        }
    }
}