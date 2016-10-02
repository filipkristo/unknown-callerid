using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;

namespace FindUnknownCaller.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public bool IsRegisterEnabled { get { return Get<bool>(); } set { Set(value); } }
        public bool IsUnRegisterEnabled { get { return Get<bool>(); } set { Set(value); } }       
    }
}

