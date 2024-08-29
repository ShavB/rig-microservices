using Authentication.Service.Data;
using Authentication.Service.DTO.Stocks;
using Authentication.Service.Helpers;
using Authentication.Service.Interfaces;
using Authentication.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Service.Repository;

public class StockRepository : IStockRepository
{
    private readonly ApplicationDBContext _context;

    public StockRepository(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<Stock> CreateAsync(Stock stockModel)
    {
        await _context.Stocks.AddAsync(stockModel);
        await _context.SaveChangesAsync();
        return stockModel;
    }

    public async Task<Stock?> DeleteAsync(int id)
    {
        var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
        if (stock == null)
        {
            return null;
        }
        _context.Remove(stock);
        await _context.SaveChangesAsync();
        return stock;
    }

    public async Task<List<Stock>> GetAllAsync(QueryObject query)
    {
        var stock = _context
            .Stocks.Include(com => com.Comments)
            .ThenInclude(a => a.AppUser)
            .AsQueryable();
        if (!string.IsNullOrWhiteSpace(query.CompanyName))
        {
            stock = stock.Where(s => s.CompanyName.Contains(query.CompanyName));
        }
        if (!string.IsNullOrWhiteSpace(query.Symbol))
        {
            stock = stock.Where(s => s.Symbol.Contains(query.Symbol));
        }

        if (!string.IsNullOrWhiteSpace(query.SortBy))
        {
            if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
            {
                stock = query.IsDescending
                    ? stock.OrderByDescending(s => s.Symbol)
                    : stock.OrderBy(s => s.Symbol);
            }
        }

        var skipNumber = (query.PageNumber - 1) * query.PageSize;

        return await stock.Skip(skipNumber).Take(query.PageSize).ToListAsync();
    }

    public async Task<Stock?> GetById(int id)
    {
        var stock = await _context
            .Stocks.Include(com => com.Comments)
            .FirstOrDefaultAsync(i => i.Id == id);
        if (stock == null)
        {
            return null;
        }
        return stock;
    }

    public async Task<Stock> GetBySymbolAsync(string symbol)
    {
        return await _context.Stocks.FirstOrDefaultAsync(s => s.Symbol == symbol);
    }

    public async Task<bool> StockExists(int id)
    {
        return await _context.Stocks.AnyAsync(i => i.Id == id);
    }

    public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockRequestDto)
    {
        var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
        if (stock == null)
        {
            return null;
        }
        stock.Symbol = stockRequestDto.Symbol;
        stock.CompanyName = stockRequestDto.CompanyName;
        stock.Purchase = stockRequestDto.Purchase;
        stock.LastDiv = stockRequestDto.LastDiv;
        stock.Industry = stockRequestDto.Industry;
        stock.MarketCap = stockRequestDto.MarketCap;
        await _context.SaveChangesAsync();
        return stock;
    }
}
