using Arch.Samples;

// Info:
// This sample demonstrates a example usage of `Arch`.
// Especially a few different iteration techniques for entity iterations.
// Its not a full demonstration of all features.
// Hit "delete" to remove velocity from all entities.

// Disclaimer:
// You can spawn in to 1 million entities, then the performance starts dropping.
// The bottleneck is not the ECS framework, it's actually the rendering (Monogame Spritebatch).

Console.WriteLine("Sample App starts...");
using var game = new Game();
game.Run();

Environment.Exit(0);
