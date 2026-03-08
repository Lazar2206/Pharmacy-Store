using Domain;
using Infrastructure.Repositories.Interfaces;
using Infrastructure;
using Microsoft.EntityFrameworkCore; 
using System.Linq;                   

public class BillItemRepository : IBillItemRepository
{
    private readonly Context context;

    public BillItemRepository(Context context)
    {
        this.context = context;
    }

    
    public IQueryable<BillItem> Query()
    {
        return context.Bills.SelectMany(b => b.BillItems);
    }

    public async Task<List<BillItem>> GetAllAsync()
    {
        return await context.Bills
            .SelectMany(b => b.BillItems)
            .ToListAsync();
    }

    public async Task<int> GetNextRbAsync(int billId)
    {
        var bill = await context.Bills
            .Include(b => b.BillItems) 
            .FirstOrDefaultAsync(b => b.IdBill == billId);

        var lastRb = bill?.BillItems != null && bill.BillItems.Any()
            ? bill.BillItems.Max(x => x.Rb)
            : 0;

        return lastRb + 1;
    }


    public void Add(BillItem entity) {  }
    public void Update(BillItem entity) { /* EF prati promene automatski */ }
    public void Delete(BillItem entity) { /* Implementirati ako je potrebno */ }
    public IEnumerable<BillItem> GetAll() => Query().ToList();
    public BillItem GetById(int id) => null;
}