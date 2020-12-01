namespace CloudFoundry.CloudController.Common
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using CloudFoundry.CloudController.Common.Http;

    using CloudFoundry.CloudController.V3.Client.common.Configuration;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Steeltoe.Extensions.Configuration.CloudFoundry;

    /// <summary>
    /// This is an abstract class that serves as base for the CloudFoundry clients.
    /// </summary>
    public abstract class CloudFoundryClient
    {
        private ILogger _logger;
        public CFConfiguration CloudFoundryClientConfiguration { get; private set; }
        public CloudFoundryClient(IOptions<CloudFoundryServicesOptions> cfOptions,
            IOptions<CfServiceBinding> cfServiceOptions, ILogger logger)
        {
            _logger = logger;
            if (cfOptions == null)
                _logger.LogCCInformation("CF OPtions not found");
            else if (cfServiceOptions == null)
                _logger.LogCCInformation("vcap services not found");
            else
            {
                var credentials = cfOptions?.Value?.GetServicesList()?.FirstOrDefault(
                    service => service.Name.CompareTo(cfServiceOptions?.Value?.ServiceBinding) == 0)
                    .Credentials;
                var cfConfig = CFConfiguration.GetConfigurationFromCredential(credentials);
                CloudFoundryClientConfiguration = cfConfig;
                SetClassProperty(new Uri(cfConfig.TragetUri), CancellationToken.None,
                    !string.IsNullOrWhiteSpace(cfConfig?.Proxy) ? new Uri(cfConfig?.Proxy) : null,
                    cfConfig.SkipCertificateValidation,
                    !string.IsNullOrWhiteSpace(cfConfig?.AuthorizationUrl) ? new Uri(cfConfig?.AuthorizationUrl) : null,
                    cfConfig.UseStrictCode,
                    new TimeSpan((long)(cfConfig?.TimeOutPeriod * Math.Pow(10, 9) / 100))
                    );
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CloudFoundryClient"/> class.
        /// </summary>
        /// <param name="cloudTarget">The cloud target.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        protected CloudFoundryClient(Uri cloudTarget, CancellationToken cancellationToken)
            : this(cloudTarget, cancellationToken, null, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CloudFoundryClient" /> class.
        /// </summary>
        /// <param name="cloudTarget">The cloud target.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="httpProxy">The HTTP proxy.</param>
        protected CloudFoundryClient(Uri cloudTarget, CancellationToken cancellationToken, Uri httpProxy)
            : this(cloudTarget, cancellationToken, httpProxy, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CloudFoundryClient" /> class.
        /// </summary>
        /// <param name="cloudTarget">The cloud target.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="httpProxy">The HTTP proxy.</param>
        /// <param name="skipCertificateValidation">if set to <c>true</c> it will skip TLS certificate validation for HTTP requests.</param>
        protected CloudFoundryClient(Uri cloudTarget, CancellationToken cancellationToken, Uri httpProxy, bool skipCertificateValidation)
            : this(cloudTarget, cancellationToken, httpProxy, skipCertificateValidation, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CloudFoundryClient" /> class.
        /// </summary>
        /// <param name="cloudTarget">The cloud target.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="httpProxy">The HTTP proxy.</param>
        /// <param name="skipCertificateValidation">if set to <c>true</c> it will skip TLS certificate validation for HTTP requests.</param>
        /// <param name="authorizationUrl">Authorization Endpoint</param>
        protected CloudFoundryClient(Uri cloudTarget, CancellationToken cancellationToken, Uri httpProxy, bool skipCertificateValidation, Uri authorizationUrl)
            : this(cloudTarget, cancellationToken, httpProxy, skipCertificateValidation, authorizationUrl, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CloudFoundryClient" /> class.
        /// </summary>
        /// <param name="cloudTarget">The cloud target.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="httpProxy">The HTTP proxy.</param>
        /// <param name="skipCertificateValidation">if set to <c>true</c> it will skip TLS certificate validation for HTTP requests.</param>
        /// <param name="authorizationUrl">Authorization Endpoint</param>
        /// <param name="useStrictStatusCodeChecking">Throw exception if the successful http status code returned from the server does not match the expected code</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "This method is implemented by sealed cf clients")]
        protected CloudFoundryClient(Uri cloudTarget, CancellationToken cancellationToken, Uri httpProxy, bool skipCertificateValidation, Uri authorizationUrl, bool useStrictStatusCodeChecking)
            : this(cloudTarget, cancellationToken, httpProxy, skipCertificateValidation, authorizationUrl, useStrictStatusCodeChecking, SimpleHttpClient.DefaultTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CloudFoundryClient" /> class.
        /// </summary>
        /// <param name="cloudTarget">The cloud target.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="httpProxy">The HTTP proxy.</param>
        /// <param name="skipCertificateValidation">if set to <c>true</c> it will skip TLS certificate validation for HTTP requests.</param>
        /// <param name="authorizationUrl">Authorization Endpoint</param>
        /// <param name="useStrictStatusCodeChecking">Throw exception if the successful http status code returned from the server does not match the expected code</param>
        /// <param name="requestTimeout">Http requests timeout</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "This method is implemented by sealed cf clients")]
        protected CloudFoundryClient(Uri cloudTarget, CancellationToken cancellationToken, Uri httpProxy, bool skipCertificateValidation, Uri authorizationUrl, bool useStrictStatusCodeChecking, TimeSpan requestTimeout)
        {

            SetClassProperty(cloudTarget, cancellationToken, httpProxy, skipCertificateValidation,
                authorizationUrl, useStrictStatusCodeChecking, requestTimeout);
           // this.InitEndpoints();
        }
        private void SetClassProperty(Uri cloudTarget, CancellationToken cancellationToken, Uri httpProxy, bool skipCertificateValidation, Uri authorizationUrl, bool useStrictStatusCodeChecking, TimeSpan requestTimeout)
        {
            this.CloudTarget = cloudTarget;
            this.CancellationToken = cancellationToken;
            this.HttpProxy = httpProxy;
            this.SkipCertificateValidation = skipCertificateValidation;
            this.AuthorizationEndpoint = authorizationUrl;
            this.UseStrictStatusCodeChecking = useStrictStatusCodeChecking;
            this.RequestTimeout = requestTimeout;
        }
        /// <summary>
        /// Authorization Endpoint
        /// </summary>
        public Uri AuthorizationEndpoint { get; protected set; }

        /// <summary>
        /// Cancellation Token
        /// </summary>
        public CancellationToken CancellationToken { get; protected set; }

        /// <summary>
        /// Target
        /// </summary>
        public Uri CloudTarget { get; protected set; }

        /// <summary>
        /// Proxy
        /// </summary>
        public Uri HttpProxy { get; protected set; }

        /// <summary>
        /// Skip Validation
        /// </summary>
        public bool SkipCertificateValidation { get; protected set; }

        /// <summary>
        /// Skip Validation
        /// </summary>
        public bool UseStrictStatusCodeChecking { get; protected set; }

        /// <summary>
        /// Http requests timeout
        /// </summary>
        public TimeSpan RequestTimeout { get; protected set; }

        /// <summary>
        /// Initializes all API Endpoints
        /// </summary>
        public abstract void InitEndpoints();
    }
}