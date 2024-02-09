using Microsoft.AspNetCore.Mvc;
using RocketseatAuction.API.Entites;
using RocketseatAuction.API.UseCases.Auctions.GetCurrent;

namespace RocketseatAuction.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[ProducesResponseType(typeof(Auction),StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status204NoContent)]
public class AuctionController : ControllerBase
{
    [HttpGet]
    public IActionResult GetCurrentAuction()
    {
        var useCase = new GetCurrentAuctionUseCase();
        
        var result = useCase.Execute();
        
        if (result is null)
            return NoContent();
        
        return Ok(result);
    }
}