using System;
using System.Collections.Generic;
using System.Text;

namespace CloudFoundry.CloudController.V3.Client.Data
{
   

        public class DC_ListTaskResponse

        {
            public Pagination pagination { get; set; }
            public Resource[] resources { get; set; }
        }

        public class Pagination
        {
            public int total_results { get; set; }
            public int total_pages { get; set; }
            public First first { get; set; }
            public Last last { get; set; }
            public Next next { get; set; }
            public object previous { get; set; }
        }

        public class First
        {
            public string href { get; set; }
        }

        public class Last
        {
            public string href { get; set; }
        }

        public class Next
        {
            public string href { get; set; }
        }

        public class Resource
        {
            public string guid { get; set; }
            public int sequence_id { get; set; }
            public string name { get; set; }
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
