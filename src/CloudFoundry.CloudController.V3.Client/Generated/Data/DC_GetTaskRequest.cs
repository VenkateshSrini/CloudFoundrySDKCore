using System;
using System.Collections.Generic;
using System.Text;

namespace CloudFoundry.CloudController.V3.Client.Data
{
    public class DC_GetTaskRequest
    {
        public string TaskGuid { get; set; }
        public string AuthorizationHeader { get; set; }
    }
}
