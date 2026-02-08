using System.Net.Http.Json;
using BlazingTrails.Shared.Features.Home.Shared;
using MediatR;

public class GetTrailsHandler : IRequestHandler<GetTrailsRequest, GetTrailsRequest.Response>
{
    private readonly HttpClient _httpClient;

    public GetTrailsHandler(HttpClient httpClient)
    {
        ArgumentNullException.ThrowIfNull(httpClient);
        _httpClient = httpClient;
    }
    public async Task<GetTrailsRequest.Response> Handle(GetTrailsRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _httpClient
                .GetFromJsonAsync<GetTrailsRequest.Response>(GetTrailsRequest.RouteTemplate);

            return response ?? default!;
        }        
        catch (HttpRequestException)
        {
            return default!;
        }        
    }
}