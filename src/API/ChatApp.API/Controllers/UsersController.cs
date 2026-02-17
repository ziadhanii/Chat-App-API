namespace ChatApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IMediator mediator) : ControllerBase
{
    [HttpPost("")]
    public async Task<IActionResult> CreateUser([FromForm] AddUserCommandRequest command)
    {
        var result = await mediator.Send(command);

        return result.IsFailure
            ? result.ToProblem()
            : CreatedAtAction(
                nameof(GetUser),
                new { id = result.Value!.Id },
                result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(string id)
    {
        // TODO: Implement GetUserQuery
        return Ok(new { message = "GetUser endpoint - Not implemented yet", userId = id });
    }
}