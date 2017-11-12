//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

//
// This source code was auto-generated by cf-sdk-builder
//

using CloudFoundry.CloudController.Common;
using CloudFoundry.CloudController.V2.Client.Data;
using Newtonsoft.Json;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace CloudFoundry.CloudController.V2.Client
{
    /// <summary>
    /// Buildpacks Endpoint
    /// </summary>
    [GeneratedCodeAttribute("cf-sdk-builder", "1.0.0.0")]
    public partial class BuildpacksEndpoint : CloudFoundry.CloudController.V2.Client.Base.AbstractBuildpacksEndpoint
    {
        internal BuildpacksEndpoint(CloudFoundryClient client) : base()
        {
            this.Client = client;
        }
    }
}

namespace CloudFoundry.CloudController.V2.Client.Base
{
    /// <summary>
    /// Base abstract class for Buildpacks Endpoint
    /// </summary>
    [GeneratedCodeAttribute("cf-sdk-builder", "1.0.0.0")]
    public abstract class AbstractBuildpacksEndpoint : BaseEndpoint
    {
        /// <summary>
        /// Initializes the class
        /// </summary>
        protected AbstractBuildpacksEndpoint()
        {
        }

        /// <summary>
        /// Enable or disable a Buildpack
        /// <para>For detailed information, see online documentation at: "http://apidocs.cloudfoundry.org/250/buildpacks/enable_or_disable_a_buildpack.html"</para>
        /// </summary>
        public async Task<EnableOrDisableBuildpackResponse> EnableOrDisableBuildpack(Guid? guid, EnableOrDisableBuildpackRequest value)
        {
            UriBuilder uriBuilder = new UriBuilder(this.Client.CloudTarget);
            uriBuilder.Path = string.Format(CultureInfo.InvariantCulture, "/v2/buildpacks/{0}", guid);
            var client = this.GetHttpClient();
            client.Uri = uriBuilder.Uri;
            client.Method = HttpMethod.Put;
            var authHeader = await BuildAuthenticationHeader();
            if (!string.IsNullOrWhiteSpace(authHeader.Key))
            {
                client.Headers.Add(authHeader);
            }
            client.ContentType = "application/x-www-form-urlencoded";
            client.Content = ((string)JsonConvert.SerializeObject(value)).ConvertToStream();
            var expectedReturnStatus = 201;
            var response = await this.SendAsync(client, expectedReturnStatus);
            return Utilities.DeserializeJson<EnableOrDisableBuildpackResponse>(await response.ReadContentAsStringAsync());
        }

        /// <summary>
        /// Retrieve a Particular Buildpack
        /// <para>For detailed information, see online documentation at: "http://apidocs.cloudfoundry.org/250/buildpacks/retrieve_a_particular_buildpack.html"</para>
        /// </summary>
        public async Task<RetrieveBuildpackResponse> RetrieveBuildpack(Guid? guid)
        {
            UriBuilder uriBuilder = new UriBuilder(this.Client.CloudTarget);
            uriBuilder.Path = string.Format(CultureInfo.InvariantCulture, "/v2/buildpacks/{0}", guid);
            var client = this.GetHttpClient();
            client.Uri = uriBuilder.Uri;
            client.Method = HttpMethod.Get;
            var authHeader = await BuildAuthenticationHeader();
            if (!string.IsNullOrWhiteSpace(authHeader.Key))
            {
                client.Headers.Add(authHeader);
            }
            var expectedReturnStatus = 200;
            var response = await this.SendAsync(client, expectedReturnStatus);
            return Utilities.DeserializeJson<RetrieveBuildpackResponse>(await response.ReadContentAsStringAsync());
        }

        /// <summary>
        /// List all Buildpacks
        /// <para>For detailed information, see online documentation at: "http://apidocs.cloudfoundry.org/250/buildpacks/list_all_buildpacks.html"</para>
        /// </summary>
        public async Task<PagedResponseCollection<ListAllBuildpacksResponse>> ListAllBuildpacks()
        {
            return await ListAllBuildpacks(new RequestOptions());
        }

        /// <summary>
        /// List all Buildpacks
        /// <para>For detailed information, see online documentation at: "http://apidocs.cloudfoundry.org/250/buildpacks/list_all_buildpacks.html"</para>
        /// </summary>
        public async Task<PagedResponseCollection<ListAllBuildpacksResponse>> ListAllBuildpacks(RequestOptions options)
        {
            UriBuilder uriBuilder = new UriBuilder(this.Client.CloudTarget);
            uriBuilder.Path = "/v2/buildpacks";
            uriBuilder.Query = options.ToString();
            var client = this.GetHttpClient();
            client.Uri = uriBuilder.Uri;
            client.Method = HttpMethod.Get;
            var authHeader = await BuildAuthenticationHeader();
            if (!string.IsNullOrWhiteSpace(authHeader.Key))
            {
                client.Headers.Add(authHeader);
            }
            var expectedReturnStatus = 200;
            var response = await this.SendAsync(client, expectedReturnStatus);
            return Utilities.DeserializePage<ListAllBuildpacksResponse>(await response.ReadContentAsStringAsync(), this.Client);
        }

