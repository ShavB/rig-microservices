using Authentication.Service.Models;

namespace Authentication.Service.Interfaces;

public interface IPortfolioRepository
{
    Task<List<Stock>> GetUserPortfolio(AppUser appUser);
    Task<PortFolio> CreateAsync(PortFolio portFolio);
    Task<PortFolio> DeletePortfolio(AppUser appUser, string symbol);
}