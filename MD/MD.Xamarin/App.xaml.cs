using MD.Xamarin.Authentication;
using MD.Xamarin.EF;
using MD.Xamarin.EF.Models;
using MD.Xamarin.Helpers;
using MD.Xamarin.Interfaces;
using MD.Xamarin.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MD.Xamarin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            string dbPath = DependencyService.Get<IFileHelper>().GetLocalFilePath(ConstantsHelper.SqLiteDataBaseName);
            var applicationContext = new ApplicationContext(dbPath);
            NoteRepository = new NoteRepository(applicationContext);
            UserManager = new UserManager(applicationContext);

            if (Settings.CurrentUserId == "userId" || string.IsNullOrEmpty(Settings.CurrentUserId))
            {
                MainPage = new LoginPage();
            }
            else
            {
                MainPage = new NavigationPage(new NotesPage());
            }
        }

        /// <summary>
        /// Provide CRUD operations with notes.
        /// </summary>
        public static IRepository<Note> NoteRepository { get; private set; }

        /// <summary>
        /// Provide CRUD operations with users.
        /// </summary>
        public static UserManager UserManager { get; private set; }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
