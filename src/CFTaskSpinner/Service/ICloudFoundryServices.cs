using CloudFoundry.CloudController.V3.Client.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFTaskSpinner.Service
{
    public interface ICloudFoundryServices
    {
        Task<DC_CancelTaskResponse> CancelTaskAsync(Guid taskGuid);
        Task<DC_CreateTaskResponse> CreateTaskAsync(Guid appGuid, string taskName, string startCommand,
            params string[] commandArguments);
        Task<Guid> GetAppIdAsync(string applicationName);
        Task<DC_GetTaskResponse> GetTaskAsync(Guid taskGuid);
        Task<DC_ListTaskForAnAppResponse> ListAppTaskAsync(Guid appGuid, string statesCsv);
        Task<DC_ListTaskResponse> ListTaskAsync(string statesCsv);
        Task<string> LoginAsync();
    }
}
