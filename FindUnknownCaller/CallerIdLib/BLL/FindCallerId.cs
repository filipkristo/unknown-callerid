using CallerIdLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Contacts;

namespace CallerIdLib.BLL
{
    public class FindCallerId
    {
        public async Task<bool> ExistsInContacts(string phoneNumber)
        {
            var contactStore = await ContactManager.RequestStoreAsync(ContactStoreAccessType.AllContactsReadOnly);
            var contactList = await contactStore.FindContactListsAsync();

            foreach (var item in contactList)
            {
                var contact = await item.GetContactAsync(item.Id);
                var exists = contact.Phones.Any(x => phoneNumber.Replace("+385", "0") == x.Number.Replace("+385", "0"));

                if (exists)
                    return true;
            }

            return false;
        }

        public async Task<CallerId> FindByPhoneNumber(string phoneNumber)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Fiddler");

                var uri = $"http://www.imenik.hr/imenik/trazi/1/{phoneNumber}.html";
                var html = await client.GetStringAsync(uri);

                html = html.Substring(html.IndexOf("list_naslov"));

                var name = html.Substring(html.IndexOf("href"));

                name = name.Substring(name.IndexOf("\">") + 2);
                name = name.Substring(0, name.IndexOf("</a")).Trim();

                var adresa = html.Substring(html.IndexOf("adresa"));
                adresa = adresa.Substring(adresa.IndexOf("nofollow\">") + 10);
                adresa = adresa.Substring(0, adresa.IndexOf("</a")).Trim();

                return new CallerId
                {
                    Name = name,
                    Address = adresa.Substring(0, 10)
                };
            }
        }


    }
}
