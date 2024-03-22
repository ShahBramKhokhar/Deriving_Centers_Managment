using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ManagementSystem.Centres.Dto;
using ManagementSystem.Common.Dto;
using ManagementSystem.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Centres
{
  //  public interface ICentreAppService : IAsyncCrudAppService<CentreDto, int, PagedCentreResultRequestDto, CreateCentreDto, CentreDto>
    public interface ICentreAppService :IApplicationService
    {
        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns>The list of products.</returns>
        Task<ListResultDto<CentreDto>> GetAll();
      
        /// <summary>
        /// Gets a product by its id.
        /// </summary>
        /// <param name="id">The id of the product.</param>
        /// <returns>The product.</returns>
        Task<CentreDto> GetById(int id);

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="input">The product to create.</param>
        /// <returns>The created product.</returns>
        Task Create(CreateCentreDto input);

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="input">The product to update.</param>
        /// <returns>The updated product.</returns>
        Task Update(CentreDto input);

        /// <summary>
        /// Deletes a product.
        /// </summary>
        /// <param name="input">The id of the product.</param>
        /// <returns>The deleted product.</returns>
        Task Delete(EntityDto input);


        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<CentreDto>> GetPagedResultAsync(PagedCentreResultRequestDto input);
    }
}
