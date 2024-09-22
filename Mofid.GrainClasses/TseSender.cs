using Microsoft.Extensions.Logging;
using Mofid.Dto;
using Mofid.GrainInterfaces;
using Orleans.Streams;
using Orleans.Streams.Core;

namespace Mofid.GrainClasses;

//[ImplicitStreamSubscription("orders-sent-to-exchange-sender-grain")]
public class TseSender : Grain, ITseSender//, IStreamSubscriptionObserver
{
    // private readonly ILogger<ITseSender> _logger;
    // private readonly LoggerObserver _observer;
    //
    // public TseSender(ILogger<ITseSender> logger)
    // {
    //     _logger = logger;
    //     _observer = new LoggerObserver(_logger);
    // }
    //
    // // Called when a subscription is added
    // public async Task OnSubscribed(IStreamSubscriptionHandleFactory handleFactory)
    // {
    //     // Plug our LoggerObserver to the stream
    //     var handle = handleFactory.Create<AddOrderDto>();
    //     await handle.ResumeAsync(_observer);
    // }
    //
    //
    // public override Task OnActivateAsync(CancellationToken token)
    // {
    //     _logger.LogInformation("OnActivateAsync");
    //     return Task.CompletedTask;
    // }
    //
    // public Task<string> Send(AddOrderDto orderDto)
    // {
    //     return Task.Delay(1).ContinueWith(_ => "Sent successfully");
    // }
    //
    //
    // private class LoggerObserver// : IAsyncObserver<AddOrderDto>
    // {
    //     private readonly ILogger<ITseSender> _logger;
    //
    //     public LoggerObserver(ILogger<ITseSender> logger)
    //     {
    //         _logger = logger;
    //     }
    //
    //     public Task OnNextAsync(AddOrderDto item, StreamSequenceToken? token = null)
    //     {
    //         var msg = $"{item.CustomerIsin} starting {item.SymbolIsin}\"";
    //         Console.WriteLine(msg);
    //         return Task.FromResult("");
    //         //return Send(item);
    //     }
    //
    //     public Task OnCompletedAsync()
    //     {
    //         _logger.LogInformation("OnCompletedAsync");
    //         return Task.CompletedTask;
    //     }
    //
    //     public Task OnErrorAsync(Exception ex)
    //     {
    //         _logger.LogInformation("OnErrorAsync: {Exception}", ex);
    //         return Task.CompletedTask;
    //     }
    //     
    // }

    public Task<string> Send(AddOrderDto orderDto)
    {
        return Task.FromResult("Sent!");
    }
}