using System;
using System.Collections.ObjectModel;

namespace MD.WPF.ViewModels
{
    public class NoteViewModel : BaseViewModel
    {
        private int _id;
        private string _description;
        private DateTime _date;

        public int Id
        {
            get => _id;
            set => SetValue(ref _id, value, nameof(Id));
        }

        public string Description
        {
            get => _description;
            set => SetValue(ref _description, value, nameof(Description));
        }

        public DateTime Date
        {
            get => _date;
            set => SetValue(ref _date, value, nameof(Date));
        }

        public ObservableCollection<PhotoViewModel> Photos { get; set; }
    }
}