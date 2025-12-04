using System.Security.Cryptography.X509Certificates;

namespace BlazingTrails.Client.Features.Home;

public class Trail
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public string? Description { get; set; }
    public string? Image { get; set; }
    public string? Location { get; set; }
    public int TimeInMinutes { get; set; }
    public string TimeFormatted => $"{TimeInMinutes / 60}h {TimeInMinutes % 60}m";

    public int Length { set; get; }
    public IEnumerable<RouteInstruction> Route { get; set; } = Array.Empty<RouteInstruction>();

    private bool Contains(Func<string> property, string filter)
    {
        return property().ToUpper().Contains(filter.ToUpper());
    }
    public bool Contains(string filter)
    {
        return Contains(() => this.Location!, filter) || Contains(() => this.Name!, filter);
    }

    public bool LengthLessThan(int maxlength)
    {
        return Length <= maxlength;
    }


    public bool TimeLessThan(int? maxTimeHours)
    {
        return TimeInMinutes <= (maxTimeHours*60);
    }
}

public class RouteInstruction
{
    public int Stage { get; set; }
    public string Description { get; set; } = "";

}