using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json;
using Apache.Web.Areas.HelpPage;

namespace Apache.Web.Controllers
{
    /// <summary>
    ///     Postman is a Chrome Extension that is used to test API endpoints. We expose a collection of all API end points for easy importing
    ///     into Postman. "Import a collection" using the api/v1/postman endpoint and save yourself an immense amount of typing. 
    /// 
    ///     Based on
    ///     http://blogs.msdn.com/b/yaohuang1/archive/2012/06/15/using-apiexplorer-to-export-api-information-to-postman-a-chrome-extension-for-testing-web-apis.aspx
    ///    Updates based on : https://github.com/a85/POSTMan-Chrome-Extension/issues/946
    /// </summary>
    [RoutePrefix("api")]
    //[ApiExplorerSettings(IgnoreApi = true)]
    public class PostmanApiController : ApiController
    {
        /// <summary>
        ///     Produce [POSTMAN](http://www.getpostman.com) related responses
        /// </summary>
        public PostmanApiController()
        {
            // exists for documentation purposes
        }

        private readonly Regex _pathVariableRegEx = new Regex("\\{([A-Za-z0-9-_]+)\\}", RegexOptions.ECMAScript | RegexOptions.Compiled);
        private readonly Regex _urlParameterVariableRegEx = new Regex("=\\{([A-Za-z0-9-_]+)\\}", RegexOptions.ECMAScript | RegexOptions.Compiled);

        /// <summary>
        ///     Get a postman collection of all visible Api
        ///     (Get the [POSTMAN](http://www.getpostman.com) chrome extension)
        /// </summary>
        /// <returns>object describing a POSTMAN collection</returns>
        /// <remarks>Get a postman collection of all visible api</remarks>
        //[Route(Name = "GetPostmanCollection")]
        [ResponseType(typeof(PostmanCollectionGet))]
        [Route("postman")]
        public IHttpActionResult GetPostmanCollection()
        {
            return Ok(this.PostmanCollectionForController());
        }

        private PostmanCollectionGet PostmanCollectionForController()
        {
            var requestUri = Request.RequestUri;
            var baseUri = requestUri.Scheme + "://" + requestUri.Host + ":" + requestUri.Port + HttpContext.Current.Request.ApplicationPath;

            var postManCollection = new PostmanCollectionGet
            {
                Id = Guid.NewGuid(),
                Name = "UserManagement API - " + requestUri.Host,
                Timestamp = DateTime.Now.Ticks,
                Requests = new Collection<PostmanRequestGet>(),
                Folders = new Collection<PostmanFolderGet>(),
                Synced = false,
                HasRequests = true,
                Owner = "144999999",
                Order = new Collection<Guid>(),
                Description = "UserManagement API"
            };


            var helpPageSampleGenerator = Configuration.GetHelpPageSampleGenerator();

            var apiExplorer = Configuration.Services.GetApiExplorer();

            var apiDescriptionsByController = apiExplorer.ApiDescriptions.GroupBy(description => description.ActionDescriptor.ActionBinding.ActionDescriptor.ControllerDescriptor.ControllerType);

            foreach( var apiDescriptionsByControllerGroup in apiDescriptionsByController )
            {
                var controllerName = apiDescriptionsByControllerGroup.Key.Name.Replace("Controller", string.Empty);

                var postManFolder = new PostmanFolderGet
                {
                    Id = Guid.NewGuid(),
                    CollectionId = postManCollection.Id,
                    Name = controllerName,
                    Description = string.Format("Api Methods for {0}", controllerName),
                    CollectionName = "api",
                    Order = new Collection<Guid>(),
                    Owner = "144999999"
                };

                foreach( var apiDescription in apiDescriptionsByControllerGroup
                    .OrderBy(description => description.HttpMethod, new HttpMethodComparator())
                    .ThenBy(description => description.RelativePath) )
                //.ThenBy(description => description.Documentation.ToString(CultureInfo.InvariantCulture)))
                {
                    TextSample sampleData = null;
                    var sampleDictionary = helpPageSampleGenerator.GetSample(apiDescription, SampleDirection.Request);
                    MediaTypeHeaderValue mediaTypeHeader;
                    if( MediaTypeHeaderValue.TryParse("application/json", out mediaTypeHeader) && sampleDictionary.ContainsKey(mediaTypeHeader) )
                    {
                        sampleData = sampleDictionary [ mediaTypeHeader ] as TextSample;
                    }

                    // scrub curly braces from url parameter values
                    var cleanedUrlParameterUrl = this._urlParameterVariableRegEx.Replace(apiDescription.RelativePath, "=$1-value");

                    // get pat variables from url
                    var pathVariables = this._pathVariableRegEx.Matches(cleanedUrlParameterUrl)
                        .Cast<Match>()
                        .Select(m => m.Value)
                        .Select(s => s.Substring(1, s.Length - 2))
                        .ToDictionary(s => s, s => string.Format("{0}-value", s));

                    // change format of parameters within string to be colon prefixed rather than curly brace wrapped
                    var postmanReadyUrl = this._pathVariableRegEx.Replace(cleanedUrlParameterUrl, ":$1");

                    // prefix url with base uri
                    var url = baseUri.TrimEnd('/') + "/" + postmanReadyUrl;

                    var request = new PostmanRequestGet
                    {
                        CollectionId = postManCollection.Id,
                        Id = Guid.NewGuid(),
                        Name = apiDescription.RelativePath,
                        Description = apiDescription.Documentation,
                        Url = url,
                        Method = apiDescription.HttpMethod.Method,
                        Headers = "Content-Type: application/json",
                        Data = sampleData == null
                            ? null
                            : sampleData.Text,
                        DataMode = "raw",
                        Time = postManCollection.Timestamp,
                        Synced = false,
                        DescriptionFormat = "markdown",
                        Version = "beta",
                        Responses = new Collection<string>(),
                        PathVariables = pathVariables,
                        Folder = postManFolder.Id
                    };

                    postManFolder.Order.Add(request.Id); // add to the folder
                    postManCollection.Requests.Add(request);
                }

                postManCollection.Folders.Add(postManFolder);
            }

            return postManCollection;
        }
    }

