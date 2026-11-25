using Arch.Core;
using Arch.Core.Utils;
using Collections.Pooled;

namespace Arch.Buffer;

/// <summary>
///     The <see cref="StructuralChanges"/> class
///     works out which components one <see cref="Entity"/> gets and loses from all its recorded <see cref="Command"/>s.
///     Reused for every <see cref="Entity"/> to avoid allocations.
/// </summary>
internal sealed class StructuralChanges : IDisposable
{
    private readonly PooledList<ComponentType> _cancelled = new(4);

    /// <summary>
    ///     The components to add to the <see cref="Entity"/>, without duplicates.
    /// </summary>
    public PooledList<ComponentType> Added { get; } = new(16);

    /// <summary>
    ///     The components to remove from the <see cref="Entity"/>, without duplicates.
    /// </summary>
    public PooledList<ComponentType> Removed { get; } = new(16);

    /// <summary>
    ///     Whether the <see cref="Entity"/> has values to set, either for existing components or for the <see cref="Added"/> ones.
    /// </summary>
    public bool HasValues { get; private set; }

    /// <summary>
    ///     Whether the <see cref="Entity"/> has to be destroyed, which makes all its other commands pointless.
    /// </summary>
    public bool IsDestroyed { get; private set; }

    /// <summary>
    ///     Collects the changes of one <see cref="Entity"/>.
    ///     A component that is added and removed again leaves the <see cref="Entity"/> as it was, so it ends up in neither <see cref="Added"/> nor <see cref="Removed"/>.
    /// </summary>
    /// <param name="commands">All commands, linked through <see cref="Command.Next"/> by <see cref="EntityGroups"/>.</param>
    /// <param name="first">The index of the first command of the <see cref="Entity"/>.</param>
    public void Collect(ReadOnlySpan<Command> commands, int first)
    {
        Clear();

        for (var index = first; index >= 0; index = commands[index].Next)
        {
            ref readonly var command = ref commands[index];
            switch (command.Kind)
            {
                case CommandKind.Add:
                    AddUnique(Added, command.Type);
                    HasValues = true;
                    break;
                case CommandKind.Set:
                    HasValues = true;
                    break;
                case CommandKind.Remove:
                    AddUnique(Removed, command.Type);
                    break;
                case CommandKind.Destroy:
                    IsDestroyed = true;
                    return;
            }
        }

        if (Added.Count > 0 && Removed.Count > 0)
        {
            CancelAddedAndRemoved();
        }
    }

    /// <summary>
    ///     Checks whether the value of a component is pointless, since the component gets removed again.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDropped(in ComponentType type)
    {
        return (Removed.Count > 0 || _cancelled.Count > 0) && (Removed.Contains(type) || _cancelled.Contains(type));
    }

    /// <summary>
    ///     Disposes the lists.
    /// </summary>
    public void Dispose()
    {
        Added.Dispose();
        Removed.Dispose();
        _cancelled.Dispose();
    }

    private void Clear()
    {
        Added.Clear();
        Removed.Clear();
        _cancelled.Clear();
        HasValues = false;
        IsDestroyed = false;
    }

    private void CancelAddedAndRemoved()
    {
        for (var index = Removed.Count - 1; index >= 0; index--)
        {
            var type = Removed[index];
            if (Added.Remove(type))
            {
                Removed.RemoveAt(index);
                _cancelled.Add(type);
            }
        }
    }

    private static void AddUnique(PooledList<ComponentType> types, in ComponentType type)
    {
        if (!types.Contains(type))
        {
            types.Add(type);
        }
    }
}
