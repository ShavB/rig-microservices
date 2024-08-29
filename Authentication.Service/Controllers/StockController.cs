using Authentication.Service.DTO.Stocks;
using Authentication.Service.Helpers;
using Authentication.Service.Interfaces;
using Authentication.Service.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Service.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController(IStockRepository stockRepository) : ControllerBase
{
    private readonly IStockRepository _stockRepository = stockRepository;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] QueryObject queryObject)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var stocks = await _stockRepository.GetAllAsync(queryObject);
        var stockData = stocks.Select(x => x.ToStockDto()).ToList();
        return Ok(stocks);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var stock = await _stockRepository.GetById(id);
        if (stock != null)
        {
            return Ok(stock.ToStockDto());
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateData([FromBody] CreateStockRequestDto stockDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var stockModel = stockDto.ToStockRequestDto();
        await _stockRepository.CreateAsync(stockModel);
        return CreatedAtAction(
            nameof(GetById),
            new { id = stockModel.Id },
            stockModel.ToStockDto()
        );
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateData(
        [FromRoute] int id,
        [FromBody] UpdateStockRequestDto updateStockRequestDto
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var stock = await _stockRepository.UpdateAsync(id, updateStockRequestDto);
        if (stock == null)
        {
            return NotFound();
        }
        return Ok(stock.ToStockDto());
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteStock([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var stock = await _stockRepository.DeleteAsync(id);
        if (stock == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}
