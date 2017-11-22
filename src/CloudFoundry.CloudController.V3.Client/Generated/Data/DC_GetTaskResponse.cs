using System;
using System.Collections.Generic;
using System.Text;

namespace CloudFoundry.CloudController.V3.Client.Data
{

    public class DC_GetTaskResponse
    {
        public string guid { get; set; }
        public int sequence_id { get; set; }
        public string name { get; set; }
        public string command { get; set; }
        public string state { get; set; }
        public int memory_in_mb { get; set; }
        public int disk_in_mb { get; set; }
        public Result result { get; set; }
        public string droplet_guid { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public Links links { get; set; }
    }

   
}
