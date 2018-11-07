using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MD.Xamarin.Extensions;
using MD.Xamarin.Helpers;
using Plugin.Multilingual;
using Xamarin.Forms;

namespace MD.Xamarin.ViewModels
{
    public class NoteViewModel : BaseViewModel
    {
        public NoteViewModel()
        {
            UpdateCommand = new Command<Task>(async task => await UpdateCommandExecute());
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public ObservableCollection<PhotoViewModel> Photos { get; set; }

        public ICommand UpdateCommand { get; set; }

        private async Task UpdateCommandExecute()
        {
            var ci = CrossMultilingual.Current.CurrentCultureInfo;
            var okLocalized = Resmgr.Value.GetString(ConstantsHelper.Ok);

            var result = await NotesService.UpdateNote(Id, this.ToNoteModel());

            if (result)
            {
                MessagingCenter.Send(this, ConstantsHelper.ShouldUpdateUI);
            }
            else
            {
                var noteUpdateErrorMessageLocalized = Resmgr.Value.GetString(ConstantsHelper.NoteUpdateError, ci);
                AlertService.ShowOkAlert(noteUpdateErrorMessageLocalized, okLocalized);
            }
        }
    }
}