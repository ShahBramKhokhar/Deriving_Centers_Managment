using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ManagementSystem.Teacher.Dto;
using ManagementSystem.Teachers.Dto;
using System.Threading.Tasks;

namespace ManagementSystem.teacher
{
    //  public interface ICentreAppService : IAsyncCrudAppService<TeacherDto, int, PagedCentreResultRequestDto, CreateTeacherDto, TeacherDto>
    public interface ITeacherAppService :IApplicationService
    {
        /// <summary>
        /// Gets all teachers.
        /// </summary>
        /// <returns>The list of teachers.</returns>
        Task<ListResultDto<TeacherDto>> GetAll();
      
        /// <summary>
        /// Gets a teacher by its id.
        /// </summary>
        /// <param name="id">The id of the teacher.</param>
        /// <returns>The teacher.</returns>
        Task<TeacherDto> GetById(int id);

        /// <summary>
        /// Creates a new teacher.
        /// </summary>
        /// <param name="input">The teacher to create.</param>
        /// <returns>The created teacher.</returns>
        Task Create(CreateTeacherDto input);

        /// <summary>
        /// Updates an existing teacher.
        /// </summary>
        /// <param name="input">The teacher to update.</param>
        /// <returns>The updated teacher.</returns>
        Task Update(TeacherDto input);

        /// <summary>
        /// Deletes a teacher.
        /// </summary>
        /// <param name="input">The id of the teacher.</param>
        /// <returns>The deleted teacher.</returns>
        Task Delete(EntityDto input);


        /// <summary>
        /// Gets all teachers.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<TeacherDto>> GetPagedResultAsync(PagedTeacherResultRequestDto input);
    }
}
