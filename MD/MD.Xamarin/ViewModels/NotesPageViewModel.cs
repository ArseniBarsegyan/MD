using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MD.Xamarin.Helpers;
using MD.Xamarin.Models;
using Plugin.Multilingual;
using Xamarin.Forms;

namespace MD.Xamarin.ViewModels
{
    public class NotesPageViewModel : BaseViewModel
    {
        public NotesPageViewModel()
        {
            RefreshCommand = new Command(async c => await RefreshCommandExecute());
            DeleteNoteCommand = new Command<int>(async id => await DeleteNoteCommandExecute(id));
            Task.Run(async () =>
            {
                var noteModels = await NotesService.GetNotes();
                Notes = new ObservableCollection<NoteModel>(noteModels);
            });
        }

        public bool IsRefreshing { get; set; }
        public ObservableCollection<NoteModel> Notes { get; set; }

        public ICommand RefreshCommand { get; set; }
        public ICommand DeleteNoteCommand { get; set; }

        private async Task RefreshCommandExecute()
        {
            IsRefreshing = true;
            var noteModels = await NotesService.GetNotes();
            Notes = new ObservableCollection<NoteModel>(noteModels);
            IsRefreshing = false;
        }

        private async Task DeleteNoteCommandExecute(int id)
        {
            var ci = CrossMultilingual.Current.CurrentCultureInfo;
            var noteDeleteMessageLocalized = Resmgr.Value.GetString(ConstantsHelper.NoteDeleteMessage, ci);
            var okLocalized = Resmgr.Value.GetString(ConstantsHelper.Ok, ci);
            var cancelLocalized = Resmgr.Value.GetString(ConstantsHelper.Cancel, ci);

            bool result = await AlertService.ShowYesNoAlert(noteDeleteMessageLocalized, okLocalized, cancelLocalized);
            if (result)
            {
                var deleteResult = await NotesService.DeleteNote(id);
                if (deleteResult)
                {
                    MessagingCenter.Send(this, ConstantsHelper.ShouldUpdateUI);
                }
                //await App.NoteRepository.DeleteAsync(id);
                //await App.NoteRepository.SaveAsync();
            }
        }
    }
}