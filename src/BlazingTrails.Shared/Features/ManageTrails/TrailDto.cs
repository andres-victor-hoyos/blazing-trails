namespace BlazingTrails.Shared.Features.ManageTrails;

using FluentValidation;

public class TrailDto
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;
    public int Length { get; set; }
    public string Description { get; set; } = default!;
    public string Location { get; set; } = default!;
    public int TimeInMinutes { get; set; }
    public string? Image {get;set;}
    public ImageAction ImageAction {get; set;}
    public List<RouteInstruction> Route { get; set; } = new List<RouteInstruction>();

    public void AssignNewRoute(IEnumerable<RouteInstruction> route)
    {
        this.Route.Clear();
        this.Route.AddRange(route);
    }

    public class RouteInstruction
    {
        public int Stage { get; set; }
        public string Description { get; set; } = default!;
    }
}

public class TrailValidator : AbstractValidator<TrailDto>
{
    public TrailValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Please enter a name");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Please enter description.");
        RuleFor(x => x.Location).NotEmpty().WithMessage("Please enter a Location");
        RuleFor(x => x.Length).GreaterThan(0).WithMessage("Please enter a lenght");
        RuleFor(x => x.Route).NotEmpty().WithMessage("Please add a route instruction");
        RuleFor(x => x.TimeInMinutes).GreaterThan(0).WithMessage("Please enter a time");
        RuleForEach(x => x.Route).SetValidator(new RouteInstructionValidator());
    }
}

public class RouteInstructionValidator : AbstractValidator<TrailDto.RouteInstruction>
{
    public RouteInstructionValidator()
    {
        RuleFor(x => x.Stage).NotEmpty().WithMessage("Please enter a stage");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Please enter a description");
    }
}

public enum ImageAction
{
    None,
    Add,
    Remove
}