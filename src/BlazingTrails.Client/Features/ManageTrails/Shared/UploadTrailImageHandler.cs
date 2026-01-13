using MediatR;
public class UploadTrailImageHandler : IRequestHandler<UploadTrailImageRequest, UploadTrailImageRequest.Response>
{
    private readonly HttpClient _httpClient;

    public UploadTrailImageHandler(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UploadTrailImageRequest.Response> Handle(UploadTrailImageRequest request, CancellationToken cancellationToken)
    {
        using var content = request.FileContent();
        var response = await _httpClient
            .PostAsync(request.GetRoute(), content, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            var fileName = await response.Content.ReadAsStringAsync(cancellationToken);
            return new UploadTrailImageRequest.Response(fileName);
        }
        return new UploadTrailImageRequest.Response(String.Empty);
    }
}
