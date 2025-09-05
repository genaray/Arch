namespace Arch.Core;

/// <summary>
/// The <see cref="ChunkLayoutMode"/> enum
/// represents an enum to define the layout of each <see cref="Chunk"/> inside an <see cref="Archetype"/>.
/// </summary>
public enum ChunkLayoutMode
{
    FixedChunkSize,     // e.g. ChunkSizeBytes = 16 * 1024
    FixedEntityCount,   // e.g. TargetEntitiesPerChunk = 256
    MinChunkSizeAndMinEntities // Soft Ziele – z.B. min. 8kB, min. 64 Entities
}

/// <summary>
/// The <see cref="ChunkLayoutConfig"/> struct
/// represents a config
/// </summary>
public struct ChunkLayoutConfig()
{
    public ChunkLayoutMode LayoutMode { get; set; } = ChunkLayoutMode.MinChunkSizeAndMinEntities;

    public int FixedChunkSize { get; set; } = 32 * 1024;
    public int FixedEntityCount { get; set; } = 256;

    public int MinChunkSize { get; set; } = 16 * 1024;
    public int MinEntityCount { get; set; } = 64;
}
