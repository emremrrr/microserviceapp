using MicroServiceProject.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformService
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<IUnitOfWork<PlatformDataContext>>());
            }
        }

        private static void SeedData(IUnitOfWork<PlatformDataContext> context)
        {
            var repo = context.GetRepository<Platform>();
            if (!repo.GetAllAsync().Result.Any())
            {
                repo.InsertRange(new List<Platform> {
                 new Platform{Name="Dot Net",Publisher="Microsoft",Cost="Free" },
                 new Platform{Name="SQL Server",Publisher="Microsoft",Cost="Free" },
                 new Platform{Name="Kubernetes",Publisher="Cloud Native Computing Foundation",Cost="Free" }
                });
                var res= context.CommitAsync().Result;
            }
        }
    }
}
