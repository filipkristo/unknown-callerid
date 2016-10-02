using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using CallerIdLib.BLL;

namespace FindUnknownCaller.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private BackgroundTaskManager btm = new BackgroundTaskManager();

        public DelegateCommand OpenSettings => new DelegateCommand(() => btm.OpenSettings());

        public MainPageViewModel()
        {

        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            var result = await btm.RegisterTask();

            if (btm.IsActive())
                Status = "Sve OK";
            else
                Status = "Otvorite postavke";
        }
    }
}

