using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MD.Xamarin.Extensions;
using MD.Xamarin.Helpers;
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
            await App.NoteRepository.Update(this.ToNoteModel());
            await App.NoteRepository.SaveAsync();
            MessagingCenter.Send(this, ConstantsHelper.ShouldUpdateUI);
        }
    }
}