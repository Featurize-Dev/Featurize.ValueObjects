using Featurize.ValueObjects.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace Featurize.ValueObjects;

/// <summary>
/// Simple State Machine
/// </summary>
public record State
{
    private State(int Value) { Ordinal = Value; }

    private HashSet<int> _transitions = new();
    
    /// <summary>
    /// The name of this state.
    /// </summary>
    public string Name { get; private set; } = string.Empty;

    /// <summary>
    /// The Ordinal value
    /// </summary>
    public int Ordinal { get; private set; }

    /// <summary>
    /// Create a <see cref="State"/> for a specific ordinal.
    /// </summary>
    /// <param name="ordinal">The ordinal for this state.</param>
    /// <returns>Returns a state of the ordinal.</returns>
    public static State Create(int ordinal) => new(ordinal);

    /// <summary>
    /// Gives <see cref="State"/> a name.
    /// </summary>
    /// <param name="name">The name of this state.</param>
    /// <returns>Returns the <see cref="State"/> with the given name.</returns>
    public State WithName(string name) => this with { Name = name };

    /// <summary>
    /// Sets the allowed Transitions for this <see cref="State"/>.
    /// </summary>
    /// <param name="allowedTransitions">The ordinals of the allowed transitions.</param>
    /// <returns>Returns the <see cref="State"/> with the allowed transitions.</returns>
    public State WithAllowedTransitions(params int[] allowedTransitions)
    {
        var hset = new HashSet<int>();
        foreach (var transitions in allowedTransitions)
        {
            hset.Add(transitions);
        }
        return this with { _transitions = hset };
    }

    /// <summary>
    /// Indicates if a this state can transition to the new state.
    /// </summary>
    /// <param name="newState">The <see cref="State"/> where to transition.</param>
    /// <returns>Returns <c>true</c> if the Transition is allowed.</returns>
    public bool CanTransitionTo(State newState)
        => _transitions.Contains(newState.Ordinal);

    /// <summary>
    /// Transition to the new state.
    /// </summary>
    /// <param name="newState">The new state.</param>
    /// <returns>Returns the new state if the transition is allowed.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the transition is not allowed.</exception>
    public State TransitionTo(State newState)
    {
        if(!_transitions.Contains(newState.Ordinal))
           throw new InvalidOperationException($"Transition to: {newState.Ordinal} not allowed.");

        return newState;
    }
    
    /// <inheritdoc />
    public override string ToString()
    {
        return Name;
    }
}
