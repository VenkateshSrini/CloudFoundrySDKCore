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
            var cloudFoundryClient = new CloudFoundryClient(new Uri("https://api.cfns.travp.net"), CancellationToken.None);
            var cloudCredentials = new CloudCredentials
            {
                //Todo: Substitute with user ID
                User = "",
                //Todo: Substitute with pasword as base 64 string
                Password = Encoding.UTF8.GetString(Convert.FromBase64String("ZXhwbG9yZTEyMzQ1"))
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
    }
}
