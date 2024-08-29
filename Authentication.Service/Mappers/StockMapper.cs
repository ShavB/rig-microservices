using Authentication.Service.DTO.Stocks;
using Authentication.Service.Models;

namespace Authentication.Service.Mappers;

public static class StockMapper
{
    public static StockDto ToStockDto(this Stock stockModel)
    {
        return new StockDto
        {
            Id= stockModel.Id,
            Symbol= stockModel.Symbol,
            CompanyName = stockModel.CompanyName,
            Purchase = stockModel.Purchase,
            LastDiv = stockModel.LastDiv,
            Industry = stockModel.Industry,
            MarketCap = stockModel.MarketCap,
            CommentDtos = stockModel.Comments.Select(x => x.ToCommentDto()).ToList(),
        };
    }

    public static Stock ToStockRequestDto(this CreateStockRequestDto createStockRequestDto)
    {
        return new Stock
        {
            Symbol= createStockRequestDto.Symbol,
            CompanyName = createStockRequestDto.CompanyName,
            Purchase = createStockRequestDto.Purchase,
            LastDiv = createStockRequestDto.LastDiv,
            Industry = createStockRequestDto.Industry,
            MarketCap = createStockRequestDto.MarketCap,
        };
    }
}