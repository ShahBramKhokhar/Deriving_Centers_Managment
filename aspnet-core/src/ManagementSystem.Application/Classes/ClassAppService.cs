using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using Core.Helpers;
using ManagementSystem.ClassArea;
using ManagementSystem.ClassArea.Dto;
using ManagementSystem.ClassAreas.Dto;
using ManagementSystem.Classess;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.ClassAreas
{

    public class ClassAreaAppService : ManagementSystemServiceBase , IClassAreaAppService 
    {
        private readonly IRepository<ManagementSystem.Classess.ClassArea> _repository;
        private readonly IRepository<StudentsClasses> _studentClassRepository; 

        public ClassAreaAppService( IRepository<ManagementSystem.Classess.ClassArea> repository,IRepository<StudentsClasses> studentClassRepository ) 
        {
            _repository = repository;
           _studentClassRepository = studentClassRepository;
        }

        public async Task Create(CreateClassAreaDto input)
        {

            var tenantId = AbpSession.TenantId ?? AppConstants.DefaultTenantId;
            input.TenantId = tenantId;

            var ClassArea = new ManagementSystem.Classess.ClassArea()
            {
                TenantId = tenantId,
                CreationTime = new System.DateTime(),
                LastModificationTime = new System.DateTime(),
                StartTime = input.StartTime.ConvertDateTimeStringToDateTime(),
                EndTime = input.EndTime.ConvertDateTimeStringToDateTime(),
                TeacherId = input.TeacherId,
                Title = input.Title,
                Descrtipton = input.Descrtipton,
                TotalFees = input.TotalFees,
            };

            await _repository.InsertAsync(ClassArea);
            UnitOfWorkManager.Current.SaveChanges();


            foreach (var std in input.studentIds)
            {
                var studentClass = new StudentsClasses();
                studentClass.StudentId = std;
                studentClass.ClassId = ClassArea.Id;

                _studentClassRepository.Insert(studentClass);
                UnitOfWorkManager.Current.SaveChanges();

            }
            
        }


        public async Task Delete(EntityDto input)
        {
            var data = await _repository.GetAsync(input.Id);
            if (data == null)
            {
                throw new UserFriendlyException(AppConstants.ErrorMessages.ClassAreaNotFound);
            }
            await _repository.DeleteAsync(data);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }


        private async Task<ClassAreaDto> ClassAreaToClassAreaDto(ManagementSystem.Classess.ClassArea item)
        {
            var data = new ClassAreaDto
            {
                Id = item.Id,
                TenantId = item.TenantId,
                StartTime = item.StartTime,
                EndTime = item.EndTime,
                TeacherId = item.TeacherId,
                Title = item.Title,
                Descrtipton = item.Descrtipton,
                studentIds = new List<int>()
            };
            var list = await _studentClassRepository.GetAllListAsync(x => x.ClassId == item.Id);
            foreach (var record in list)
            {
                data.studentIds.Add(record.StudentId);

                //var oldStudentsInClass = await _studentClassRepository.GetAllListAsync(x => x.ClassId == record.Id);
                //foreach (var item1 in oldStudentsInClass)
                //{
                //    data.studentIds.Append(item1.StudentId);
                //}

            }

            return data;
        }

        public async Task<ClassAreaDto> GetById(int id)
        {
            var data = await _repository.GetAsync(id);
            var ClassAreaDto = await ClassAreaToClassAreaDto(data);
            return ClassAreaDto;
        }

        public async Task<PagedResultDto<ClassAreaDto>> GetPagedResultAsync(PagedClassAreaResultRequestDto input)
        {
          
            var query = _repository.GetAll();
            query = ApplyFilters(input, query);
            IQueryable<ClassAreaDto> selectQuery = GetSelectQuery(query);

            var data = selectQuery.ToList();
            return await selectQuery.GetPagedResultAsync(input.SkipCount, input.MaxResultCount);
        }

        public async Task Update(CreateClassAreaDto input)
        {
            var data = await _repository.GetAsync(input.Id.Value);

            if (data == null)
            {
                throw new UserFriendlyException(AppConstants.ErrorMessages.ClassAreaNotFound);
            }
            data.Title = input.Title;
            data.Descrtipton = input.Descrtipton;
            data.TeacherId = input.TeacherId;
            data.LastModificationTime = new System.DateTime();
            data.StartTime = input.StartTime.ConvertDateTimeStringToDateTime();
            data.EndTime = input.EndTime.ConvertDateTimeStringToDateTime();
            var oldStudentsInClass = await _studentClassRepository.GetAllListAsync(x => x.ClassId == input.Id);
            foreach (var item in oldStudentsInClass)
            {
                await _studentClassRepository.DeleteAsync(item);

            }
            UnitOfWorkManager.Current.SaveChanges();
            foreach (var std in input.studentIds)
            {
                var studentClass = new StudentsClasses();
                studentClass.StudentId = std;
                studentClass.ClassId = data.Id;

                _studentClassRepository.Insert(studentClass);
                UnitOfWorkManager.Current.SaveChanges();

            }

            await _repository.UpdateAsync(data);
            await UnitOfWorkManager.Current.SaveChangesAsync();
           // ObjectMapper.Map(input, data);
        }

        #region Private Methods
        private static IQueryable<ManagementSystem.Classess.ClassArea> ApplyFilters(PagedClassAreaResultRequestDto input, IQueryable<ManagementSystem.Classess.ClassArea> query)
        {
            if (string.IsNullOrWhiteSpace(input.Keyword) == false)
                query = query.Where(g => g.Title.Contains(input.Keyword));
            return query;
        }
        private static IQueryable<ClassAreaDto> GetSelectQuery(IQueryable<ManagementSystem.Classess.ClassArea> query)
        {
            return query.Select(item => new ClassAreaDto
            {
                Id = item.Id,
                TenantId = item.TenantId,
                StartTime = item.StartTime,
                EndTime = item.EndTime,
                TeacherId = item.TeacherId,
                Title = item.Title,
                Descrtipton = item.Descrtipton,
                TotalFees = item.TotalFees
            });
        }

        #endregion

    }
}
