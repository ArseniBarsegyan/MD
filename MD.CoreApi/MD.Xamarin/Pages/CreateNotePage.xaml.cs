using System;
using MD.Xamarin.Interfaces.FilePickerService;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MD.Xamarin.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateNotePage : ContentPage
    {
        public CreateNotePage()
        {
            InitializeComponent();
        }

        private async void CreateNoteButton_OnClicked(object sender, EventArgs e)
        {
            ViewModel.CreateNoteCommand.Execute(null);
            await Navigation.PopAsync();
        }

        private async void PickPhotoOption_OnClicked(object sender, EventArgs e)
        {
            var documentPicker = DependencyService.Get<IPlatformDocumentPicker>();
            var document = await documentPicker.DisplayImportAsync(this);
            if (document == null)
            {
                ViewModel.IsLoading = false;
                return;
            }
            ViewModel.PickPhotoCommand.Execute(document);
        }
    }
}