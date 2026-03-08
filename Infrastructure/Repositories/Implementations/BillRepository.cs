using Domain;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implementations
{
    public class BillRepository
    : GenericRepository<Bill>, IBillRepository
    {
        private readonly Context context;

        public BillRepository(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<Bill> GetByIdAsync(int id)
        {
            return await context.Bills
                .FirstOrDefaultAsync(b => b.IdBill == id);
        }

        public async Task<Bill> GetByIdWithItemsAsync(int id)
        {
            return await context.Bills
                .Include(b => b.BillItems)
                .FirstOrDefaultAsync(b => b.IdBill == id);
        }

        public async Task AddAsync(BillItem item)
        {
            
            var bill = await context.Bills
                .Include(b => b.BillItems)
                .FirstOrDefaultAsync(b => b.IdBill == item.IdBill);

            if (bill != null)
            {
                bill.BillItems.Add(item);
            }
        }
        public async Task<IEnumerable<Bill>> GetAllAsync()
        {
            return await context.Bills.ToListAsync(); 
        }
    }
}
