using System;
using MD.Xamarin.Extensions;
using MD.Xamarin.Helpers;
using MD.Xamarin.Models;
using MD.Xamarin.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MD.Xamarin.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesPage : ContentPage
    {
        public NotesPage()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<NotesPageViewModel>(this, ConstantsHelper.ShouldUpdateUI, sender =>
            {
                ViewModel.RefreshCommand.Execute(null);
            });
            MessagingCenter.Subscribe<NoteViewModel>(this, ConstantsHelper.ShouldUpdateUI, sender =>
            {
                ViewModel.RefreshCommand.Execute(null);
            });
            MessagingCenter.Subscribe<CreateNoteViewModel>(this, ConstantsHelper.ShouldUpdateUI, sender =>
            {
                ViewModel.RefreshCommand.Execute(null);
            });
        }

        private void Logout_OnClicked(object sender, EventArgs e)
        {
            Settings.CurrentUserId = "userId";
            Application.Current.MainPage = new LoginPage();
        }

        private async void CreateNoteButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateNotePage());
        }

        private async void NotesList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var noteModel = NotesList.SelectedItem as NoteModel;

            NotesList.SelectedItem = null;
            if (noteModel != null)
            {
                await Navigation.PushAsync(new NoteDetailPage(noteModel.ToNoteViewModel()));
            }
        }

        private void Delete_OnClicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var noteModel = menuItem?.CommandParameter as NoteModel;
            ViewModel.DeleteNoteCommand.Execute(noteModel?.Id);
        }
    }
}