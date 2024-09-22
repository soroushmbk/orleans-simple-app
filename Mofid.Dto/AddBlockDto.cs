using Orleans;

namespace Mofid.Dto;

[GenerateSerializer]
public record AddBlockDto(decimal Value)
{
    [Id(0)]
    public decimal Value { get; set; } = Value;
}