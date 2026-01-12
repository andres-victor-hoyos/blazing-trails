using System.Drawing;
using Ardalis.ApiEndpoints;
using BlazingTrails.Client.Features.ManageTrails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

public class UploadTrailImageEndPoint : EndpointBaseAsync.WithRequest<int>.WithResult<string>
{
    static string TRAIL_DOES_NOT_EXIST = "Trail does not exist.";
    static string NOT_IMAGE_FOUND = "Not Image found.";
    private readonly BlazingTrailsContext _database;
    public UploadTrailImageEndPoint(BlazingTrailsContext database)
    {
        _database = database;
    }

    [HttpPost(UploadTrailImageRequest.RouteTemplate)]
    public override async Task<string> HandleAsync([FromRoute] int trailId, CancellationToken cancellationToken = default)
    {
        var trail = await _database.Trails.SingleOrDefaultAsync(x=>x.Id == trailId,cancellationToken);
        if(trail is null)
            return TRAIL_DOES_NOT_EXIST;
        var file = Request.Form.Files[0];
        if(file.Length == 0)
            return NOT_IMAGE_FOUND;
        var filename = $"{Guid.NewGuid()}.jpg";
        var saveLocation = Path.Combine(Directory.GetCurrentDirectory(), "images",filename);
        var resizeOptions = new ResizeOptions
        {
            Mode = ResizeMode.Pad,
            Size = new SixLabors.ImageSharp.Size(640,426)
        };
        using var image = Image.Load(file.OpenReadStream());
        image.Mutate(x=>x.Resize(resizeOptions));
        await image.SaveAsJpegAsync(saveLocation, cancellationToken);
        trail.Image=filename;
        await _database.SaveChangesAsync(cancellationToken);
        return trail.Image;
    }
}