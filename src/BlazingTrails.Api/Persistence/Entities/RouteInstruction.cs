using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazingTrails.Api.Persistence.Entities;

public class RouteInstruction
{
    public int Id { get; set; }
    public int TrailId { get; set; }
    public int Stage {get;set;}
    public string Description { get; set; }
    public Trail Trail{get;set;} = default;
}

public class RouteInstructionConfiguration : IEntityTypeConfiguration<RouteInstruction>
{
    public void Configure(EntityTypeBuilder<RouteInstruction> builder)
    {
        builder.Property(x=>x.Id).IsRequired();
        builder.Property(x=>x.Stage).IsRequired();
        builder.Property(x=>x.Description).IsRequired();
    }
}