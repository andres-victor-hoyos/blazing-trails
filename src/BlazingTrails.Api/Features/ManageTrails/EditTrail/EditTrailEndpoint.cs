using Ardalis.ApiEndpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using BlazingTrails.Api.Persistence.Entities;
using BlazingTrails.Shared.Features.ManageTrails;
using BlazingTrails.Features.ManageTrails.EditTrail;

public class EditTrailEndpoint : EndpointBaseAsync.WithRequest<EditTrailRequest>.WithActionResult<EditTrailRequest.Response>
{
    private readonly BlazingTrailsContext _context;

    public EditTrailEndpoint(BlazingTrailsContext context)
    {
        _context = context;
    }

    [HttpPut(EditTrailRequest.RouteTemplate)]
    public override async Task<ActionResult<EditTrailRequest.Response>> HandleAsync(EditTrailRequest request, CancellationToken cancellationToken = default)
    {
        var trail = await _context.Trails
        .Include(x => x.Route)
        .SingleOrDefaultAsync<Trail>(x => x.Id == request.Trail.Id);
        if (trail is null)
            return BadRequest("Trail not be found.");

        trail.Name = request.Trail.Name;
        trail.Description = request.Trail.Description;
        trail.Location = request.Trail.Location;
        trail.Image = request.Trail.Image;
        trail.TimeInMinutes = request.Trail.TimeInMinutes;
        trail.Length = request.Trail.Length;
        trail.Route.Clear();
        trail.Route = request.Trail.Route.Select(ri => new RouteInstruction()
        {
            Stage = ri.Stage,
            Description = ri.Description,
            Trail = trail
        }).ToList();
        if (request.Trail.ImageAction == ImageAction.Remove)
        {
            //System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "images", trail.Image!));
            trail.Image = null;
        }
        await _context.SaveChangesAsync();
        return Ok(new EditTrailRequest.Response(true));
    }
}