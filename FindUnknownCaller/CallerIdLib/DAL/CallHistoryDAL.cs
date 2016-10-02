using CallerIdLib.DataModel;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallerIdLib.DAL
{
    public class CallHistoryDAL : IDisposable
    {
        private static string path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "CallHistory.sqlite");
        SQLiteConnection conn = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
        public void Insert(string phoneNumber, bool inContacts, string contactId, bool resloved)
        {
            var history = new CallHistory()
            {
                CreatedUtc = DateTime.UtcNow,
                PhoneNumber = phoneNumber,
                ContactId = contactId,
                InContacts = inContacts,
                Resolved = resloved
            };

            conn.Insert(history);
        }

        public void Dispose()
        {
            conn.Dispose();
        }
    }
}
