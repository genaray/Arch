

using Arch.Core;
using System;
using System.Threading;

namespace Arch.Core.Utils;


/// <inheritdoc cref="Component"/>
public static class Component<T0, T1>
{
    internal static readonly int Id;

    /// <summary>
    ///     An <see cref="Signature"/> for this given set of components.
    /// </summary>
    public static readonly Signature Signature;

    /// <summary>
    ///     The hash code for this given set of components.
    /// </summary>
    public static readonly int Hash;

    static Component()
    {
        Id = Interlocked.Increment(ref Component.Id);
        Signature = new Signature(new [] { Component<T0>.ComponentType,Component<T1>.ComponentType, });
        Hash = Signature.GetHashCode();
    }
}


/// <inheritdoc cref="Component"/>
public static class Component<T0, T1, T2>
{
    internal static readonly int Id;

    /// <summary>
    ///     An <see cref="Signature"/> for this given set of components.
    /// </summary>
    public static readonly Signature Signature;

    /// <summary>
    ///     The hash code for this given set of components.
    /// </summary>
    public static readonly int Hash;

    static Component()
    {
        Id = Interlocked.Increment(ref Component.Id);
        Signature = new Signature(new [] { Component<T0>.ComponentType,Component<T1>.ComponentType,Component<T2>.ComponentType, });
        Hash = Signature.GetHashCode();
    }
}


/// <inheritdoc cref="Component"/>
public static class Component<T0, T1, T2, T3>
{
    internal static readonly int Id;

    /// <summary>
    ///     An <see cref="Signature"/> for this given set of components.
    /// </summary>
    public static readonly Signature Signature;

    /// <summary>
    ///     The hash code for this given set of components.
    /// </summary>
    public static readonly int Hash;

    static Component()
    {
        Id = Interlocked.Increment(ref Component.Id);
        Signature = new Signature(new [] { Component<T0>.ComponentType,Component<T1>.ComponentType,Component<T2>.ComponentType,Component<T3>.ComponentType, });
        Hash = Signature.GetHashCode();
    }
}


/// <inheritdoc cref="Component"/>
public static class Component<T0, T1, T2, T3, T4>
{
    internal static readonly int Id;

    /// <summary>
    ///     An <see cref="Signature"/> for this given set of components.
    /// </summary>
    public static readonly Signature Signature;

    /// <summary>
    ///     The hash code for this given set of components.
    /// </summary>
    public static readonly int Hash;

    static Component()
    {
        Id = Interlocked.Increment(ref Component.Id);
        Signature = new Signature(new [] { Component<T0>.ComponentType,Component<T1>.ComponentType,Component<T2>.ComponentType,Component<T3>.ComponentType,Component<T4>.ComponentType, });
        Hash = Signature.GetHashCode();
    }
}


/// <inheritdoc cref="Component"/>
public static class Component<T0, T1, T2, T3, T4, T5>
{
    internal static readonly int Id;

    /// <summary>
    ///     An <see cref="Signature"/> for this given set of components.
    /// </summary>
    public static readonly Signature Signature;

    /// <summary>
    ///     The hash code for this given set of components.
    /// </summary>
    public static readonly int Hash;

    static Component()
    {
        Id = Interlocked.Increment(ref Component.Id);
        Signature = new Signature(new [] { Component<T0>.ComponentType,Component<T1>.ComponentType,Component<T2>.ComponentType,Component<T3>.ComponentType,Component<T4>.ComponentType,Component<T5>.ComponentType, });
        Hash = Signature.GetHashCode();
    }
}


/// <inheritdoc cref="Component"/>
public static class Component<T0, T1, T2, T3, T4, T5, T6>
{
    internal static readonly int Id;

    /// <summary>
    ///     An <see cref="Signature"/> for this given set of components.
    /// </summary>
    public static readonly Signature Signature;

    /// <summary>
    ///     The hash code for this given set of components.
    /// </summary>
    public static readonly int Hash;

