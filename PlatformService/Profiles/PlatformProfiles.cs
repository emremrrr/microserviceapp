using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformService
{
    public class PlatformProfiles:Profile
    {
        public PlatformProfiles()
        {
            CreateMap<PlatformCreateDto,Platform>();
            CreateMap<Platform, PlatformReadDto>();

        }
    }
}
