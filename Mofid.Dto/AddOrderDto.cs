using Orleans;

namespace Mofid.Dto;

[GenerateSerializer]
public record AddOrderDto(string CustomerIsin, string SymbolIsin, decimal Price, int Quantity)
{
    [Id(0)]
    public string CustomerIsin{ get; set; } = CustomerIsin;

    [Id(1)]
    public string SymbolIsin { get; set; } = SymbolIsin;

    [Id(2)]
    public decimal Price { get; set; } = Price;

    [Id(3)]
    public int Quantity { get; set; } = Quantity;
}