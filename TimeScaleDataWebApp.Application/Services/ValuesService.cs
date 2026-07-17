using Microsoft.EntityFrameworkCore;
using TimeScaleDataWebApp.Domain.Entities;
using TimeScaleDataWebApp.Infrastructure;

namespace TimeScaleDataWebApp.Application.Services;

public class ValuesService (ApplicationDbContext context)
{
    public async Task<List<Values>> GetSortedValues(string fileName)
    {
        return await context.Values
            .Where(x => x.FileName == fileName)
            .OrderByDescending(x => x.Date)
            .Take(10)
            .ToListAsync();
    }
}