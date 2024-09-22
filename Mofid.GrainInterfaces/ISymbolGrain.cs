using Mofid.Dto;

namespace Mofid.GrainInterfaces;

public interface ISymbolGrain : IGrainWithStringKey
{
    Task<GetSymbolDto> GetState();
}