using CloudFoundry.CloudController.V3.Client.Data;
using CloudFoundry.UAA;
using System;
using System.Text;
using System.Threading;
using Xunit;

namespace CloudFoundry.CloudController.V3.Client.Integ.Test
{
    public class UnitTest1
    {
        [Fact]
        public async void CreateTastTest()
        {
            var cloudFoundryClient = new CloudFoundryClient(new Uri(""), CancellationToken.None);
            var cloudCredentials = new CloudCredentials
            {
                //Todo: Substitute with user ID
                User = "",
                //Todo: Substitute with pasword as base 64 string
                Password = Encoding.UTF8.GetString(Convert.FromBase64String(""))
            };
            var authenticationContext = await cloudFoundryClient.Login(cloudCredentials);
            var taskResource = new TaskResourceEndPoint(cloudFoundryClient);
            var taskRequest = new DC_CreateTaskRequest
            {

                AppGuid = "",
                command = "",
            };
            var taskResponse = await taskResource.CreateTask(taskRequest);
            Assert.Null(taskResponse.result.failure_reason);
        }
        [Fact]
        Public async Task CancelTask_Test()
        {
            var cloudFoundryClient = new CloudFoundryClient(new Uri(), CancellationToken.None, null, true);
            var cloudCredentials = new CloudCredentials
            {
                User =””,
                Password = Encoding.UTF8.GetString(Convert.FromBase64String(“”))
            };
            var authenticationContext = await cloudFoundryClient.Login(cloudCredentials);
            var taskResource = new TaskResourceEndPoint(cloudFoundryClient);
            var createTaskRequest = new DC_CreateTaskRequest
            {
                AppGuid = “”,
                Command = “”
                };
            var createTaskResponse = await taskResource.CreateTask(createTaskRequest);
            var taskGuid = createTaskResponse.guid;
            var cancelTaskRequest = new DC_CancelTaskRequest
            {
                TaskGuid = taskGuid
            };
            Var cancelTaskResponse = await taskResource.cancelTask(cancelTaskRequest);
        }

        [Fact]
        Public async Task ListTask_Test()
        {
            var cloudFoundryClient = new CloudFoundryClient(new Uri(), CancellationToken.None, null, true);
            var cloudCredentials = new CloudCredentials
            {
                User =””,
                Password = Encoding.UTF8.GetString(Convert.FromBase64String(“”))
            };
            var authenticationContext = await cloudFoundryClient.Login(cloudCredentials);
            var taskResource = new TaskResourceEndPoint(cloudFoundryClient);
            var listTaskRequest = new DC_ListTaskRequest
            {
                AppGuid = “”
                }
                        Var listTaskResponse = await.TaskResource.ListAllTask();
            Assert.True(listTaskResponse.resources.Length > 0);
        }

        [Fact]
        Public async Task GetTaskByGuid_Test()
        {
            var cloudFoundryClient = new CloudFoundryClient(new Uri(), CancellationToken.None, null, true);
            var cloudCredentials = new CloudCredentials
            {
                User =””,
                Password = Encoding.UTF8.GetString(Convert.FromBase64String(“”))
            };
            var authenticationContext = await cloudFoundryClient.Login(cloudCredentials);
            var taskResource = new TaskResourceEndPoint(cloudFoundryClient);
            var createTaskRequest = new DC_CreateTaskRequest
            {
                AppGuid = “”,
                Command = “”
                };
            var createTaskResponse = await taskResource.CreateTask(createTaskRequest);
            var taskGuid = createTaskResponse.guid;
            var getTaskByGuid = new DC_GetTaskRequest
            {
                TaskGuid = taskGuid;
        }
        Var getTaskResponse = await taskResource.GetTaskByGuid(getTaskByGuid);
        Assert.NotNull(getTaskResponse);
}

    [Fact]
    Public async Task GetTaskForAnApp_Test()
    {
        var cloudFoundryClient = new CloudFoundryClient(new Uri(), CancellationToken.None, null, true);
        var cloudCredentials = new CloudCredentials
        {
            User =””,
            Password = Encoding.UTF8.GetString(Convert.FromBase64String(“”))
        };
        var authenticationContext = await cloudFoundryClient.Login(cloudCredentials);
        var taskResource = new TaskResourceEndPoint(cloudFoundryClient);
        var taskRequest = new DC_CreateTaskRequest
        {
            AppGuid = “”,
            Command = “”
                };
        var createTaskResponse = await taskResource.CreateTask(createTaskRequest);
        var taskGuid = createTaskResponse.guid;
        var taskRequest = new DC_ListTaskForAnAppRequest
        {
            AppGuid = “”
                };
        Var getTaskResponse = await taskResource.GetTaskForAnApp(taskRequest);
        Assert.True(listTaskResponse.resources.Length > 0);
    }

}
}