    /// <summary>
    ///     Quick comparer for ordering http methods for display
    /// </summary>
    internal class HttpMethodComparator : IComparer<HttpMethod>
    {
        private readonly string [] _order =
        {
            "GET",
            "POST",
            "PUT",
            "DELETE"
        };

        public int Compare(HttpMethod x, HttpMethod y)
        {
            return Array.IndexOf(this._order, x.ToString()).CompareTo(Array.IndexOf(this._order, y.ToString()));
        }
    }


    /// <summary>
    ///     [Postman](http://getpostman.com) collection representation
    /// </summary>
    public class PostmanCollectionGet
    {
        /// <summary>
        ///     Id of collection
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        /// <summary>
        ///     Name of collection
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        ///     Description of collection
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        ///     ordered list of ids of items in folder
        /// </summary>
        [JsonProperty(PropertyName = "order")]
        public ICollection<Guid> Order { get; set; }



        /// <summary>
        ///     folders within the collection
        /// </summary>
        [JsonProperty(PropertyName = "folders")]
        public ICollection<PostmanFolderGet> Folders { get; set; }

        /// <summary>
        ///     Collection generation time
        /// </summary>
        [JsonProperty(PropertyName = "timestamp")]
        public long Timestamp { get; set; }

        /// <summary>
        ///     Requests associated with the collection
        /// </summary>
        [JsonProperty(PropertyName = "requests")]
        public ICollection<PostmanRequestGet> Requests { get; set; }

        /// <summary>
        ///     **unused always false**
        /// </summary>
        [JsonProperty(PropertyName = "synced")]
        public bool Synced { get; set; }

        /// <summary>
        ///     **unused always true**
        /// </summary>
        [JsonProperty(PropertyName = "hasRequests")]
        public bool HasRequests { get; set; }


        /// <summary>
        ///     Owner of collection
        /// </summary>
        [JsonProperty(PropertyName = "owner")]
        public string Owner { get; set; }
    }


    /// <summary>
    ///     Object that describes a [Postman](http://getpostman.com) folder
    /// </summary>
    public class PostmanFolderGet
    {
        /// <summary>
        ///     id of the folder
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        /// <summary>
        ///     folder name
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        ///     folder description
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        ///     ordered list of ids of items in folder
        /// </summary>
        [JsonProperty(PropertyName = "order")]
        public ICollection<Guid> Order { get; set; }

        /// <summary>
        ///     Name of the collection
        /// </summary>
        [JsonProperty(PropertyName = "collection_name")]
        public string CollectionName { get; set; }

        /// <summary>
        ///     id of the collection
        /// </summary>
        [JsonProperty(PropertyName = "collection_id")]
        public Guid CollectionId { get; set; }

        [JsonProperty(PropertyName = "owner")]
        public string Owner { get; set; }
    }


    /// <summary>
    ///     [Postman](http://getpostman.com) request object
    /// </summary>
    public class PostmanRequestGet
    {
        /// <summary>
        ///     id of request
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        /// <summary>
        ///     headers associated with the request
        /// </summary>
        [JsonProperty(PropertyName = "headers")]
        public string Headers { get; set; }

        /// <summary>
        ///     url of the request
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        /// <summary>
        ///     path variables of the request
        /// </summary>
        [JsonProperty(PropertyName = "pathVariables")]
        public Dictionary<string, string> PathVariables { get; set; }

        /// <summary>
        ///     method of request
        /// </summary>
        [JsonProperty(PropertyName = "method")]
        public string Method { get; set; }

        /// <summary>
        ///     data to be sent with the request
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public string Data { get; set; }

        /// <summary>
        ///     data mode of reqeust
        /// </summary>
        [JsonProperty(PropertyName = "dataMode")]
        public string DataMode { get; set; }

        /// <summary>
        ///     name of request
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        ///     request description
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        ///     format of description
        /// </summary>
        [JsonProperty(PropertyName = "descriptionFormat")]
        public string DescriptionFormat { get; set; }

        /// <summary>
        ///     time that this request object was generated
        /// </summary>
        [JsonProperty(PropertyName = "time")]
        public long Time { get; set; }

        /// <summary>
        ///     version of the request object
        /// </summary>
        [JsonProperty(PropertyName = "version")]
        public string Version { get; set; }

        /// <summary>
        ///     request response
        /// </summary>
        [JsonProperty(PropertyName = "responses")]
        public ICollection<string> Responses { get; set; }

        /// <summary>
        ///     the id of the collection that the request object belongs to
        /// </summary>
        [JsonProperty(PropertyName = "collectionId")]
        public Guid CollectionId { get; set; }

        /// <summary>
        ///     Synching
        /// </summary>
        [JsonProperty(PropertyName = "synced")]
        public bool Synced { get; set; }

        [JsonProperty(PropertyName = "Folder")]
        public Guid Folder { get; set; }
    }
}
