using Ardalis.ApiEndpoints;
using BlazingTrails.Api.Persistence.Entities;
using Microsoft.AspNetCore.Mvc;

public class AddTrailEndPoint : EndpointBaseAsync.WithRequest<AddTrailRequest>.WithResult<int>
{
    private readonly BlazingTrailsContext _database;
    public AddTrailEndPoint(BlazingTrailsContext database)
    {
        _database = database;
    }

    [HttpPost(AddTrailRequest.RouteTemplate)]
    public override async Task<int> HandleAsync(AddTrailRequest request, CancellationToken cancellationToken = default)
    {
        var trail = new Trail
        {
            Name = request.Trail.Name,
            Description = request.Trail.Description,
            Location = request.Trail.Location,
            TimeInMinutes = request.Trail.TimeInMinutes,
            Length = request.Trail.Length,
            Owner = request.Trail.Owner   
        };

        trail.Route = request.Trail.Route.Select(x => new RouteInstruction
        {
            Stage = x.Stage,
            Description = x.Description,
            Trail = trail
        }).ToList();

        await _database.Trails.AddAsync(trail, cancellationToken);

        await _database.SaveChangesAsync(cancellationToken);
        return trail.Id;
    }
}
