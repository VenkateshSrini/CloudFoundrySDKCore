namespace CloudFoundry.CloudController.Common.Http
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using CloudFoundry.CloudController.Common.Http;
    using CloudFoundry.CloudController.V3.Client.common.Configuration;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Steeltoe.Extensions.Configuration.CloudFoundry;

    /// <inheritdoc/>
    public class SimpleHttpClient : IDisposable, ISimpleHttpClient
    {
        private ILogger<SimpleHttpClient> _logger;
        /// <summary>
        /// Default HTTP Request timeout
        /// </summary>
        public static readonly TimeSpan DefaultTimeout = new TimeSpan(0, 0, 30);

        private HttpClient client = null;
        private readonly SimpleHttpRedirectHandler handler = null;
        public CancellationToken cancellationToken = CancellationToken.None;
        private bool disposed = false;

        public SimpleHttpClient(HttpClient httpClient, 
            IOptions<CloudFoundryServicesOptions>cfOptions,IOptions<CfServiceBinding> cfServiceOptions,
            ILogger<SimpleHttpClient> logger, SimpleHttpRedirectHandler httpHandler)
        {
            _logger = logger;
            if (cfOptions == null)
                logger.LogCCInformation("CF options not found");
            else if (cfServiceOptions == null)
                logger.LogCCInformation("VCAP services not found");
            else
            {
                var credentials = cfOptions?.Value.GetServicesList()?.FirstOrDefault(service =>
                service.Name.CompareTo(cfServiceOptions?.Value?.ServiceBinding) == 0).Credentials;
                var cfConfig = CFConfiguration.GetConfigurationFromCredential(credentials);
                handler = httpHandler;
                SetClassProperties(httpClient, cfConfig);
            }
        }
        private void SetClassProperties(HttpClient httpClient, CFConfiguration cfOPtions)
        {
            this.client = httpClient;
            this.Timeout = new TimeSpan(0, 0, (int)cfOPtions?.TimeOutPeriod);
            this.Method = HttpMethod.Get;
            this.Headers = new Dictionary<string, string>();
            ContentType = string.Empty;
            cancellationToken = CancellationToken.None;
            SkipCertificateValidation = cfOPtions.SkipCertificateValidation;

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleHttpClient"/> class.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        public SimpleHttpClient(CancellationToken cancellationToken)
            : this(cancellationToken, SimpleHttpClient.DefaultTimeout, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleHttpClient"/> class.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="timeout">The timeout.</param>
        public SimpleHttpClient(CancellationToken cancellationToken, TimeSpan timeout, bool skipCertificateValidation)
        {
            try
            {

                this.handler = new SimpleHttpRedirectHandler();
                //if (!validateClientCertificate)
                //    this.handler.ServerCertificateCustomValidationCallback = delegate { return true; };
                this.client = new HttpClient(this.handler);

                this.Timeout = timeout;
                this.cancellationToken = cancellationToken;
                this.Method = HttpMethod.Get;
                this.Headers = new Dictionary<string, string>();
                this.ContentType = string.Empty;
                //this.SkipCertificateValidation = false;
                this.SkipCertificateValidation = skipCertificateValidation;
            }
            catch
            {
                if (this.handler != null)
                {
                    this.handler.Dispose();
                }

                if (this.client != null)
                {
                    this.client.Dispose();
                }

                throw;
            }
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="SimpleHttpClient"/> class.
        /// </summary>
        ~SimpleHttpClient()
        {
            this.Dispose(false);
        }

        /// <inheritdoc/>
        public Stream Content { get; set; }

        /// <inheritdoc/>
        public string ContentType { get; set; }

        /// <inheritdoc/>
        public IDictionary<string, string> Headers { get; private set; }

        /// <inheritdoc/>
        public HttpMethod Method { get; set; }

        /// <inheritdoc/>
        public TimeSpan Timeout { get; set; }

        /// <inheritdoc/>
        public Uri Uri { get; set; }

        /// <inheritdoc/>
        public Uri HttpProxy { get; set; }

        /// <inheritdoc/>
        public bool SkipCertificateValidation { get; set; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc/>
        public async Task<SimpleHttpResponse> SendAsync()
        {
            HttpContent httpContent = null;
            if (this.Content != null)
            {
                httpContent = new StreamContent(this.Content);
            }

            return await this.SendAsync(httpContent);
        }

        /// <inheritdoc/>
        public async Task<SimpleHttpResponse> SendAsync(IEnumerable<HttpMultipartFormData> multipartData)
        {
            var boundary = Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N");

            var httpContent = new MultipartFormDataContent(boundary);

            foreach (var field in multipartData)
            {
                var content = new StreamContent(field.Content);

                if (!string.IsNullOrEmpty(field.ContentType))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue(field.ContentType);
                }

                // Workaround for CloudController's NGINX. Make sure the filename and name are surrounded with quotes to make NGINX upload module happy
                // https://github.com/vkholodkov/nginx-upload-module/blob/2.2/ngx_http_upload_module.c#L39
                if (string.IsNullOrEmpty(field.FileName))
                {
                    var name = string.Concat("\"", field.Name, "\"");
                    httpContent.Add(content, name);
                }
                else
                {
                    var name = string.Concat("\"", field.Name, "\"");
                    var fileName = string.Concat("\"", field.FileName, "\"");
                    httpContent.Add(content, name, fileName);
                }
            }

            // Workaround for CloudController's NGINX. The NGINX upload module doesn't like the boundary to be wrapped in quotes.
            // http://www.codedisqus.com/0HNVXVqWUj/wrong-contenttype-header-generated-using-multipartformdatacontent.html
            httpContent.Headers.Remove("Content-Type");
            httpContent.Headers.TryAddWithoutValidation("Content-Type", "multipart/form-data; boundary=" + boundary);

            return await this.SendAsync(httpContent);
        }

        public async Task<SimpleHttpResponse> SendAsync(HttpContent requestContent)
        {
            var requestMessage = new HttpRequestMessage { Method = this.Method, RequestUri = this.Uri };

            if (this.Method == HttpMethod.Post || this.Method == HttpMethod.Put)
            {
                if (requestContent != null)
                {
                    requestMessage.Content = requestContent;
                }
            }

            requestMessage.Headers.Clear();
            foreach (var header in this.Headers)
            {
                requestMessage.Headers.Add(header.Key, header.Value);
            }

            if (this.HttpProxy != null)
            {
                this.handler.Proxy = new SimpleProxy(this.HttpProxy);
            }

            this.handler.SkipCertificateValidation = this.SkipCertificateValidation;

            this.client.Timeout = this.Timeout;
            var startTime = DateTime.Now;

            try
            {
                var result = await this.client.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead, this.cancellationToken).ConfigureAwait(false);

                var headers = new HttpHeadersCollection(result.Headers);

                HttpContent content = null;
                if (result.Content != null)
                {
                    headers.AddRange(result.Content.Headers);

                    content = result.Content;
                }

                return new SimpleHttpResponse(content, headers, result.StatusCode);
            }
            catch (Exception ex)
            {
                var tcex = ex as TaskCanceledException;
                if (tcex == null)
                {
                    throw;
                }

                if (this.cancellationToken.IsCancellationRequested)
                {
                    throw new OperationCanceledException("The operation was canceled by user request.", tcex, this.cancellationToken);
                }

                if (DateTime.Now - startTime > this.Timeout)
                {
                    throw new TimeoutException(string.Format(CultureInfo.InvariantCulture, "The task failed to complete in the given timeout period ({0}).", this.Timeout), tcex);
                }

                throw;
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (this.handler != null)
            {
                this.handler.Dispose();
            }

            if (this.client != null)
            {
                this.client.Dispose();
            }

            this.disposed = true;
        }
    }
}