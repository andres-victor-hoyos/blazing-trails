using System.Dynamic;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazingTrails.Client.Features.ManageTrails;

public record UploadTrailImageRequest(int TrailId, IBrowserFile File):IRequest<UploadTrailImageRequest.Response>
{
    public const string RouteTemplate = "/api/trails/{TrailId}/images";
    public string GetRoute() => RouteTemplate.Replace("{TrailId}", TrailId.ToString());
    public record Response(string ImageName);

    public MultipartContent FileContent()
    {
        var fileContent = File.OpenReadStream(File.Size);
        var content = new MultipartFormDataContent();
        content.Add(new StreamContent(fileContent), "image", File.Name);
        return  content;        
    }
}