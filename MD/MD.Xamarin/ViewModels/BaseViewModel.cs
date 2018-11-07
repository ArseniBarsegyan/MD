using System;
using System.ComponentModel;
using System.Reflection;
using System.Resources;
using MD.Xamarin.Helpers;
using MD.Xamarin.Interfaces;
using MD.Xamarin.Rest;
using Xamarin.Forms;

namespace MD.Xamarin.ViewModels
{
    /// <inheritdoc />
    /// <summary>
    /// Base view model for all view models of the app.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected INotesServiceClient NotesService = new NotesServiceClient();
        protected readonly IAlertService AlertService = DependencyService.Get<IAlertService>();

        public static readonly Lazy<ResourceManager> Resmgr = new Lazy<ResourceManager>(
            () => new ResourceManager(ConstantsHelper.TranslationResourcePath, typeof(BaseViewModel).GetTypeInfo().Assembly));

        public event PropertyChangedEventHandler PropertyChanged;
    }
}