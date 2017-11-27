using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace CloudFoundry.CloudController.V3.Client.Data

{
    public class DC_CreateTaskRequest
    {
        [JsonIgnore]
        public string AppGuid { get; set; }
        public string command { get; set; }
        public string name { get; set; }
        public double memory_in_mb { get; set; }
        public double disk_in_mb { get; set; }
        public string droplet_guid { get; set; }
        public bool ShouldSerializename()
        {
            return string.IsNullOrWhiteSpace(name);
        }
        public bool ShouldSerializememory_in_mb()
        {
            return (memory_in_mb > 0);
        }
        public bool ShouldSerializedisk_in_mb()
        {
            return (disk_in_mb > 0);
        }
        public bool ShouldSerializedroplet_guid()
        {
            return string.IsNullOrWhiteSpace(droplet_guid);
        }
    }
}
