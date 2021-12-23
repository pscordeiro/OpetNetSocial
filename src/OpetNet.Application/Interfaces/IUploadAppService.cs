using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace OpetNet.Application.Interfaces
{
    public interface IUploadAppService
    {
        Task UploadImagemPerfil(IFormFile formFile, string nome);
        void UploadImagemPost();
    }
}
