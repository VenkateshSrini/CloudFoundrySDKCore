using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFTaskSpinner.MessagePacket
{
    public class ReqTaskInfo
    {
        public string ApplicationName { get; set; }
        public string TaskName { get; set; }
        public string StartCommand { get; set; }
        public string[] CommandArguments { get; set; }
    }
}
