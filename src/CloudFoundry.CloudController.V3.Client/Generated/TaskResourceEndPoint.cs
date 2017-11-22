using CloudFoundry.CloudController.Common;
using CloudFoundry.CloudController.V3.Client.Data;
using Newtonsoft.Json;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace CloudFoundry.CloudController.V3.Client
{
    public class TaskResourceEndPoint: AbstractTaskResourceEndPoint
    {
        public TaskResourceEndPoint(CloudFoundryClient client):base()
        {
            this.Client = client;
        }
    }
    public abstract class AbstractTaskResourceEndPoint:BaseEndpoint
    {
        protected AbstractTaskResourceEndPoint()
        {
        }
        public Task<DC_CreateTaskResponse>CreateTask(DC_CreateTaskRequest createTaskRequest)
        {
            return null;
        }
        public Task<DC_CancelTaskResponse>CancelTask(DC_CancelTaskRequest cancelTaskRequest) { return null; }
        public Task<DC_GetTaskResponse>GetTaskByGuid(DC_GetTaskRequest getTaskRequest)
        {
            return null;
        }
        public Task<DC_ListTaskResponse>ListAllTask()
        {
            return null;
        }
        public Task<DC_ListTaskForAnAppResponse>GetAllTaskForAnApp(DC_ListTaskForAnAppResponse)
        {
            return null;
        }
    }
}
