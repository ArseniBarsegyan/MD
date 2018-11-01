namespace MD.WPF.ViewModels
{
    public class PhotoViewModel : BaseViewModel
    {
        private int _id;
        private string _name;
        private string _image;

        public int Id
        {
            get => _id;
            set => SetValue(ref _id, value, nameof(Id));
        }

        public string Name
        {
            get => _name;
            set => SetValue(ref _name, value, nameof(Name));
        }

        public string Image
        {
            get => _image;
            set => SetValue(ref _image, value, nameof(Image));
        }
    }
}