using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace JB2.Common.Data
{
    public class AzureBlobRepository
    {
        private CloudBlobClient _blobclient;
        private CloudBlobContainer _container;


        public AzureBlobRepository(string containerName) : this(AzureHelper.StorageAccount, containerName)
        {

        }

        public AzureBlobRepository(CloudStorageAccount account, string containerName)
        {
            _blobclient = account.CreateCloudBlobClient();
            _container = _blobclient.GetContainerReference(containerName);

            _container.CreateIfNotExists();
        }

        public bool Insert(byte[] content, string blobName)
        {
            CloudBlockBlob blockBlob = _container.GetBlockBlobReference(blobName);

            using (var stream = new MemoryStream(content, writable: false))
            {
                blockBlob.UploadFromStream(stream);
            }

            return true;
        }

        public bool Insert(Stream stream, string blobName)
        {
            CloudBlockBlob blockBlob = _container.GetBlockBlobReference(blobName);

            blockBlob.UploadFromStream(stream);

            return true;

        }

        public byte[] GetByteArray(string blobName)
        {
            CloudBlockBlob blockBlob = _container.GetBlockBlobReference(blobName);

            byte[] result = null;

            blockBlob.DownloadToByteArray(result, 0);

            return result;
        }

        public Stream GetStream(string blobName)
        {
            CloudBlockBlob blockBlob = _container.GetBlockBlobReference(blobName);

            Stream result = null;

            blockBlob.DownloadToStream(result);

            return result;

        }

        public string GetUrl(string blobName)
        {
            CloudBlockBlob blockBlob = _container.GetBlockBlobReference(blobName);
            //Create an ad-hoc Shared Access Policy with read permissions which will expire in 12 hours
            SharedAccessBlobPolicy policy = new SharedAccessBlobPolicy()
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(12),
            };
            //Set content-disposition header for force download
            SharedAccessBlobHeaders headers = new SharedAccessBlobHeaders()
            {
                ContentDisposition = string.Format("attachment;filename=\"{0}\"",blobName),
            };

            var sasToken = blockBlob.GetSharedAccessSignature(policy, headers);
            return blockBlob.Uri.AbsoluteUri + sasToken;
        }
        
        public bool Delete(string blobName)
        {
            CloudBlockBlob blockBlob = _container.GetBlockBlobReference(blobName);

            // Delete the blob.
            blockBlob.Delete();

            return true;

            
        }

    

    }
}
