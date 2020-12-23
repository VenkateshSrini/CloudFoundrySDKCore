using CloudFoundry.CloudController.V2.Client;
using CloudFoundry.CloudController.V3.Client;
using CloudFoundry.CloudController.V3.Client.Data;
using CloudFoundry.UAA;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFTaskSpinner.Service
{
    public class CloudFoundryServices : ICloudFoundryServices
    {
        ILogger<CloudFoundryServices> _logger;
        CloudFoundryClientV2 _cfV2Client;
        CloudFoundryClientV3 _cfV3Client;
        public CloudFoundryServices(ILogger<CloudFoundryServices> logger, 
            CloudFoundryClientV3 cfV3Client)
        {
            _logger = logger;
            _cfV2Client = cfV3Client.V2;
            _cfV3Client = cfV3Client;
        }
        public async Task<DC_CancelTaskResponse> CancelTaskAsync(Guid taskGuid)
        {
            var request = new DC_CancelTaskRequest
            {
                TaskGuid = taskGuid.ToString()
            };
            try
            {
                var response = await _cfV3Client.TaskResource.CancelTask(request);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error {ex.Message} Stack Trac {ex.StackTrace}");
                throw;
            }
        }

        public async Task<DC_CreateTaskResponse> CreateTaskAsync(Guid appGuid, string taskName, string startCommand, params string[] commandArguments)
        {
            try
            {
                var taskCommand = $"{startCommand}{string.Join(" ", commandArguments)}";
                var request = new DC_CreateTaskRequest
                {
                    AppGuid = appGuid.ToString(),
                    command = taskCommand,
                    name = taskName
                };
                var response = await _cfV3Client.TaskResource.CreateTask(request);
                return response;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error {ex.Message} Stack Trac {ex.StackTrace}");
                throw;
            }
        }

        public async Task<Guid> GetAppIdAsync(string applicationName)
        {
            try
            {
                var orgResponses = await _cfV2Client.Organizations.ListAllOrganizations();
                var orgId = orgResponses?.FirstOrDefault((orgs) =>
                orgs.Name.CompareTo(_cfV2Client.CloudFoundryClientConfiguration.OrgName) == 0)?.EntityMetadata.Guid;
                var spaceResponse = await _cfV2Client.Organizations.ListAllSpacesForOrganization(orgId);
                var spaceId = spaceResponse?.FirstOrDefault(space => space.Name.CompareTo(
                    _cfV2Client.CloudFoundryClientConfiguration.SpaceName) == 0)?.EntityMetadata.Guid;
                var appResponse = await _cfV2Client.Spaces.ListAllAppsForSpace(spaceId);
                var appId = appResponse?.FirstOrDefault(App => App.Name.CompareTo(applicationName) == 0)?.EntityMetadata?.Guid;
                return appId;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error {ex.Message} and Stack trace {ex.StackTrace}");
                throw;
            }
        }

        public async Task<DC_GetTaskResponse> GetTaskAsync(Guid taskGuid)
        {
            try
            {
                var request = new DC_GetTaskRequest
                {
                    TaskGuid = taskGuid.ToString()
                };
                var response = await _cfV3Client.TaskResource.GetTaskByGuid(request);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error {ex.Message} and Stack trace {ex.StackTrace}");
                throw;
            }
        }

        public async Task<DC_ListTaskForAnAppResponse> ListAppTaskAsync(Guid appGuid, string statesCsv)
        {
            var request = new DC_ListTaskForAnAppRequest
            {
                AppGuid = appGuid.ToString(),
                TaskStates = statesCsv
            };
            try
            {
                var response = await _cfV3Client.TaskResource.GetAllTaskForAnApp(request);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error {ex.Message} and Stack trace {ex.StackTrace}");
                throw;
            }
        }

        public async Task<DC_ListTaskResponse> ListTaskAsync(string statesCsv)
        {
            var request = new DC_ListTaskRequest
            {
                TaskStates = statesCsv,
                Page = 1,
                PerPageRecords = 5000,
                OrderBy = "-update_at"
            };
            try
            {
                var response = await _cfV3Client.TaskResource.ListAllTask(request);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error {ex.Message} and Stack trace {ex.StackTrace}");
                throw;
            }
        }

        public async Task<string> LoginAsync()
        {
            try
            {
                var accessToken = await _cfV3Client.GenerateAuthorizationToken();
                if (!string.IsNullOrWhiteSpace(accessToken)) return accessToken;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Refresh token expired {ex.Message} using credential to attempt connection");
            }
            try
            {
                CloudCredentials credentials = new CloudCredentials
                {
                    User = _cfV3Client.CloudFoundryClientConfiguration.UserName,
                    Password = _cfV3Client.CloudFoundryClientConfiguration.Password
                };
                AuthenticationContext authContext = await _cfV3Client.Login(credentials);
                return authContext.Token.RefreshToken;
            }
            
            catch(Exception ex)
            {
                _logger.LogError($"Log in to CC API Failed {ex.Message}");
                throw ;
            }
}
    }
}
