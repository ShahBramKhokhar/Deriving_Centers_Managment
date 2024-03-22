using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ManagementSystem.Common.Dto;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public class ImageHelperService : IImageHelperService
    {
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;
        public ImageHelperService(
            IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _cloudinaryConfig = cloudinaryConfig;

            Account acc = new Account
            (
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }
        public async Task<ImageUploadDto> AddImageAsync(string base64String)
        {
            var uploadResult = new ImageUploadResult();
            if (string.IsNullOrWhiteSpace(base64String) == false)
            {
                uploadResult = await UploadToCloudnary(base64String);
            }

            var imageUploadResult = new ImageUploadDto();
            imageUploadResult.Url = uploadResult.Url == null ? string.Empty : uploadResult.Url.ToString();
            imageUploadResult.PublicId = string.IsNullOrWhiteSpace(uploadResult.PublicId) ? string.Empty : uploadResult.PublicId;

            return imageUploadResult;
        }

        public async Task<DeletionResult> DeleteImageAsync(string logoPublicId)
        {
            var result = new DeletionResult();
            if (string.IsNullOrWhiteSpace(logoPublicId) == false)
                result = await CreateDeleteParamsAndCallDestroyMethod(logoPublicId, result);

            return result;
        }

        private async Task<DeletionResult> CreateDeleteParamsAndCallDestroyMethod(string logoPublicId, DeletionResult result)
        {
            var deleteParams = new DeletionParams(logoPublicId);

            result = await _cloudinary.DestroyAsync(deleteParams);
            ThrowExceptionIfResultHasError(result);
            return result;
        }

        private static void ThrowExceptionIfResultHasError(DeletionResult result)
        {
            if (result.Error != null)
                throw new Exception(result.Error.Message);
        }

        private async Task<ImageUploadResult> UploadToCloudnary(string base64String)
        {
            var fileType = GetFileType(base64String);
            base64String = base64String.Substring(base64String.IndexOf(',') + 1);
            var buffer = Convert.FromBase64String(base64String);
            var fileName = string.Format("{0}.{1}", DateTime.Now.ToLongTimeString(), fileType);
           
            using (var stream = new MemoryStream(buffer))
                return await CreateImageUploadParamAndUploadFile(fileName, stream);
        }

        private async Task<ImageUploadResult> CreateImageUploadParamAndUploadFile(string fileName, MemoryStream stream)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(fileName, stream),
            };

            return await _cloudinary.UploadAsync(uploadParams);
        }

        private string GetFileType(string base64String)
        {
            var fromIndex = base64String.IndexOf('/') + 1;
            var toIndex = base64String.IndexOf(';');
            var length = toIndex - fromIndex;
            var fileType = base64String.Substring(fromIndex, length);
            return fileType;
        }
    }
}