    static Component()
    {
        Id = Interlocked.Increment(ref Component.Id);
        Signature = new Signature(new [] { Component<T0>.ComponentType,Component<T1>.ComponentType,Component<T2>.ComponentType,Component<T3>.ComponentType,Component<T4>.ComponentType,Component<T5>.ComponentType,Component<T6>.ComponentType, });
        Hash = Signature.GetHashCode();
    }
}


/// <inheritdoc cref="Component"/>
public static class Component<T0, T1, T2, T3, T4, T5, T6, T7>
{
    internal static readonly int Id;

    /// <summary>
    ///     An <see cref="Signature"/> for this given set of components.
    /// </summary>
    public static readonly Signature Signature;

    /// <summary>
    ///     The hash code for this given set of components.
    /// </summary>
    public static readonly int Hash;

    static Component()
    {
        Id = Interlocked.Increment(ref Component.Id);
        Signature = new Signature(new [] { Component<T0>.ComponentType,Component<T1>.ComponentType,Component<T2>.ComponentType,Component<T3>.ComponentType,Component<T4>.ComponentType,Component<T5>.ComponentType,Component<T6>.ComponentType,Component<T7>.ComponentType, });
        Hash = Signature.GetHashCode();
    }
}


/// <inheritdoc cref="Component"/>
public static class Component<T0, T1, T2, T3, T4, T5, T6, T7, T8>
{
    internal static readonly int Id;

    /// <summary>
    ///     An <see cref="Signature"/> for this given set of components.
    /// </summary>
    public static readonly Signature Signature;

    /// <summary>
    ///     The hash code for this given set of components.
    /// </summary>
    public static readonly int Hash;

    static Component()
    {
        Id = Interlocked.Increment(ref Component.Id);
        Signature = new Signature(new [] { Component<T0>.ComponentType,Component<T1>.ComponentType,Component<T2>.ComponentType,Component<T3>.ComponentType,Component<T4>.ComponentType,Component<T5>.ComponentType,Component<T6>.ComponentType,Component<T7>.ComponentType,Component<T8>.ComponentType, });
        Hash = Signature.GetHashCode();
    }
}


/// <inheritdoc cref="Component"/>
public static class Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>
{
    internal static readonly int Id;

    /// <summary>
    ///     An <see cref="Signature"/> for this given set of components.
    /// </summary>
    public static readonly Signature Signature;

    /// <summary>
    ///     The hash code for this given set of components.
    /// </summary>
    public static readonly int Hash;

    static Component()
    {
        Id = Interlocked.Increment(ref Component.Id);
        Signature = new Signature(new [] { Component<T0>.ComponentType,Component<T1>.ComponentType,Component<T2>.ComponentType,Component<T3>.ComponentType,Component<T4>.ComponentType,Component<T5>.ComponentType,Component<T6>.ComponentType,Component<T7>.ComponentType,Component<T8>.ComponentType,Component<T9>.ComponentType, });
        Hash = Signature.GetHashCode();
    }
}


/// <inheritdoc cref="Component"/>
public static class Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
{
    internal static readonly int Id;

    /// <summary>
    ///     An <see cref="Signature"/> for this given set of components.
    /// </summary>
    public static readonly Signature Signature;

    /// <summary>
    ///     The hash code for this given set of components.
    /// </summary>
    public static readonly int Hash;

    static Component()
    {
        Id = Interlocked.Increment(ref Component.Id);
        Signature = new Signature(new [] { Component<T0>.ComponentType,Component<T1>.ComponentType,Component<T2>.ComponentType,Component<T3>.ComponentType,Component<T4>.ComponentType,Component<T5>.ComponentType,Component<T6>.ComponentType,Component<T7>.ComponentType,Component<T8>.ComponentType,Component<T9>.ComponentType,Component<T10>.ComponentType, });
        Hash = Signature.GetHashCode();
    }
}


/// <inheritdoc cref="Component"/>
public static class Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
{
    internal static readonly int Id;

