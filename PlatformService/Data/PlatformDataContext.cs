using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformService.Data
{
    public class PlatformDataContext:DbContext
    {
        public PlatformDataContext(DbContextOptions<PlatformDataContext> opt):base(opt)
        {

        }
        DbSet<Platform> Platforms { get; set; }
    }
}
