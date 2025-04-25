![Arch](docs/arch-banner.png)
[![Discord](https://img.shields.io/discord/1099813114876284928?style=for-the-badge&logo=discord&label=Arch)](https://discord.gg/htc8tX3NxZ)
[![Maintenance](https://img.shields.io/badge/Maintained%3F-yes-green.svg?style=for-the-badge)](https://GitHub.com/Naereen/StrapDown.js/graphs/commit-activity)
[![Nuget](https://img.shields.io/nuget/v/Arch?style=for-the-badge)](https://www.nuget.org/packages/Arch/)
[![License](https://img.shields.io/badge/License-Apache_2.0-blue.svg?style=for-the-badge)](https://opensource.org/licenses/Apache-2.0)
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)

A high-performance C# based Archetype & Chunks [Entity Component System](https://www.wikiwand.com/en/Entity_component_system) (ECS) for game development and data-oriented programming.     

- 🏎️ **_FAST_** > Best cache efficiency, iteration, and allocation speed. Plays in the same league as C++/Rust ECS Libs! 
- 🚀 **_FASTER_** > Arch is on average quite faster than other ECS implemented in C#. Check out this [Benchmark](https://github.com/Doraku/Ecs.CSharp.Benchmark)!
- 🤏 **_BARE MINIMUM_** >  Not bloated, it's small and only provides the essentials for you! 
- ☕️ **_SIMPLE_** >  Promotes a clean, minimal, and self-explanatory API that is simple by design. Check out the [Wiki](https://github.com/genaray/Arch/wiki)!
- 💪 _**MAINTAINED**_ > It's actively being worked on, maintained, and comes along several [Extensions](https://github.com/genaray/Arch.Extended)! 
- 🚢 _**SUPPORT**_ > Supports .NetStandard 2.1, .Net Core 6 and 8, and therefore you may use it with Unity or Godot!

Download the [package](https://github.com/genaray/Arch/packages/1697222), get started today and join the [Discord](https://discord.gg/htc8tX3NxZ)!
```console
dotnet add PROJECT package Arch --version 2.1.0-beta
```

# ⏩ Quickstart

Arch is bare minimum, easy to use, and efficient. Let's say you want to create some game entities and make them move based on their velocity... sounds complicated?
It's not! Arch does everything for you, you only need to define the entities and the logic.

I bet you don't want to read tons of documentation, theory, and other boring stuff right?  
Let's just ignore all that deep knowledge and jump in directly to get something done. 

```cs
using Arch;

// Components
public record struct Position(float X, float Y);
public record struct Velocity(float Dx, float Dy);

// Create a world and an entity with position and velocity.
using var world = World.Create();
var adventurer = world.Create(new Position(0,0), new Velocity(1,1));

// Enumerate all entities with Position & Velocity to move them
var query = new QueryDescription().WithAll<Position,Velocity>();
world.Query(in query, (Entity entity, ref Position pos, ref Velocity vel) => {
    pos.X += vel.Dx;
    pos.Y += vel.Dy;
    Console.WriteLine($"Moved adventurer: {entity.Id}"); 
}); 
```
> [!NOTE]
> The example is very simple. There more features including queries without lambda or an API without generics and much more. Checkout the [Documentation](https://arch-ecs.gitbook.io/arch)!

# 💡 Highlights

This is all you need to know, with this little knowledge you are already able to bring your worlds to life.  
However, if you want to take a closer look at Arch's features and performance techniques, check out the [Wiki](https://arch-ecs.gitbook.io/arch)! 
There's more to explore, for example...

## 🤝 Our promise
- [x] Bare minimum - No overengineering, no abstracted hidden costs
- [x] Incredibly fast and efficient
- [x] Is actively maintained and developed
- [x] Grateful for every contribution 

## 🚀 Features
- [x] Archetypes with 16KB large chunks for your massive worlds
- [x] Incredibly small Entity size
- [x] Optional `Pure-ECS` for maximum performance and efficiency
- [x] Bulk/Batch Entity operations 
- [x] High-performance Queries
- [x] Multithreaded Queries
- [x] Enumerators
- [x] CommandBuffers
- [x] Events
- [x] Generic and Non-Generic API
- [x] AOT friendly
- [x] Several extensions with systems, tools, source generators and more
- [x] Monogame, Unity, Godot Integration guides
- And much more... 

# 🧩 Extensions

Arch has some extensions that add more features and tools. Among them for example : 
- 🛠️ **_[Arch.Extended](https://github.com/genaray/Arch.Extended)_** >  Adds a set of tools and features to save boilerplate code!
- 🔎 *_[Godot Entity Debugger](https://github.com/RoadTurtleGames/ArchGodotEntityDebugger)_* > An Arch Entity debugger for the Godot engine!
- 🔎 *_[Stride Entity Debugger](https://github.com/Doprez/stride-arch-ecs)_* > An example of Arch in the Stride engine, with additional entity and system inspector!
- 🔎 *_[Arch.Unity](https://github.com/AnnulusGames/Arch.Unity)_* > A library that makes the integration of Arch in Unity much easier, with many cool new features!
- 🔎 *_[Zinc-Framework](https://github.com/zinc-framework)_* > A small but fine engine framework which is based on Arch and also has its own source generator to define entities declaratively!
- ❓ **_Your Tool-Library?_** > If you develop more tools and features for Arch, let us know and we'll list them here!

# 🚀 Performance
Arch is already one of the fastest ECS and uses the best techniques under the hood to achieve maximum performance and efficiency. 
Care is always taken to find a healthy balance between CPU performance and ram utilization. 

If you are more interested, have a look at the [benchmark](https://github.com/Doraku/Ecs.CSharp.Benchmark)! 

# 📖 [Documentation](https://arch-ecs.gitbook.io/arch)
Were we able to convince you? If so, let's get started. 
We have prepared a whole wiki to explain all the important aspects and provide examples. 
Click here for the [documentation](https://arch-ecs.gitbook.io/arch)!

# 💻 Projects using Arch
Arch is already used in some projects, for a more detailed look, take a look at the [wiki](https://github.com/genaray/Arch/wiki/Projects-using-Arch)!

## [Space Station 14](https://spacestation14.io/)
Space Station 14 is inspired by the cult classic Space Station 13 and tells the extraordinary story of everything that can go wrong on a shift at a space station. You take on a role and complete your tasks so that the space station doesn't go to the dogs... or do the exact opposite. Prepare yourself for chaos and the finest roleplay. Best of all, SS14 is open-source and anyone can play!
![Ingame screenshot](https://spacestation14.com/images/home/gallery-medbay.jpg)

## [Roguelite-Survivor](https://github.com/proc-gen/roguelite-survivor)
An action-packed c# clone of the hit "vampire survivor" based on monogame and arch!
Fight your way through hordes of different enemies, level up your character, collect permanent items and explore various maps!
Try it out!
![Ingame screenshot](https://user-images.githubusercontent.com/65076703/232624411-6a9e8a29-3118-41a6-a8f3-dd8d9c8f0edf.png)

## [EquilibriumEngine-CSharp](https://github.com/clibequilibrium/EquilibriumEngine-CSharp)
Equilibrium Engine is a data-oriented C# game engine that takes advantage of ECS pattern followed by Hot-Reloading of your libraries which allows you to quickly iterate on different aspects of your projects.
![Equilibrium Engine screenshot](https://raw.githubusercontent.com/clibequilibrium/EquilibriumEngine-CSharp/master/docs/home.png)

# Contributors
<a href="https://github.com/genaray/Arch/graphs/contributors">
  <img src="https://contrib.rocks/image?repo=genaray/Arch" />
</a>

A huge thanks to all the supporters who did their part, especially [TwistableGolf](https://github.com/TwistableGolf) for their dedication and design of the official Arch logo and banner! 
