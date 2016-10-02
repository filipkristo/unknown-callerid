using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.Calls.Background;
using Windows.ApplicationModel.Calls.Provider;

namespace CallerIdLib.BLL
{
    public class BackgroundTaskManager
    {
        public const string BackgroundTaskEntryPoint = "CallerIDBackgroundTask.CallerIDBackgroundTask";
        public const string BackgroundTaskName = "CallerIDBackgroundTask";

        public IBackgroundTaskRegistration FindBackgroundTaskRegistration()
        {
            foreach (var taskRegistration in BackgroundTaskRegistration.AllTasks.Values)
            {
                if (taskRegistration.Name == BackgroundTaskName)
                {
                    return taskRegistration;
                }
            }
            return null;
        }

        public async Task<bool> RegisterTask()
        {
            if (FindBackgroundTaskRegistration() == null)
                return false;

            var access = await BackgroundExecutionManager.RequestAccessAsync();

            if (access == BackgroundAccessStatus.AlwaysAllowed ||
                access == BackgroundAccessStatus.AllowedSubjectToSystemPolicy)
            {                
                var taskBuilder = new BackgroundTaskBuilder();                
                var trigger = new PhoneTrigger(PhoneTriggerType.CallOriginDataRequest, false);
                
                taskBuilder.SetTrigger(trigger);
                taskBuilder.TaskEntryPoint = BackgroundTaskEntryPoint;                
                taskBuilder.Name = BackgroundTaskName;
                
                var taskRegistration = taskBuilder.Register();                
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UnregisterTask()
        {
            var taskRegistration = FindBackgroundTaskRegistration();

            if (taskRegistration != null)
            {
                taskRegistration.Unregister(true);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsActive()
        {
            return PhoneCallOriginManager.IsCurrentAppActiveCallOriginApp;
        }

        public void OpenSettings()
        {            
            PhoneCallOriginManager.ShowPhoneCallOriginSettingsUI();
        }
    }
}
