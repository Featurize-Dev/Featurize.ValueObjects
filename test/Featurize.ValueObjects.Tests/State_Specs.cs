using FluentAssertions;

namespace Featurize.ValueObjects.Tests;
internal class State_Specs
{
    [Test]
    public void CanTransitionTo()
    {
        var start = State.Create(0)
            .WithName("Start")
            .WithAllowedTransitions(1);

        var inprocess = State.Create(1)
            .WithName("InProcess")
            .WithAllowedTransitions(2);

        var end = State.Create(2)
            .WithName("End")
            .WithAllowedTransitions(0);
        
        start.CanTransitionTo(inprocess).Should().BeTrue();
        start.CanTransitionTo(end).Should().BeFalse();

        inprocess.CanTransitionTo(end).Should().BeTrue();
        inprocess.CanTransitionTo(start).Should().BeFalse();

        end.CanTransitionTo(start).Should().BeTrue();
        end.CanTransitionTo(inprocess).Should().BeFalse();
    }

    [Test]
    public void Transition()
    {
        var current = StateMachine.Start;

        current = current.TransitionTo(StateMachine.InProcess);

        current.Should().Be(StateMachine.InProcess);

        current = current.TransitionTo(StateMachine.End);

        current.Should().Be(StateMachine.End);
    }
}

public static class StateMachine
{
    public static State Start = State.Create(0)
        .WithName(nameof(Start))
        .WithAllowedTransitions(1);

    public static State InProcess = State.Create(1)
        .WithName(nameof(InProcess))
        .WithAllowedTransitions(2);

    public static State End = State.Create(2)
        .WithName(nameof(End))
        .WithAllowedTransitions(0);
}
