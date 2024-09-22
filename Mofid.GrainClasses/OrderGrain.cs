using Microsoft.CodeAnalysis.Host;
using Mofid.Dto;
using Mofid.GrainClasses.States;
using Mofid.GrainInterfaces;
using Orleans.Streams;

namespace Mofid.GrainClasses;

public class OrderGrain : Grain,IOrderGrain
{
    private readonly IPersistentState<OrderState> _orderState;
    //IAsyncStream<AddOrderDto> _stream;

    public OrderGrain([PersistentState("state", "RedisStorage")]IPersistentState<OrderState> orderState)
    {
        _orderState = orderState;
        
        // var streamId = StreamId.Create("orders-sent-to-exchange-sender-grain", "Mofid");
        // _stream = this.GetStreamProvider("KafkaProvider").GetStream<AddOrderDto>(streamId);
    }

    public async Task<string> Add(AddOrderDto orderDto)
    {
        var symbolGrain = GrainFactory.GetGrain<ISymbolGrain>(orderDto.SymbolIsin);
        var customerGrain = GrainFactory.GetGrain<ICustomerGrain>(orderDto.CustomerIsin);

        var symbol = await symbolGrain.GetState();

        if (!symbol.IsActive)
        {
            _orderState.State = new OrderState("Symbol is closed");
            await _orderState.WriteStateAsync();
            return await Task.FromResult("Symbol is closed");
        }

        var isBlockedOk = await customerGrain.Block(new AddBlockDto(10));

        if (!isBlockedOk)
        {
            _orderState.State = new OrderState("Customer has not enough money");
            await _orderState.WriteStateAsync();
            return await Task.FromResult<string>("Customer has not enough money");
        }
        
        // await _stream.OnNextAsync(orderDto);
        var sendToAppropriateExchange = await SendToAppropriateExchange(orderDto, symbol);
        if (sendToAppropriateExchange != "true")
        {
            await customerGrain.Block(new AddBlockDto(-1 *10));
            _orderState.State = new OrderState("Cant send to Exchange");
            await _orderState.WriteStateAsync();
            return await Task.FromResult<string>("Cant send to Exchange");
        }
        _orderState.State = new OrderState("Sent to hub");
        await _orderState.WriteStateAsync();
        
        return await Task.FromResult<string>(sendToAppropriateExchange);
        
    }

    public async Task<string> GetOrderState()
    {
        return _orderState.State.State;
    }

    private Task<string> SendToAppropriateExchange(AddOrderDto orderDto, GetSymbolDto symbolDto)
    {
        return symbolDto.ExchangeType switch
        {
            "TSE" => Task.FromResult<string>("true"),//SendToTse(orderDto),
            _ => Task.FromResult<string>("true")
        };
    }

    private async Task<string> SendToTse(AddOrderDto orderDto)
    {
        var tseSenderGrain = GrainFactory.GetGrain<ITseSender>("TseMofid");
        return await tseSenderGrain.Send(orderDto);
    }
}