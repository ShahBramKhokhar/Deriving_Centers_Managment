using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ManagementSystem.ClassArea.Dto;
using ManagementSystem.ClassAreas.Dto;
using System.Threading.Tasks;

namespace ManagementSystem.ClassArea
{
    //  public interface ICentreAppService : IAsyncCrudAppService<ClassAreaDto, int, PagedCentreResultRequestDto, CreateClassAreaDto, ClassAreaDto>
    public interface IClassAreaAppService :IApplicationService
    {
       
      
        /// <summary>
        /// Gets a ClassArea by its id.
        /// </summary>
        /// <param name="id">The id of the ClassArea.</param>
        /// <returns>The ClassArea.</returns>
        Task<ClassAreaDto> GetById(int id);

        /// <summary>
        /// Creates a new ClassArea.
        /// </summary>
        /// <param name="input">The ClassArea to create.</param>
        /// <returns>The created ClassArea.</returns>
        Task Create(CreateClassAreaDto input);

        /// <summary>
        /// Updates an existing ClassArea.
        /// </summary>
        /// <param name="input">The ClassArea to update.</param>
        /// <returns>The updated ClassArea.</returns>
        Task Update(CreateClassAreaDto input);

        /// <summary>
        /// Deletes a ClassArea.
        /// </summary>
        /// <param name="input">The id of the ClassArea.</param>
        /// <returns>The deleted ClassArea.</returns>
        Task Delete(EntityDto input);


        /// <summary>
        /// Gets all ClassAreas.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<ClassAreaDto>> GetPagedResultAsync(PagedClassAreaResultRequestDto input);
    }
}
