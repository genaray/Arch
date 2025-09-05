#if DIRTY_FLAGS

using System;
using System.Diagnostics.Contracts;
using Arch.Core;
using Arch.Core.Utils;

namespace Arch.Core;
public partial struct QueryDescription
{

    [UnscopedRef]
    public ref QueryDescription WithDirty<T0, T1>()
    {
        Dirty = Component<T0, T1>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithDirty<T0, T1, T2>()
    {
        Dirty = Component<T0, T1, T2>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithDirty<T0, T1, T2, T3>()
    {
        Dirty = Component<T0, T1, T2, T3>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithDirty<T0, T1, T2, T3, T4>()
    {
        Dirty = Component<T0, T1, T2, T3, T4>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithDirty<T0, T1, T2, T3, T4, T5>()
    {
        Dirty = Component<T0, T1, T2, T3, T4, T5>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithDirty<T0, T1, T2, T3, T4, T5, T6>()
    {
        Dirty = Component<T0, T1, T2, T3, T4, T5, T6>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithDirty<T0, T1, T2, T3, T4, T5, T6, T7>()
    {
        Dirty = Component<T0, T1, T2, T3, T4, T5, T6, T7>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithDirty<T0, T1, T2, T3, T4, T5, T6, T7, T8>()
    {
        Dirty = Component<T0, T1, T2, T3, T4, T5, T6, T7, T8>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithDirty<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>()
    {
        Dirty = Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithDirty<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
    {
        Dirty = Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithDirty<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>()
    {
        Dirty = Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithDirty<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
    {
        Dirty = Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithDirty<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
    {
        Dirty = Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithDirty<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
    {
        Dirty = Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithDirty<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
    {
        Dirty = Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithDirty<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
    {
        Dirty = Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithDirty<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>()
    {
        Dirty = Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithDirty<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>()
    {
        Dirty = Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithDirty<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>()
    {
        Dirty = Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithDirty<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>()
    {
        Dirty = Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithDirty<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>()
    {
        Dirty = Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithDirty<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>()
    {
        Dirty = Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithDirty<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>()
    {
        Dirty = Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithDirty<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>()
    {
        Dirty = Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>.Signature;
        Build();
        return ref this;
    }
}

#endif
