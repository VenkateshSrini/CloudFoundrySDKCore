using System;
using System.Collections.Generic;
using System.Text;

namespace CloudFoundry.CloudController.V3.Client.Data

{
    public class DC_CreateTaskRequest
    {
        public string AppGuid { get; set; }
        public string Command { get; set; }
    }
}
