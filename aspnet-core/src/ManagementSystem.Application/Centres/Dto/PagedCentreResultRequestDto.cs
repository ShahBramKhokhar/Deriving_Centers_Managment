﻿using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Centres.Dto
{
    public class PagedCentreResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