        /// <summary>
        /// Lock or unlock a Buildpack
        /// <para>For detailed information, see online documentation at: "http://apidocs.cloudfoundry.org/250/buildpacks/lock_or_unlock_a_buildpack.html"</para>
        /// </summary>
        public async Task<LockOrUnlockBuildpackResponse> LockOrUnlockBuildpack(Guid? guid, LockOrUnlockBuildpackRequest value)
        {
            UriBuilder uriBuilder = new UriBuilder(this.Client.CloudTarget);
            uriBuilder.Path = string.Format(CultureInfo.InvariantCulture, "/v2/buildpacks/{0}", guid);
            var client = this.GetHttpClient();
            client.Uri = uriBuilder.Uri;
            client.Method = HttpMethod.Put;
            var authHeader = await BuildAuthenticationHeader();
            if (!string.IsNullOrWhiteSpace(authHeader.Key))
            {
                client.Headers.Add(authHeader);
            }
            client.ContentType = "application/x-www-form-urlencoded";
            client.Content = ((string)JsonConvert.SerializeObject(value)).ConvertToStream();
            var expectedReturnStatus = 201;
            var response = await this.SendAsync(client, expectedReturnStatus);
            return Utilities.DeserializeJson<LockOrUnlockBuildpackResponse>(await response.ReadContentAsStringAsync());
        }

        /// <summary>
        /// Delete a Particular Buildpack
        /// <para>For detailed information, see online documentation at: "http://apidocs.cloudfoundry.org/250/buildpacks/delete_a_particular_buildpack.html"</para>
        /// </summary>
        public async Task DeleteBuildpack(Guid? guid)
        {
            UriBuilder uriBuilder = new UriBuilder(this.Client.CloudTarget);
            uriBuilder.Path = string.Format(CultureInfo.InvariantCulture, "/v2/buildpacks/{0}", guid);
            var client = this.GetHttpClient();
            client.Uri = uriBuilder.Uri;
            client.Method = HttpMethod.Delete;
            var authHeader = await BuildAuthenticationHeader();
            if (!string.IsNullOrWhiteSpace(authHeader.Key))
            {
                client.Headers.Add(authHeader);
            }
            client.ContentType = "application/x-www-form-urlencoded";
            var expectedReturnStatus = 204;
            var response = await this.SendAsync(client, expectedReturnStatus);
        }

        /// <summary>
        /// Creates an admin Buildpack
        /// <para>For detailed information, see online documentation at: "http://apidocs.cloudfoundry.org/250/buildpacks/creates_an_admin_buildpack.html"</para>
        /// </summary>
        public async Task<CreatesAdminBuildpackResponse> CreatesAdminBuildpack(CreatesAdminBuildpackRequest value)
        {
            UriBuilder uriBuilder = new UriBuilder(this.Client.CloudTarget);
            uriBuilder.Path = "/v2/buildpacks";
            var client = this.GetHttpClient();
            client.Uri = uriBuilder.Uri;
            client.Method = HttpMethod.Post;
            var authHeader = await BuildAuthenticationHeader();
            if (!string.IsNullOrWhiteSpace(authHeader.Key))
            {
                client.Headers.Add(authHeader);
            }
            client.ContentType = "application/x-www-form-urlencoded";
            client.Content = ((string)JsonConvert.SerializeObject(value)).ConvertToStream();
            var expectedReturnStatus = 201;
            var response = await this.SendAsync(client, expectedReturnStatus);
            return Utilities.DeserializeJson<CreatesAdminBuildpackResponse>(await response.ReadContentAsStringAsync());
        }

        /// <summary>
        /// Change the position of a Buildpack
        /// <para>Buildpacks are maintained in an ordered list.  If the target position is already occupied,</para>
        /// <para>the entries will be shifted down the list to make room.  If the target position is beyond</para>
        /// <para>the end of the current list, the buildpack will be positioned at the end of the list.</para>
        /// <para>For detailed information, see online documentation at: "http://apidocs.cloudfoundry.org/250/buildpacks/change_the_position_of_a_buildpack.html"</para>
        /// </summary>
        public async Task<ChangePositionOfBuildpackResponse> ChangePositionOfBuildpack(Guid? guid, ChangePositionOfBuildpackRequest value)
        {
            UriBuilder uriBuilder = new UriBuilder(this.Client.CloudTarget);
            uriBuilder.Path = string.Format(CultureInfo.InvariantCulture, "/v2/buildpacks/{0}", guid);
            var client = this.GetHttpClient();
            client.Uri = uriBuilder.Uri;
            client.Method = HttpMethod.Put;
            var authHeader = await BuildAuthenticationHeader();
            if (!string.IsNullOrWhiteSpace(authHeader.Key))
            {
                client.Headers.Add(authHeader);
            }
            client.ContentType = "application/x-www-form-urlencoded";
            client.Content = ((string)JsonConvert.SerializeObject(value)).ConvertToStream();
            var expectedReturnStatus = 201;
            var response = await this.SendAsync(client, expectedReturnStatus);
            return Utilities.DeserializeJson<ChangePositionOfBuildpackResponse>(await response.ReadContentAsStringAsync());
        }
    }
}