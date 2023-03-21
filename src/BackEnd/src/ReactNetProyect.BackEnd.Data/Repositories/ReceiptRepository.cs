using Microsoft.EntityFrameworkCore;
using ReactNetProyect.BackEnd.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactNetProyect.BackEnd.Data.Repositories
{
    public class ReceiptRepository : IDisposable
    {
        private readonly ReactNetProyectContext _context;

        public ReceiptRepository(ReactNetProyectContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Receipt>> GetAllReceiptsAsync()
        {
            return await _context.Receipts
                .Include(r => r.Currency)
                .ToListAsync();
        }

        public async Task<Receipt> GetReceiptByIdAsync(int id)
        {
            return await _context.Receipts
                .Include(r => r.Currency)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddReceiptAsync(Receipt receipt)
        {
            await _context.Receipts.AddAsync(receipt);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReceiptAsync(Receipt receipt)
        {
            _context.Entry(receipt).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReceiptAsync(int id)
        {
            var receipt = await _context.Receipts.FindAsync(id);
            if (receipt != null)
            {
                _context.Receipts.Remove(receipt);
                await _context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
