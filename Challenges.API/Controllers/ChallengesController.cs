using Challenges.Application.Challenges.Commands.CreateChallenge;
using Challenges.Application.Challenges.DTOs;
using Challenges.Application.Challenges.Queries.GetChallengeById;
using Challenges.Domain.Ids;
using Enmeshed.BuildingBlocks.API;
using Enmeshed.BuildingBlocks.API.Mvc;
using Enmeshed.BuildingBlocks.API.Mvc.ControllerAttributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Challenges.API.Controllers;

[Route("api/v1/[controller]")]
[Authorize]
public class ChallengesController : ApiControllerBase
{
    public ChallengesController(IMediator mediator) : base(mediator) { }

    [HttpPost]
    [ProducesResponseType(typeof(HttpResponseEnvelopeResult<ChallengeDTO>), StatusCodes.Status201Created)]
    [ProducesError(StatusCodes.Status400BadRequest)]
    [AllowAnonymous]
    public async Task<IActionResult> Create()
    {
        var response = await _mediator.Send(new CreateChallengeCommand());
        return CreatedAtAction(nameof(GetById), new {id = response.Id}, response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(HttpResponseEnvelopeResult<ChallengeDTO>), StatusCodes.Status200OK)]
    [ProducesError(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] ChallengeId id)
    {
        var @event = await _mediator.Send(new GetChallengeByIdQuery {Id = id});
        return Ok(@event);
    }
}
