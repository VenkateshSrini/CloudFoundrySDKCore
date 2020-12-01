using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CloudFoundry.CloudController.Common.Http
{
    public interface ISimpleHttpClient
    {
        //CancellationToken CancellationToken { get; set; }
        Stream Content { get; set; }
        string ContentType { get; set; }
        IDictionary<string, string> Headers { get; }
        Uri HttpProxy { get; set; }
        HttpMethod Method { get; set; }
        bool SkipCertificateValidation { get; set; }
        TimeSpan Timeout { get; set; }
        Uri Uri { get; set; }

        void Dispose();
        Task<SimpleHttpResponse> SendAsync();
        Task<SimpleHttpResponse> SendAsync(HttpContent requestContent);
        Task<SimpleHttpResponse> SendAsync(IEnumerable<HttpMultipartFormData> multipartData);
    }
}