    /// <summary>
    ///     An <see cref="Signature"/> for this given set of components.
    /// </summary>
    public static readonly Signature Signature;

    /// <summary>
    ///     The hash code for this given set of components.
    /// </summary>
    public static readonly int Hash;

    static Component()
    {
        Id = Interlocked.Increment(ref Component.Id);
        Signature = new Signature(new [] { Component<T0>.ComponentType,Component<T1>.ComponentType,Component<T2>.ComponentType,Component<T3>.ComponentType,Component<T4>.ComponentType,Component<T5>.ComponentType,Component<T6>.ComponentType,Component<T7>.ComponentType,Component<T8>.ComponentType,Component<T9>.ComponentType,Component<T10>.ComponentType,Component<T11>.ComponentType, });
        Hash = Signature.GetHashCode();
    }
}


/// <inheritdoc cref="Component"/>
public static class Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>
{
    internal static readonly int Id;

    /// <summary>
    ///     An <see cref="Signature"/> for this given set of components.
    /// </summary>
    public static readonly Signature Signature;

    /// <summary>
    ///     The hash code for this given set of components.
    /// </summary>
    public static readonly int Hash;

    static Component()
    {
        Id = Interlocked.Increment(ref Component.Id);
        Signature = new Signature(new [] { Component<T0>.ComponentType,Component<T1>.ComponentType,Component<T2>.ComponentType,Component<T3>.ComponentType,Component<T4>.ComponentType,Component<T5>.ComponentType,Component<T6>.ComponentType,Component<T7>.ComponentType,Component<T8>.ComponentType,Component<T9>.ComponentType,Component<T10>.ComponentType,Component<T11>.ComponentType,Component<T12>.ComponentType, });
        Hash = Signature.GetHashCode();
    }
}


/// <inheritdoc cref="Component"/>
public static class Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
{
    internal static readonly int Id;

    /// <summary>
    ///     An <see cref="Signature"/> for this given set of components.
    /// </summary>
    public static readonly Signature Signature;

    /// <summary>
    ///     The hash code for this given set of components.
    /// </summary>
    public static readonly int Hash;

    static Component()
    {
        Id = Interlocked.Increment(ref Component.Id);
        Signature = new Signature(new [] { Component<T0>.ComponentType,Component<T1>.ComponentType,Component<T2>.ComponentType,Component<T3>.ComponentType,Component<T4>.ComponentType,Component<T5>.ComponentType,Component<T6>.ComponentType,Component<T7>.ComponentType,Component<T8>.ComponentType,Component<T9>.ComponentType,Component<T10>.ComponentType,Component<T11>.ComponentType,Component<T12>.ComponentType,Component<T13>.ComponentType, });
        Hash = Signature.GetHashCode();
    }
}


/// <inheritdoc cref="Component"/>
public static class Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
{
    internal static readonly int Id;

    /// <summary>
    ///     An <see cref="Signature"/> for this given set of components.
    /// </summary>
    public static readonly Signature Signature;

    /// <summary>
    ///     The hash code for this given set of components.
    /// </summary>
    public static readonly int Hash;

    static Component()
    {
        Id = Interlocked.Increment(ref Component.Id);
        Signature = new Signature(new [] { Component<T0>.ComponentType,Component<T1>.ComponentType,Component<T2>.ComponentType,Component<T3>.ComponentType,Component<T4>.ComponentType,Component<T5>.ComponentType,Component<T6>.ComponentType,Component<T7>.ComponentType,Component<T8>.ComponentType,Component<T9>.ComponentType,Component<T10>.ComponentType,Component<T11>.ComponentType,Component<T12>.ComponentType,Component<T13>.ComponentType,Component<T14>.ComponentType, });
        Hash = Signature.GetHashCode();
    }
}


/// <inheritdoc cref="Component"/>
public static class Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
{
    internal static readonly int Id;

