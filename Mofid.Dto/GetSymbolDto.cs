using Orleans;

namespace Mofid.Dto;

[GenerateSerializer]
public record GetSymbolDto
{
    [Id(0)]
    public required string SymbolName { get; set; }
    [Id(1)]
    public bool IsActive { get; set; }
    [Id(2)]
    public required string ExchangeType { get; set; }
}