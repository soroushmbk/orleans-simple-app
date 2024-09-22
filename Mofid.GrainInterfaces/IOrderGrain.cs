using Mofid.Dto;

namespace Mofid.GrainInterfaces;

public interface IOrderGrain : IGrainWithStringKey
{
    Task<string> Add(AddOrderDto orderDto);
    Task<string> GetOrderState();
}