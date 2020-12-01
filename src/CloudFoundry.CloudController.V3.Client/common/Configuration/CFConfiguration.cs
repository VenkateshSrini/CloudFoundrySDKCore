using Steeltoe.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudFoundry.CloudController.V3.Client.common.Configuration
{
    public class CFConfiguration
    {
        public string OrgName { get; set; } = string.Empty;
        public string SpaceName { get; set; } = string.Empty;
        public string ApiHost { get; set; } = string.Empty;
        public string Domain { get; set; } = string.Empty;
        public string TragetUri { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Proxy { get; set; } = string.Empty;
        public bool SkipCertificateValidation { get; set; }
        public string AuthorizationUrl { get; set; } = string.Empty;
        public string LogAggregationUrl { get; set; } = string.Empty;
        public bool UseStrictCode { get; set; } = false;
        /// <summary>
        /// Time period in seconds
        /// </summary>
        public int TimeOutPeriod { get; set; } = -1;
        public static CFConfiguration GetConfigurationFromCredential
            (Dictionary<string, Credential> credentials) => new CFConfiguration { 
                ApiHost = credentials["apiHost"]?.Value,
                AuthorizationUrl = credentials.ContainsKey("authorizationUrl") ? credentials["authorizationUrl"]?.Value
                                                                                :string.Empty,
                Domain = credentials["domain"]?.Value,
                LogAggregationUrl=credentials.ContainsKey("logAggregationUrl")? credentials["logAggregationUrl"]?.Value
                                                                               : string.Empty,
                UserName = credentials["username"]?.Value,
                OrgName = credentials["orgName"]?.Value,
                Password=credentials.ContainsKey("password")?Encoding.UTF8.GetString(
                    Convert.FromBase64String(credentials["password"]?.Value)):string.Empty,
                Proxy = credentials.ContainsKey("proxy")?credentials["proxy"]?.Value:string.Empty,
                SkipCertificateValidation = credentials.ContainsKey("skipSSLValidation")? 
                Convert.ToBoolean(credentials["skipSSLValidation"]?.Value):false,
                SpaceName = credentials["spaceName"]?.Value,
                TragetUri=credentials["targetUrl"]?.Value,
                TimeOutPeriod=credentials.ContainsKey("timeOutPeriod")? Convert.ToInt32(credentials["timeOutPeriod"])
                                                                    :300
            };
        
    }
}
