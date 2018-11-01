using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MD.Xamarin.Extensions;
using MD.Xamarin.Helpers;
using MD.Xamarin.Interfaces;
using Plugin.Multilingual;
using Xamarin.Forms;

namespace MD.Xamarin.ViewModels
{
    public class NotesPageViewModel : BaseViewModel
    {
        private static readonly IAlertService AlertService = DependencyService.Get<IAlertService>();

        public NotesPageViewModel()
        {
            Notes = new ObservableCollection<NoteViewModel>(App.NoteRepository
                .GetAll(Settings.CurrentUserId)
                .ToList()
                .ToNoteViewModels());
            RefreshCommand = new Command(RefreshCommandExecute);
            DeleteNoteCommand = new Command<int>(async id => await DeleteNoteCommandExecute(id));
        }

        public bool IsRefreshing { get; set; }
        public ObservableCollection<NoteViewModel> Notes { get; set; }

        public ICommand RefreshCommand { get; set; }
        public ICommand DeleteNoteCommand { get; set; }

        private void RefreshCommandExecute()
        {
            IsRefreshing = true;
            var noteViewModels = App.NoteRepository.GetAll(Settings.CurrentUserId).ToList().ToNoteViewModels();
            Notes = new ObservableCollection<NoteViewModel>(noteViewModels);
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
                await App.NoteRepository.DeleteAsync(id);
                await App.NoteRepository.SaveAsync();
            }
            
            MessagingCenter.Send(this, ConstantsHelper.ShouldUpdateUI);
        }
    }
}