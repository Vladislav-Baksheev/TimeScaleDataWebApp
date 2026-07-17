using Microsoft.EntityFrameworkCore;
using TimeScaleDataWebApp.Application.DTO;
using TimeScaleDataWebApp.Domain.Entities;
using TimeScaleDataWebApp.Infrastructure;

namespace TimeScaleDataWebApp.Application.Services;

public class FilterService (ApplicationDbContext context)
{
    public async Task<List<Results>> GetResultsAsync(FilterRequest filter)
    {
        IQueryable<Results> query = context.Results;

        if (!string.IsNullOrWhiteSpace(filter.FileName))
        {
            query = query.Where(x => x.FileName == filter.FileName);
        }

        if (filter.DateFrom.HasValue)
        {
            query = query.Where(x => x.StartDate >= filter.DateFrom.Value);
        }
        
        if (filter.DateTo.HasValue)
        {
            query = query.Where(x => x.StartDate <= filter.DateTo.Value);
        }
        
        if (filter.AvgValueFrom.HasValue)
        {
            query = query.Where(x => x.AvgValue >= filter.AvgValueFrom.Value);
        }
        
        if (filter.AvgValueTo.HasValue)
        {
            query = query.Where(x => x.AvgValue <= filter.AvgValueTo.Value);
        }
        
        if (filter.AvgExecutionTimeFrom.HasValue)
        {
            query = query.Where(x => x.AvgExecutionTime >= filter.AvgExecutionTimeFrom.Value);
        }
        
        if (filter.AvgExecutionTimeTo.HasValue)
        {
            query = query.Where(x => x.AvgExecutionTime <= filter.AvgExecutionTimeTo.Value);
        }

        return await query.ToListAsync();
    }
}