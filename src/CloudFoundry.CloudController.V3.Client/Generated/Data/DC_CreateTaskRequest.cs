using System;
using System.Collections.Generic;
using System.Text;

namespace CloudFoundry.CloudController.V3.Client.Data

{
    public class DC_CreateTaskRequest
    {
        public string AppGuid { get; set; }
        public string command { get; set; }
        public string name { get; set; }
        public double memory_in_mb { get; set; }
        public double disk_in_mb { get; set; }
        public string droplet_guid { get; set; }
    }
}