    /// <summary>
    ///     An <see cref="Signature"/> for this given set of components.
    /// </summary>
    public static readonly Signature Signature;

    /// <summary>
    ///     The hash code for this given set of components.
    /// </summary>
    public static readonly int Hash;

    static Component()
    {
        Id = Interlocked.Increment(ref Component.Id);
        Signature = new Signature(new [] { Component<T0>.ComponentType,Component<T1>.ComponentType,Component<T2>.ComponentType,Component<T3>.ComponentType,Component<T4>.ComponentType,Component<T5>.ComponentType,Component<T6>.ComponentType,Component<T7>.ComponentType,Component<T8>.ComponentType,Component<T9>.ComponentType,Component<T10>.ComponentType,Component<T11>.ComponentType,Component<T12>.ComponentType,Component<T13>.ComponentType,Component<T14>.ComponentType,Component<T15>.ComponentType, });
        Hash = Signature.GetHashCode();
    }
}


/// <inheritdoc cref="Component"/>
public static class Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>
{
    internal static readonly int Id;

    /// <summary>
    ///     An <see cref="Signature"/> for this given set of components.
    /// </summary>
    public static readonly Signature Signature;

    /// <summary>
    ///     The hash code for this given set of components.
    /// </summary>
    public static readonly int Hash;

    static Component()
    {
        Id = Interlocked.Increment(ref Component.Id);
        Signature = new Signature(new [] { Component<T0>.ComponentType,Component<T1>.ComponentType,Component<T2>.ComponentType,Component<T3>.ComponentType,Component<T4>.ComponentType,Component<T5>.ComponentType,Component<T6>.ComponentType,Component<T7>.ComponentType,Component<T8>.ComponentType,Component<T9>.ComponentType,Component<T10>.ComponentType,Component<T11>.ComponentType,Component<T12>.ComponentType,Component<T13>.ComponentType,Component<T14>.ComponentType,Component<T15>.ComponentType,Component<T16>.ComponentType, });
        Hash = Signature.GetHashCode();
    }
}


/// <inheritdoc cref="Component"/>
public static class Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>
{
    internal static readonly int Id;

    /// <summary>
    ///     An <see cref="Signature"/> for this given set of components.
    /// </summary>
    public static readonly Signature Signature;

    /// <summary>
    ///     The hash code for this given set of components.
    /// </summary>
    public static readonly int Hash;

    static Component()
    {
        Id = Interlocked.Increment(ref Component.Id);
        Signature = new Signature(new [] { Component<T0>.ComponentType,Component<T1>.ComponentType,Component<T2>.ComponentType,Component<T3>.ComponentType,Component<T4>.ComponentType,Component<T5>.ComponentType,Component<T6>.ComponentType,Component<T7>.ComponentType,Component<T8>.ComponentType,Component<T9>.ComponentType,Component<T10>.ComponentType,Component<T11>.ComponentType,Component<T12>.ComponentType,Component<T13>.ComponentType,Component<T14>.ComponentType,Component<T15>.ComponentType,Component<T16>.ComponentType,Component<T17>.ComponentType, });
        Hash = Signature.GetHashCode();
    }
}


/// <inheritdoc cref="Component"/>
public static class Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>
{
    internal static readonly int Id;

    /// <summary>
    ///     An <see cref="Signature"/> for this given set of components.
    /// </summary>
    public static readonly Signature Signature;

    /// <summary>
    ///     The hash code for this given set of components.
    /// </summary>
    public static readonly int Hash;

    static Component()
    {
        Id = Interlocked.Increment(ref Component.Id);
        Signature = new Signature(new [] { Component<T0>.ComponentType,Component<T1>.ComponentType,Component<T2>.ComponentType,Component<T3>.ComponentType,Component<T4>.ComponentType,Component<T5>.ComponentType,Component<T6>.ComponentType,Component<T7>.ComponentType,Component<T8>.ComponentType,Component<T9>.ComponentType,Component<T10>.ComponentType,Component<T11>.ComponentType,Component<T12>.ComponentType,Component<T13>.ComponentType,Component<T14>.ComponentType,Component<T15>.ComponentType,Component<T16>.ComponentType,Component<T17>.ComponentType,Component<T18>.ComponentType, });
        Hash = Signature.GetHashCode();
    }
}


