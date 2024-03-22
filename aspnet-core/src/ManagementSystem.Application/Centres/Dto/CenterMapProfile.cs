using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Centres.Dto
{
    public class CenterMapProfile:Profile
    {
        public CenterMapProfile()
        {
            //CreateMap<CentreDto, Centre>();
            //CreateMap<Centre, CentreDto>();
            //CreateMap<CentreDto, Centre>()
            //    .ForMember(x => x.Id, opt => opt.Ignore())
            //    .ForMember(g => g.CreationTime, opt => opt.Ignore())
            //    .ForMember(g => g.CreatorUserId, opt => opt.Ignore());

            //CreateMap<CentreDto, CentreDto>()
            //      .ForMember(g => g.CreationTime, opt => opt.Ignore())
            //        .ForMember(g => g.CreatorUserId, opt => opt.Ignore());


        }
    }
}
