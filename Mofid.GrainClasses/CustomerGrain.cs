using Mofid.Dto;
using Mofid.GrainInterfaces;

namespace Mofid.GrainClasses;

public class CustomerGrain : Grain, ICustomerGrain
{
    public Task<bool> Block(AddBlockDto addBlockDto)
    {
        //delay returning true for 10 miliseconds
        return Task.Delay(10).ContinueWith(_ => true);
    }
}