using System.Net.Http.Json;
using BlazingTrails.Shared.Features.ManageTrails.Shared;
using MediatR;

public class GetTrailHandler : IRequestHandler<GetTrailRequest, GetTrailRequest.Response>
{
    private readonly HttpClient _httpClient;

    public GetTrailHandler(HttpClient httpClient)
    {
        ArgumentNullException.ThrowIfNull(httpClient);
        _httpClient = httpClient;
    }
    public async Task<GetTrailRequest.Response> Handle(GetTrailRequest request, CancellationToken cancellationToken)
    {
        return await _httpClient.GetFromJsonAsync<GetTrailRequest.Response>(
            GetTrailRequest.RouteTemplate.Replace("{trailId}", request.TrailId.ToString())) ?? default!;

    }
}