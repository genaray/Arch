namespace Arch.SourceGen;

public static class EnumeratorExtensions
{

    public static StringBuilder AppendReferenceEnumerators(this StringBuilder sb, int amount)
    {
        for (int index = 1; index < amount; index++)
            sb.AppendReferenceEnumerator(index);
        return sb;
    }

    public static StringBuilder AppendReferenceEnumerator(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var template = $$"""
            public ref struct QueryReferenceEnumerator<{{generics}}>
            {
                private QueryChunkEnumerator _chunkEnumerator;
                private int _index;

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public QueryReferenceEnumerator(Query query, Span<Archetype> archetypes)
                {
                    _index = -1;
                    _chunkEnumerator = new QueryChunkEnumerator(query, archetypes);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public bool MoveNext()
                {
                    unchecked
                    {
                        --_index;

                        // We reached the end, next archetype
                        if (_index >= 0)
                        {
                            return true;
                        }

                        if (!_chunkEnumerator.MoveNext())
                        {
                            return false;
                        }

                        _index = _chunkEnumerator.Current.Size - 1;
                        return true;
                    }
                }

                /// <summary>
                ///     Resets this instance.
                /// </summary>
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public void Reset()
                {
                    _index = -1;
                    _chunkEnumerator.Reset();
                }

                /// <summary>
                ///     Returns a reference to the current <see cref="Entity"/>.
                /// </summary>
                public References<{{generics}}> Current
                {
                    [MethodImpl(MethodImplOptions.AggressiveInlining)]
                    get => _chunkEnumerator.Current.Get<{{generics}}>(_index);
                }
            }

            public readonly ref struct QueryReferenceIterator<{{generics}}>
            {
                private readonly Query _query;
                private readonly Span<Archetype> _archetypes;

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public QueryReferenceIterator(Query query, Span<Archetype> archetypes)
                {
                    _query = query;
                    _archetypes = archetypes;
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public QueryReferenceEnumerator<{{generics}}> GetEnumerator()
                {
                    return new QueryReferenceEnumerator<{{generics}}>(_query, _archetypes);
                }
            }
        """;

        sb.AppendLine(template);
        return sb;
    }

     public static StringBuilder AppendEntityReferenceEnumerators(this StringBuilder sb, int amount)
    {
        for (int index = 1; index < amount; index++)
            sb.AppendEntityReferenceEnumerator(index);
        return sb;
    }

    public static StringBuilder AppendEntityReferenceEnumerator(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var template = $$"""
            public ref struct QueryEntityReferenceEnumerator<{{generics}}>
            {
                private QueryChunkEnumerator _chunkEnumerator;
                private int _index;

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public QueryEntityReferenceEnumerator(Query query, Span<Archetype> archetypes)
                {
                    _index = -1;
                    _chunkEnumerator = new QueryChunkEnumerator(query, archetypes);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public bool MoveNext()
                {
                    unchecked
                    {
                        --_index;

                        // We reached the end, next archetype
                        if (_index >= 0)
                        {
                            return true;
                        }

                        if (!_chunkEnumerator.MoveNext())
                        {
                            return false;
                        }

                        _index = _chunkEnumerator.Current.Size - 1;
                        return true;
                    }
                }

                /// <summary>
                ///     Resets this instance.
                /// </summary>
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public void Reset()
                {
                    _index = -1;
                    _chunkEnumerator.Reset();
                }

                /// <summary>
                ///     Returns a reference to the current <see cref="Entity"/>.
                /// </summary>
                public EntityReferences<{{generics}}> Current
                {
                    [MethodImpl(MethodImplOptions.AggressiveInlining)]
                    get => _chunkEnumerator.Current.GetRow<{{generics}}>(_index);
                }
            }

            public readonly ref struct QueryEntityReferenceIterator<{{generics}}>
            {
                private readonly Query _query;
                private readonly Span<Archetype> _archetypes;

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public QueryEntityReferenceIterator(Query query, Span<Archetype> archetypes)
                {
                    _query = query;
                    _archetypes = archetypes;
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public QueryEntityReferenceEnumerator<{{generics}}> GetEnumerator()
                {
                    return new QueryEntityReferenceEnumerator<{{generics}}>(_query, _archetypes);
                }
            }
        """;

        sb.AppendLine(template);
        return sb;
    }

    public static StringBuilder AppendGetReferenceIterators(this StringBuilder sb, int amount)
    {
        for (int index = 1; index < amount; index++)
            sb.AppendGetReferenceIterator(index);
        return sb;
    }

    public static StringBuilder AppendGetReferenceIterator(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var template = $$"""
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public QueryReferenceIterator<{{generics}}> GetIterator<{{generics}}>()
            {
                return new QueryReferenceIterator<{{generics}}>(this, _archetypes.Span);
            }
        """;

        sb.AppendLine(template);
        return sb;
    }

    public static StringBuilder AppendGetEntityReferenceIterators(this StringBuilder sb, int amount)
    {
        for (int index = 1; index < amount; index++)
            sb.AppendGetEntityReferenceIterator(index);
        return sb;
    }

    public static StringBuilder AppendGetEntityReferenceIterator(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var template = $$"""
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public QueryEntityReferenceIterator<{{generics}}> GetEntityIterator<{{generics}}>()
            {
                return new QueryEntityReferenceIterator<{{generics}}>(this, _archetypes.Span);
            }
        """;

        sb.AppendLine(template);
        return sb;
    }
}
