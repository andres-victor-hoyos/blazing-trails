using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazingTrails.Api.Persistence.Entities;
public class Trail
{
    public int Id{get;set;}
    public string Name { get; set; }
    public string Description { get; set; }
    public string? Image { get; set; }
    public string Location { get; set; }
    public int TimeInMinutes {get;set;}
    public int Length{get;set;}
    public ICollection<RouteInstruction> Route{get;set;}
}

public class TrailConfig : IEntityTypeConfiguration<Trail>
{    
    public void Configure(EntityTypeBuilder<Trail> builder)
    {
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x=> x.Location).IsRequired();
        builder.Property(x => x.TimeInMinutes).IsRequired();
        builder.Property(x => x.Length).IsRequired();
    }
}