using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMvcContext _context;

        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            IQueryable<SalesRecord> result = from saleRecord in _context.SalesRecord select saleRecord;

            if (minDate.HasValue)
            {
                result = result.Where(sr => sr.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(sr => sr.Date <= maxDate.Value);
            }

            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderBy(x => x.Date)
                .ToListAsync();
        }
    }
}
