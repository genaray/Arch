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
        var spans = new StringBuilder().SpanFields(amount);
        var assignSpans = new StringBuilder().AssignSpanFields(amount);
        var insertParams = new StringBuilder().InsertSpanRefs(amount);

        var template = $$"""
            [SkipLocalsInit]
            public ref struct QueryReferenceEnumerator<{{generics}}>
            {
                private QueryChunkEnumerator _chunkEnumerator;
                private int _index;
                {{spans}}

                [SkipLocalsInit]
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public QueryReferenceEnumerator(Query query, Span<Archetype> archetypes)
                {
                    _index = -1;
                    _chunkEnumerator = new QueryChunkEnumerator(query, archetypes);
                }

                [SkipLocalsInit]
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public bool MoveNext()
                {
                    unchecked
                    {
                        // We reached the end, next archetype
                        if (--_index >= 0)
                        {
                            return true;
                        }

                        if (!_chunkEnumerator.MoveNext())
                        {
                            return false;
                        }

                        _index = _chunkEnumerator.Current.Size - 1;
                        {{assignSpans}}
                        return true;
                    }
                }

                /// <summary>
                ///     Resets this instance.
                /// </summary>
                [SkipLocalsInit]
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
                    [SkipLocalsInit]
                    [MethodImpl(MethodImplOptions.AggressiveInlining)]
                    get => new References<{{generics}}>({{insertParams}});
                }
            }

            [SkipLocalsInit]
            public readonly ref struct QueryReferenceIterator<{{generics}}>
            {
                private readonly Query _query;
                private readonly Span<Archetype> _archetypes;

                [SkipLocalsInit]
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public QueryReferenceIterator(Query query, Span<Archetype> archetypes)
                {
                    _query = query;
                    _archetypes = archetypes;
                }

                [SkipLocalsInit]
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
        var spans = new StringBuilder().SpanFields(amount);
        var assignSpans = new StringBuilder().AssignSpanFields(amount);
        var insertParams = new StringBuilder().InsertSpanRefs(amount);

        var template = $$"""
            public ref struct QueryEntityReferenceEnumerator<{{generics}}>
            {
                private QueryChunkEnumerator _chunkEnumerator;
                private int _index;
                private Span<Entity> _entities;
                {{spans}}

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
                        _entities = _chunkEnumerator.Current.Entities.AsSpan();
                        {{assignSpans}}
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
                    get => new EntityReferences<{{generics}}>(in _entities[_index], {{insertParams}});
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

    public static StringBuilder AppendQueryGetReferenceIterators(this StringBuilder sb, int amount)
    {
        for (int index = 1; index < amount; index++)
            sb.AppendQueryGetReferenceIterator(index);
        return sb;
    }

    public static StringBuilder AppendQueryGetReferenceIterator(this StringBuilder sb, int amount)
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

    public static StringBuilder AppendQueryGetEntityReferenceIterators(this StringBuilder sb, int amount)
    {
        for (int index = 1; index < amount; index++)
            sb.AppendQueryGetEntityReferenceIterator(index);
        return sb;
    }

    public static StringBuilder AppendQueryGetEntityReferenceIterator(this StringBuilder sb, int amount)
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
