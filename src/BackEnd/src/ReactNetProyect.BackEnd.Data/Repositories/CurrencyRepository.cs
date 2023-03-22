using Microsoft.EntityFrameworkCore;
using ReactNetProyect.BackEnd.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactNetProyect.BackEnd.Data.Repositories
{
    public class CurrencyRepository : IDisposable
    {
        private readonly ReactNetProyectContext _context;

        public CurrencyRepository(ReactNetProyectContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Currency>> GetAllCurrenciesAsync()
        {
            return await _context.Currencies.ToListAsync();
        }

        public async Task<Currency> GetCurrencyByIdAsync(int id)
        {
            return await _context.Currencies.FindAsync(id);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