/// <inheritdoc cref="Component"/>
public static class Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>
{
    internal static readonly int Id;

    /// <summary>
    ///     An <see cref="Signature"/> for this given set of components.
    /// </summary>
    public static readonly Signature Signature;

    /// <summary>
    ///     The hash code for this given set of components.
    /// </summary>
    public static readonly int Hash;

    static Component()
    {
        Id = Interlocked.Increment(ref Component.Id);
        Signature = new Signature(new [] { Component<T0>.ComponentType,Component<T1>.ComponentType,Component<T2>.ComponentType,Component<T3>.ComponentType,Component<T4>.ComponentType,Component<T5>.ComponentType,Component<T6>.ComponentType,Component<T7>.ComponentType,Component<T8>.ComponentType,Component<T9>.ComponentType,Component<T10>.ComponentType,Component<T11>.ComponentType,Component<T12>.ComponentType,Component<T13>.ComponentType,Component<T14>.ComponentType,Component<T15>.ComponentType,Component<T16>.ComponentType,Component<T17>.ComponentType,Component<T18>.ComponentType,Component<T19>.ComponentType, });
        Hash = Signature.GetHashCode();
    }
}


/// <inheritdoc cref="Component"/>
public static class Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>
{
    internal static readonly int Id;

    /// <summary>
    ///     An <see cref="Signature"/> for this given set of components.
    /// </summary>
    public static readonly Signature Signature;

    /// <summary>
    ///     The hash code for this given set of components.
    /// </summary>
    public static readonly int Hash;

    static Component()
    {
        Id = Interlocked.Increment(ref Component.Id);
        Signature = new Signature(new [] { Component<T0>.ComponentType,Component<T1>.ComponentType,Component<T2>.ComponentType,Component<T3>.ComponentType,Component<T4>.ComponentType,Component<T5>.ComponentType,Component<T6>.ComponentType,Component<T7>.ComponentType,Component<T8>.ComponentType,Component<T9>.ComponentType,Component<T10>.ComponentType,Component<T11>.ComponentType,Component<T12>.ComponentType,Component<T13>.ComponentType,Component<T14>.ComponentType,Component<T15>.ComponentType,Component<T16>.ComponentType,Component<T17>.ComponentType,Component<T18>.ComponentType,Component<T19>.ComponentType,Component<T20>.ComponentType, });
        Hash = Signature.GetHashCode();
    }
}


/// <inheritdoc cref="Component"/>
public static class Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>
{
    internal static readonly int Id;

    /// <summary>
    ///     An <see cref="Signature"/> for this given set of components.
    /// </summary>
    public static readonly Signature Signature;

    /// <summary>
    ///     The hash code for this given set of components.
    /// </summary>
    public static readonly int Hash;

    static Component()
    {
        Id = Interlocked.Increment(ref Component.Id);
        Signature = new Signature(new [] { Component<T0>.ComponentType,Component<T1>.ComponentType,Component<T2>.ComponentType,Component<T3>.ComponentType,Component<T4>.ComponentType,Component<T5>.ComponentType,Component<T6>.ComponentType,Component<T7>.ComponentType,Component<T8>.ComponentType,Component<T9>.ComponentType,Component<T10>.ComponentType,Component<T11>.ComponentType,Component<T12>.ComponentType,Component<T13>.ComponentType,Component<T14>.ComponentType,Component<T15>.ComponentType,Component<T16>.ComponentType,Component<T17>.ComponentType,Component<T18>.ComponentType,Component<T19>.ComponentType,Component<T20>.ComponentType,Component<T21>.ComponentType, });
        Hash = Signature.GetHashCode();
    }
}


