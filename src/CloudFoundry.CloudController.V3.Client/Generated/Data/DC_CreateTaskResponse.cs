using System;
using System.Collections.Generic;
using System.Text;

namespace CloudFoundry.CloudController.V3.Client.Data
{

    public class DC_CreateTaskResponse
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

    public class Result
    {
        public object failure_reason { get; set; }
    }

    public class Links
    {
        public Self self { get; set; }
        public App app { get; set; }
        public Droplet droplet { get; set; }
    }

    public class Self
    {
        public string href { get; set; }
    }

    public class App
    {
        public string href { get; set; }
    }

    public class Droplet
    {
        public string href { get; set; }
    }

    
}
