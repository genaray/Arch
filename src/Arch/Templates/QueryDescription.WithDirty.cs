#if CHANGED_FLAGS

using System;
using System.Diagnostics.Contracts;
using Arch.Core;
using Arch.Core.Utils;

namespace Arch.Core;
public partial struct QueryDescription
{

    [UnscopedRef]
    public ref QueryDescription WithChanged<T0, T1>()
    {
        Changed = Component<T0, T1>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithChanged<T0, T1, T2>()
    {
        Changed = Component<T0, T1, T2>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithChanged<T0, T1, T2, T3>()
    {
        Changed = Component<T0, T1, T2, T3>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithChanged<T0, T1, T2, T3, T4>()
    {
        Changed = Component<T0, T1, T2, T3, T4>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithChanged<T0, T1, T2, T3, T4, T5>()
    {
        Changed = Component<T0, T1, T2, T3, T4, T5>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithChanged<T0, T1, T2, T3, T4, T5, T6>()
    {
        Changed = Component<T0, T1, T2, T3, T4, T5, T6>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithChanged<T0, T1, T2, T3, T4, T5, T6, T7>()
    {
        Changed = Component<T0, T1, T2, T3, T4, T5, T6, T7>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithChanged<T0, T1, T2, T3, T4, T5, T6, T7, T8>()
    {
        Changed = Component<T0, T1, T2, T3, T4, T5, T6, T7, T8>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithChanged<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>()
    {
        Changed = Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithChanged<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
    {
        Changed = Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithChanged<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>()
    {
        Changed = Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithChanged<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
    {
        Changed = Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithChanged<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
    {
        Changed = Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithChanged<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
    {
        Changed = Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithChanged<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
    {
        Changed = Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithChanged<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
    {
        Changed = Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithChanged<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>()
    {
        Changed = Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithChanged<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>()
    {
        Changed = Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithChanged<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>()
    {
        Changed = Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithChanged<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>()
    {
        Changed = Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithChanged<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>()
    {
        Changed = Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithChanged<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>()
    {
        Changed = Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithChanged<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>()
    {
        Changed = Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>.Signature;
        Build();
        return ref this;
    }

    [UnscopedRef]
    public ref QueryDescription WithChanged<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>()
    {
        Changed = Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>.Signature;
        Build();
        return ref this;
    }
}

#endif
