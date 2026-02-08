using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class GetTrailsEndpoint : EndpointBaseAsync
.WithoutRequest
.WithActionResult<GetTrailsRequest.Response>
{
    private readonly BlazingTrailsContext _context;
    public GetTrailsEndpoint(BlazingTrailsContext context)
    {
        _context = context;
    }

    [HttpGet(GetTrailsRequest.RouteTemplate)]
    public override async Task<ActionResult<GetTrailsRequest.Response>> HandleAsync(CancellationToken cancellationToken = default)
    {
        var trails = await _context.Trails.Include(x => x.Route).ToListAsync();
        var Response = new GetTrailsRequest.Response(trails.Select(trail=> new GetTrailsRequest.Trail(
            trail.Id,
            trail.Name,
            trail.Image,
            trail.Location,
            trail.TimeInMinutes,
            trail.Length,
            trail.Description
        )));   

        return Ok(Response);     
    }
}