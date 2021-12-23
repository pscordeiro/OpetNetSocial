using Azure.Storage;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using OpetNet.Application.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace OpetNet.Application.Services
{
    public class UploadAppService : IUploadAppService
    {
        public async Task UploadImagemPerfil(IFormFile formFile, string nome)
        {
            try
            {
                Uri blobUri = new Uri(""+ nome);
                var storageCredentials = new StorageSharedKeyCredential("", "");

                BlobClient blobClient = new BlobClient(blobUri, storageCredentials);

                using (var reader = new MemoryStream())
                {
                    formFile.CopyTo(reader);
                    reader.Position = 0;
                    await blobClient.UploadAsync(reader, true);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e.Message);
            }
        }

        public void UploadImagemPost()
        {
            throw new System.NotImplementedException();
        }
    }
}
