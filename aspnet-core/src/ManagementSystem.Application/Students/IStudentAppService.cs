using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ManagementSystem.Students.Dto;
using System.Threading.Tasks;

namespace ManagementSystem.Studentss
{
    //  public interface IStudentAppService : IAsyncCrudAppService<StudentDto, int, PagedStudentResultRequestDto, CreateStudentDto, StudentDto>
    public interface IStudentAppService :IApplicationService
    {
        /// <summary>
        /// Gets all Students.
        /// </summary>
        /// <returns>The list of Students.</returns>
        Task<ListResultDto<StudentDto>> GetAll();
      
        /// <summary>
        /// Gets a Student by its id.
        /// </summary>
        /// <param name="id">The id of the Student.</param>
        /// <returns>The Student.</returns>
        Task<StudentDto> GetById(int id);

        /// <summary>
        /// Creates a new Student.
        /// </summary>
        /// <param name="input">The Student to create.</param>
        /// <returns>The created Student.</returns>
        Task Create(CreateStudentDto input);

        /// <summary>
        /// Updates an existing Student.
        /// </summary>
        /// <param name="input">The Student to update.</param>
        /// <returns>The updated Student.</returns>
        Task Update(StudentDto input);

        /// <summary>
        /// Deletes a Student.
        /// </summary>
        /// <param name="input">The id of the Student.</param>
        /// <returns>The deleted Student.</returns>
        Task Delete(EntityDto input);


        /// <summary>
        /// Gets all Students.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<StudentDto>> GetPagedResultAsync(PagedStudentResultRequestDto input);
    }
}
