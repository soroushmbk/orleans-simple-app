using Mofid.Dto;

namespace Mofid.GrainInterfaces;

public interface ITseSender : IGrainWithStringKey
{
    Task<string> Send(AddOrderDto orderDto);
}