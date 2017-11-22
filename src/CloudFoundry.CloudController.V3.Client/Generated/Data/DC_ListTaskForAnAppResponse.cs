using System;
using System.Collections.Generic;
using System.Text;

namespace CloudFoundry.CloudController.V3.Client.Data
{

    public class DC_ListTaskForAnAppResponse
    {
        public Pagination pagination { get; set; }
        public Resource[] resources { get; set; }
    }

    
}
