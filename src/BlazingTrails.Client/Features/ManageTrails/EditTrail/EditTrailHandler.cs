using System.Net.Http.Json;
using BlazingTrails.Features.ManageTrails.EditTrail;
using MediatR;

class EditTrailHandler : IRequestHandler<EditTrailRequest, EditTrailRequest.Response>
{
    private readonly HttpClient _httpClient;

    public EditTrailHandler(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<EditTrailRequest.Response> Handle(EditTrailRequest request, CancellationToken cancellationToken)
    {
        var Response = await _httpClient.PutAsJsonAsync(EditTrailRequest.RouteTemplate, request, cancellationToken);
        return new EditTrailRequest.Response(Response.IsSuccessStatusCode);
    }
}