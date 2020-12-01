using CloudFoundry.CloudController.Common;
using CloudFoundry.CloudController.V3.Client.Data;
using Newtonsoft.Json;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using CloudFoundry.CloudController.Common.Http;

namespace CloudFoundry.CloudController.V3.Client
{
    public class TaskResourceEndPoint: AbstractTaskResourceEndPoint
    {
        public TaskResourceEndPoint(CloudFoundryClientV3 client,
            ISimpleHttpClient simpleHttpClient) :base(simpleHttpClient)
        {
            this.Client = client;
        }
    }
    public abstract class AbstractTaskResourceEndPoint:BaseEndpoint
    {
        protected AbstractTaskResourceEndPoint(ISimpleHttpClient simpleHttpClient)
            :base(simpleHttpClient)
        {
        }
        public async Task<DC_CreateTaskResponse>CreateTask(DC_CreateTaskRequest createTaskRequest)
        {
            UriBuilder uriBuilder = new UriBuilder(this.Client.CloudTarget);
            uriBuilder.Path = $"/v3/apps/{createTaskRequest.AppGuid}/tasks";
            var client = this.GetHttpClient();
            client.Uri = uriBuilder.Uri;
            client.Method = HttpMethod.Post;
            var authHeader = await BuildAuthenticationHeader();
            if (!string.IsNullOrWhiteSpace(authHeader.Key))
            {
                if (client.Headers.ContainsKey(authHeader.Key))
                    client.Headers[authHeader.Key] = authHeader.Value;
                else
                    client.Headers.Add(authHeader);
            }
            //client.ContentType = "application/x-www-form-urlencoded";
            //client.Content = ((string)JsonConvert.SerializeObject(createTaskRequest)).ConvertToStream();
            var jsonContent = JsonConvert.SerializeObject(createTaskRequest);
            StringContent stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var expectedReturnStatus = 200;
            var response = await this.SendAsync(client, expectedReturnStatus, stringContent);
            return Utilities.DeserializeJson<DC_CreateTaskResponse>(await response.ReadContentAsStringAsync());
        }
        public async Task<DC_CancelTaskResponse>CancelTask(DC_CancelTaskRequest cancelTaskRequest) {
            UriBuilder uriBuilder = new UriBuilder(this.Client.CloudTarget);
            uriBuilder.Path = $"/v3/tasks/{cancelTaskRequest.TaskGuid}/actions/cancel";
            var client = this.GetHttpClient();
            client.Uri = uriBuilder.Uri;
            client.Method = HttpMethod.Put;
            var authHeader = await BuildAuthenticationHeader();
            if (!string.IsNullOrWhiteSpace(authHeader.Key))
            {
                if (client.Headers.ContainsKey(authHeader.Key))
                    client.Headers[authHeader.Key] = authHeader.Value;
                else
                    client.Headers.Add(authHeader);
            }
            var expectedReturnStatus = 200;
            var response = await this.SendAsync(client, expectedReturnStatus);
            return Utilities.DeserializeJson<DC_CancelTaskResponse>(await response.ReadContentAsStringAsync());
        }
        public async Task<DC_GetTaskResponse>GetTaskByGuid(DC_GetTaskRequest getTaskRequest)
        {
            UriBuilder uriBuilder = new UriBuilder(this.Client.CloudTarget);
            uriBuilder.Path = $"/v3/tasks/{getTaskRequest.TaskGuid}";
            var client = this.GetHttpClient();
            client.Uri = uriBuilder.Uri;
            client.Method = HttpMethod.Get;
            var authHeader = await BuildAuthenticationHeader();
            if (!string.IsNullOrWhiteSpace(authHeader.Key))
            {
                if (client.Headers.ContainsKey(authHeader.Key))
                    client.Headers[authHeader.Key] = authHeader.Value;
                else
                    client.Headers.Add(authHeader);
            }
            var expectedReturnStatus = 200;
            var response = await this.SendAsync(client, expectedReturnStatus);
            return Utilities.DeserializeJson<DC_GetTaskResponse>(await response.ReadContentAsStringAsync());
        }
        public async Task<DC_ListTaskResponse>ListAllTask(DC_ListTaskRequest listTaskRequest=null)
        {
            UriBuilder uriBuilder = new UriBuilder(this.Client.CloudTarget);
          
            var queryParams = ((listTaskRequest != null) && (listTaskRequest.BuildQueryParams().Length>0)) ? listTaskRequest.BuildQueryParams() : string.Empty;
            uriBuilder.Path = $"/v3/tasks";
            if (!string.IsNullOrWhiteSpace(queryParams))
                uriBuilder.Query = queryParams;
            
            var client = this.GetHttpClient();
            client.Uri = uriBuilder.Uri;
            client.Method = HttpMethod.Get;
            var authHeader = await BuildAuthenticationHeader();
            if (!string.IsNullOrWhiteSpace(authHeader.Key))
            {
                if (client.Headers.ContainsKey(authHeader.Key))
                    client.Headers[authHeader.Key] = authHeader.Value;
                else
                    client.Headers.Add(authHeader);
            }
            var expectedReturnStatus = 200;
            var response = await this.SendAsync(client, expectedReturnStatus);
            return Utilities.DeserializeJson<DC_ListTaskResponse>(await response.ReadContentAsStringAsync());
        }
        public async Task<DC_ListTaskForAnAppResponse>GetAllTaskForAnApp(DC_ListTaskForAnAppRequest listTaskforAppRequest)
        {
            UriBuilder uriBuilder = new UriBuilder(this.Client.CloudTarget);

            var queryParams = ((listTaskforAppRequest != null) && (listTaskforAppRequest.BuildQueryParams().Length > 0)) ? listTaskforAppRequest.BuildQueryParams() : string.Empty;
            uriBuilder.Path = $"/v3/apps/{listTaskforAppRequest.AppGuid}/tasks";
            if (!string.IsNullOrWhiteSpace(queryParams))
                uriBuilder.Query = queryParams;

            var client = this.GetHttpClient();
            client.Uri = uriBuilder.Uri;
            client.Method = HttpMethod.Get;
            var authHeader = await BuildAuthenticationHeader();
            if (!string.IsNullOrWhiteSpace(authHeader.Key))
            {
                if (client.Headers.ContainsKey(authHeader.Key))
                    client.Headers[authHeader.Key] = authHeader.Value;
                else
                    client.Headers.Add(authHeader);
            }
            var expectedReturnStatus = 200;
            var response = await this.SendAsync(client, expectedReturnStatus);
            return Utilities.DeserializeJson<DC_ListTaskForAnAppResponse>(await response.ReadContentAsStringAsync());
        }
    }
}
