using CallerIdLib.BLL;
using CallerIdLib.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.Calls.Background;
using Windows.ApplicationModel.Calls.Provider;

namespace CallerIDBackgroundTask
{    
    public sealed class SampleCallerIDBackgroundTask : IBackgroundTask
    {
        private FindCallerId caller = new FindCallerId();
        
        //
        // The Run method is the entry point of a background task.
        //
        public async void Run(IBackgroundTaskInstance taskInstance)
        {            
            taskInstance.Canceled += new BackgroundTaskCanceledEventHandler(OnCanceled);
            
            var deferral = taskInstance.GetDeferral();

            var dal = new CallHistoryDAL();

            var callDetails = (PhoneCallOriginDataRequestTriggerDetails)taskInstance.TriggerDetails;
            var phoneNumber = callDetails.PhoneNumber;
            var exists = await caller.ExistsInContacts(phoneNumber);

            if (!exists)
            {
                var phoneCallOrigin = await LocalLookupForCallerData(phoneNumber);

                if (phoneCallOrigin != null)
                    PhoneCallOriginManager.SetCallOrigin(callDetails.RequestId, phoneCallOrigin);
            }

            dal.Insert(phoneNumber, exists, "", !exists);

            dal.Dispose();
            deferral.Complete();
        }

        private async Task<PhoneCallOrigin> LocalLookupForCallerData(String phoneNumber)
        {
            try
            {                
                var callerId = await caller.FindByPhoneNumber(phoneNumber);

                var phoneCallOrigin = new PhoneCallOrigin();
                phoneCallOrigin.DisplayName = callerId.Name;
                phoneCallOrigin.Location = callerId.Address;
                phoneCallOrigin.Category = callerId.Category.ToString();
                phoneCallOrigin.CategoryDescription = callerId.Category.ToString();

                return phoneCallOrigin;
            }
            catch (Exception)
            {
                return null;
            }
        }
                
        private void OnCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            var deferral = sender.GetDeferral();            
            deferral.Complete();            
        }
        
    }
}
