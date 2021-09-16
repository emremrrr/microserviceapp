using AutoMapper;
using MicroServiceProject.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PlatformDataContext> _unitOfWork;

        public PlatformController(IMapper mapper, IUnitOfWork<PlatformDataContext> unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var platformRepo = _unitOfWork.GetRepository<Platform>();
            return new JsonResult(await platformRepo.GetAllAsync());
        }
         
    }
}
