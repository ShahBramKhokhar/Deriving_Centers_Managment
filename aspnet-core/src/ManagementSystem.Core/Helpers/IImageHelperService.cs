using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Services;
using CloudinaryDotNet.Actions;
using ManagementSystem.Common.Dto;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public interface IImageHelperService:IDomainService
    {
        Task<ImageUploadDto> AddImageAsync(string base64String);
        Task<DeletionResult> DeleteImageAsync(string logoPublicId);
    }
}
