namespace CloudFoundry.CloudController.Common.Http
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    /// <inheritdoc/>
    public class PlatformBaseHttpClientHandler : HttpClientHandler, IPlatformBaseHttpClientHandler
    {
        /// <inheritdoc/>
        private bool _skipServerCertificateValidation;
        public bool SkipCertificateValidation
        {
            get
            {
                return _skipServerCertificateValidation;
            }

            set
            {
                _skipServerCertificateValidation = value;
                if (value == true)
                {
                    // throw new NotSupportedException("Cannot skip certificate validation on this platform.");
                    this.ServerCertificateCustomValidationCallback = delegate { return true; };
                }
            }
        }
    }
}
