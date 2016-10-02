using CallerIdLib.DataModel;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Data;

namespace FindUnknownCaller
{
    /// Documentation on APIs used in this page:
    /// https://github.com/Windows-XAML/Template10/wiki

    [Bindable]
    sealed partial class App : Template10.Common.BootStrapper
    {
        string path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "CallHistory.sqlite");

        public App() { InitializeComponent(); }

        public override async Task OnInitializeAsync(IActivatedEventArgs args)
        {
            using (var conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path))
            {
                conn.CreateTable<CallHistory>();

                var a = conn.Table<CallHistory>();

                foreach (var item in a)
                {
                    Debug.WriteLine($"Datum: {item.CreatedUtc} Phone: {item.PhoneNumber}, IsContact:{item.InContacts}");
                }            
            }                

            await Task.CompletedTask;            
        }

        public override async Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            NavigationService.Navigate(typeof(Views.MainPage));
            await Task.CompletedTask;
        }
    }
}

