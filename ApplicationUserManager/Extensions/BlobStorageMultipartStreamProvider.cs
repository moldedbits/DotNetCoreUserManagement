using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Configuration;
using Microsoft.WindowsAzure.Storage;

namespace UserAppService.Utility.Extensions
{
    public class BlobStorageMultipartStreamProvider : MultipartStreamProvider
    {
        public string FileNameSansFileExtension { get; set; }

        public string FileUrl { get; set; }

        public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
        {
            var contentDisposition = headers.ContentDisposition;

            if (string.IsNullOrWhiteSpace(contentDisposition?.FileName)) return null;

            var connectionString = ConfigurationManager.ConnectionStrings["Azure"].ConnectionString;
            var containerName = WebConfigurationManager.AppSettings["DefaultImageContainer"].Replace("/", string.Empty);

            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var blobContainer = blobClient.GetContainerReference(containerName);

            var fname = contentDisposition.FileName;
            if (FileNameSansFileExtension.HasValue())
            {
                fname = $"{FileNameSansFileExtension}{contentDisposition.FileName.GetFileNameExtension()}";
            }

            var blob = blobContainer.GetBlockBlobReference(fname.Replace("\"", string.Empty));

            FileUrl = blob.Uri.AbsoluteUri;

            var stream = blob.OpenWrite();
            return stream;
        }
    }
}