using Mofid.Dto;

namespace Mofid.GrainInterfaces;

public interface ICustomerGrain : IGrainWithStringKey
{
    Task<bool> Block(AddBlockDto addBlockDto);
}