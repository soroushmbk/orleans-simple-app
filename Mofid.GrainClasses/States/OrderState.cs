namespace Mofid.GrainClasses.States;

public record OrderState
{
    public OrderState(string state)
    {
        State = state;
    }

    public string State { get; set; }
}