/// <inheritdoc cref="Component"/>
public static class Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>
{
    internal static readonly int Id;

    /// <summary>
    ///     An <see cref="Signature"/> for this given set of components.
    /// </summary>
    public static readonly Signature Signature;

    /// <summary>
    ///     The hash code for this given set of components.
    /// </summary>
    public static readonly int Hash;

    static Component()
    {
        Id = Interlocked.Increment(ref Component.Id);
        Signature = new Signature(new [] { Component<T0>.ComponentType,Component<T1>.ComponentType,Component<T2>.ComponentType,Component<T3>.ComponentType,Component<T4>.ComponentType,Component<T5>.ComponentType,Component<T6>.ComponentType,Component<T7>.ComponentType,Component<T8>.ComponentType,Component<T9>.ComponentType,Component<T10>.ComponentType,Component<T11>.ComponentType,Component<T12>.ComponentType,Component<T13>.ComponentType,Component<T14>.ComponentType,Component<T15>.ComponentType,Component<T16>.ComponentType,Component<T17>.ComponentType,Component<T18>.ComponentType,Component<T19>.ComponentType,Component<T20>.ComponentType,Component<T21>.ComponentType,Component<T22>.ComponentType, });
        Hash = Signature.GetHashCode();
    }
}


/// <inheritdoc cref="Component"/>
public static class Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>
{
    internal static readonly int Id;

    /// <summary>
    ///     An <see cref="Signature"/> for this given set of components.
    /// </summary>
    public static readonly Signature Signature;

    /// <summary>
    ///     The hash code for this given set of components.
    /// </summary>
    public static readonly int Hash;

    static Component()
    {
        Id = Interlocked.Increment(ref Component.Id);
        Signature = new Signature(new [] { Component<T0>.ComponentType,Component<T1>.ComponentType,Component<T2>.ComponentType,Component<T3>.ComponentType,Component<T4>.ComponentType,Component<T5>.ComponentType,Component<T6>.ComponentType,Component<T7>.ComponentType,Component<T8>.ComponentType,Component<T9>.ComponentType,Component<T10>.ComponentType,Component<T11>.ComponentType,Component<T12>.ComponentType,Component<T13>.ComponentType,Component<T14>.ComponentType,Component<T15>.ComponentType,Component<T16>.ComponentType,Component<T17>.ComponentType,Component<T18>.ComponentType,Component<T19>.ComponentType,Component<T20>.ComponentType,Component<T21>.ComponentType,Component<T22>.ComponentType,Component<T23>.ComponentType, });
        Hash = Signature.GetHashCode();
    }
}


/// <inheritdoc cref="Component"/>
public static class Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>
{
    internal static readonly int Id;

    /// <summary>
    ///     An <see cref="Signature"/> for this given set of components.
    /// </summary>
    public static readonly Signature Signature;

    /// <summary>
    ///     The hash code for this given set of components.
    /// </summary>
    public static readonly int Hash;

    static Component()
    {
        Id = Interlocked.Increment(ref Component.Id);
        Signature = new Signature(new [] { Component<T0>.ComponentType,Component<T1>.ComponentType,Component<T2>.ComponentType,Component<T3>.ComponentType,Component<T4>.ComponentType,Component<T5>.ComponentType,Component<T6>.ComponentType,Component<T7>.ComponentType,Component<T8>.ComponentType,Component<T9>.ComponentType,Component<T10>.ComponentType,Component<T11>.ComponentType,Component<T12>.ComponentType,Component<T13>.ComponentType,Component<T14>.ComponentType,Component<T15>.ComponentType,Component<T16>.ComponentType,Component<T17>.ComponentType,Component<T18>.ComponentType,Component<T19>.ComponentType,Component<T20>.ComponentType,Component<T21>.ComponentType,Component<T22>.ComponentType,Component<T23>.ComponentType,Component<T24>.ComponentType, });
        Hash = Signature.GetHashCode();
    }
}



