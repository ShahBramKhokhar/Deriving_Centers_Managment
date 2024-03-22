using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Common.Dto
{
    public interface ILookupDto<TKey> : IEntityDto<TKey>
    {
        public string Name { get; set; }
    }
    public class LookUpDto<Tkey> : EntityDto<Tkey>, ILookupDto<Tkey>
    {
        public string Name { get; set; }
    }
}
