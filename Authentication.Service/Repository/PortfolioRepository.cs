using Authentication.Service.Data;
using Authentication.Service.Interfaces;
using Authentication.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Service.Repository;

public class PortfolioRepository : IPortfolioRepository
{
    private readonly ApplicationDBContext _applicationDBContext;

    public PortfolioRepository(ApplicationDBContext applicationDBContext)
    {
        _applicationDBContext = applicationDBContext;
    }

    public async Task<PortFolio> CreateAsync(PortFolio portFolio)
    {
        await _applicationDBContext.PortFolios.AddAsync(portFolio);
        _applicationDBContext.SaveChanges();
        return portFolio;
    }

    public async Task<PortFolio> DeletePortfolio(AppUser appUser, string symbol)
    {
        var portFolio = await _applicationDBContext.PortFolios.FirstOrDefaultAsync(x =>
            x.AppuserId == appUser.Id && x.Stock.Symbol.ToLower() == symbol.ToLower()
        );

        if (portFolio == null)
        {
            return null;
        }
        _applicationDBContext.PortFolios.Remove(portFolio);
        _applicationDBContext.SaveChanges();
        return portFolio;
    }

    public async Task<List<Stock>> GetUserPortfolio(AppUser appUser)
    {
        return await _applicationDBContext
            .PortFolios.Where(x => x.AppuserId == appUser.Id)
            .Select(stock => new Stock
            {
                Id = stock.StockId,
                Symbol = stock.Stock.Symbol,
                CompanyName = stock.Stock.CompanyName,
                Purchase = stock.Stock.Purchase,
                LastDiv = stock.Stock.LastDiv,
                Industry = stock.Stock.Industry,
                MarketCap = stock.Stock.MarketCap,
            })
            .ToListAsync();
    }
}
