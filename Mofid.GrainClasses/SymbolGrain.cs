using Mofid.Dto;
using Mofid.GrainInterfaces;

namespace Mofid.GrainClasses;

public class SymbolGrain : Grain, ISymbolGrain
{
    public Task<GetSymbolDto> GetState()
    {
        var symbol = new GetSymbolDto
        {
            SymbolName = "فولاد",
            IsActive = true,
            ExchangeType = "TSE"
        };
        
        return Task.FromResult(symbol);
    }
}