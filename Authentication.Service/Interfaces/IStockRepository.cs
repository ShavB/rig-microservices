using Authentication.Service.DTO.Stocks;
using Authentication.Service.Helpers;
using Authentication.Service.Models;

namespace Authentication.Service.Interfaces;

public interface IStockRepository
{
    Task<List<Stock>> GetAllAsync(QueryObject query);
    Task<Stock?> GetById(int id);
    Task<Stock> CreateAsync(Stock stockModel);
    Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockRequestDto);
    Task<Stock?> DeleteAsync(int id);
    Task<bool> StockExists(int id);
    Task<Stock>? GetBySymbolAsync(string symbol);
}