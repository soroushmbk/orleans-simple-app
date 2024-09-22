using Microsoft.AspNetCore.Mvc;
using Mofid.Dto;
using Mofid.GrainInterfaces;

namespace Mofid.Gateway;
[Route("Orders")]
public class OrderController : Controller
{
    private readonly IClusterClient _client;

    public OrderController(IClusterClient client)
    {
        _client = client;
    }

    [HttpPost("AddOrder")]
    public async Task<string> AddOrder()
    {
        var orderGrain = _client.GetGrain<IOrderGrain>(new Random().NextInt64(1, 1000).ToString());
        var addOrderResult = await orderGrain.Add(new AddOrderDto(new Random().NextInt64(1, 100).ToString(), new Random().NextInt64(1, 20).ToString(), new Random().NextInt64(10000), (int)new Random().NextInt64(10000)));
        return addOrderResult;
    }
    
    [HttpGet("GetOrderState")]
    public async Task<string> GetOrderState(string orderId)
    {
        var orderGrain = _client.GetGrain<IOrderGrain>(orderId);
        var addOrderResult = await orderGrain.GetOrderState();
        return addOrderResult;
    }
}