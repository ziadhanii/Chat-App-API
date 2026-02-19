namespace ChatApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IMediator mediator) : ControllerBase
{
    [HttpPost("")]
    public async Task<IActionResult> CreateUser(
        [FromForm] AddUserCommandRequest command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);

        return result.IsFailure
            ? result.ToProblem()
            : CreatedAtAction(
                nameof(GetUser),
                new { id = result.Value!.Id },
                result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new GetUserInfoQuery(id),
            cancellationToken);

        return result.IsFailure
            ? result.ToProblem()
            : Ok(result.Value);
    }

    [HttpPut("")]
    public async Task<IActionResult> UpdateUser(
        [FromRoute] int id,
        [FromForm] UpdateUserCommandRequest request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new UpdateUserCommand(id, request), cancellationToken);

        return result.IsFailure
            ? result.ToProblem()
            : Ok(result.Value);
    }
}