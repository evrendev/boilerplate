using EvrenDev.Application.Constants;
using EvrenDev.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EvrenDev.Infrastructure.Data.Seed
{
    public class DefaultDepartments
    {
        public static async Task<List<Department>> SeedAsync(ApplicationDbContext dbContext,
            ILoggerFactory loggerFactory, 
            int? retry = 0)
        {
            var log = loggerFactory.CreateLogger<DefaultDepartments>();
            int retryForAvailability = retry.Value;

            try
            {
                if (!await dbContext.Departments.AnyAsync())
                {
                    var departments = new List<Department>(){
                        new Department() 
                        {
                            Title = SeedSoftwareDepartmentInfo.DEFAULT_TITLE
                        },
                        new Department() 
                        {
                            Title = SeedAdminisrationDepartmentInfo.DEFAULT_TITLE
                        },
                        new Department() 
                        {
                            Title = SeedEditorialDepartmentInfo.DEFAULT_TITLE,
                        },
                        new Department() 
                        {
                            Title = SeedBasicUserDepartmentInfo.DEFAULT_TITLE,
                        }
                    };

                    await dbContext.Departments.AddRangeAsync(departments);

                    await dbContext.SaveChangesAsync();
                    
                    log.LogInformation("Default departments is created successfuly.");

                    return departments;
                } else {
                    var departments = await dbContext.Departments.ToListAsync();
                    
                    log.LogInformation("Departments getting is successfuly");

                    return departments;
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;

                    log.LogError(ex.Message);
                    await SeedAsync(dbContext, loggerFactory, retryForAvailability);
                }

                return null;
            }
        }
    }
}
