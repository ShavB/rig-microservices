using Authentication.Service.Extensions;
using Authentication.Service.Interfaces;
using Authentication.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Service.Controllers;

[Route("api/portfolio")]
[ApiController]
public class PortfolioController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IStockRepository _stockRepo;
    private readonly IPortfolioRepository _portfolioRepository;

    public PortfolioController(
        UserManager<AppUser> userManager,
        IStockRepository stockRepo,
        IPortfolioRepository portfolioRepository
    )
    {
        _userManager = userManager;
        _stockRepo = stockRepo;
        _portfolioRepository = portfolioRepository;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserPortfolio()
    {
        var userName = User.GetUserName();
        var appUser = await _userManager.FindByNameAsync(userName);
        var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
        return Ok(userPortfolio);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddPortfolio(string Symbol)
    {
        var username = User.GetUserName();
        var appUser = await _userManager.FindByNameAsync(username);
        var stock = await _stockRepo.GetBySymbolAsync(Symbol);
        if (stock == null)
        {
            return NotFound("Stock not found");
        }
        var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
        if (userPortfolio.Any(e => e.Symbol.ToLower() == Symbol.ToLower()))
        {
            return NotFound("Cannot add stocks again!");
        }

        var portfolioModel = new PortFolio { StockId = stock.Id, AppuserId = appUser.Id };
        await _portfolioRepository.CreateAsync(portfolioModel);
        if (portfolioModel == null)
        {
            return StatusCode(500, "Could not create");
        }
        else
        {
            return Created();
        }
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeletePortfolio(string symbol)
    {
        var username = User.GetUserName();
        var appUser = await _userManager.FindByNameAsync(username);
        var portFolio = await _portfolioRepository.GetUserPortfolio(appUser);

        var filterStock = portFolio.Where(x => x.Symbol.ToLower() == symbol.ToLower());

        if (filterStock.Count() == 1)
        {
            await _portfolioRepository.DeletePortfolio(appUser, symbol);
        }
        else
        {
            return BadRequest("Stock not in your portfolio");
        }
        return Ok(appUser);
    }
}
