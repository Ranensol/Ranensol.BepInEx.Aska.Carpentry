<p align="center">
  <img src="icon.png" alt="Ranensol's Carpentry" width="256"/>
</p>

# Aska - Ranensol's Carpentry

![GitHub release](https://img.shields.io/github/v/release/Ranensol/Ranensol.BepInEx.Aska.Carpentry)

A BepInEx mod for ASKA that allows you to customize the output quantities of carpenter bench recipes.

## Features

- ⚙️ Adjust output quantities for all 4 carpenter recipes
- 📝 Simple configuration file
- 🔄 Automatic bench capacity adjustment
- 🌐 Works in single-player and multiplayer

## What This Mod Does

Allows you to change how many items each carpenter recipe produces:

**Carpentry A (Axe Station):**
- Shafts (from hardwood long stick) - default: 2
- Planks (from hardwood log) - default: 2

**Carpentry B (Drawing Knife Station):**
- Beams (from hardwood long stick) - default: 1
- Posts (from hardwood log) - default: 1

The input requirements remain the same (always 1 item in), but you control how many items come out!

## Installation

### IMPORTANT

**Always backup your save games before installing any mod.** This applies to both local and server saves.

If you have existing villager tasks for carpentry (e.g. make 10 shafts) and you change the amount of shafts you get from 1 long stick you may need to go in a set the amount in the task again. 
Other than that there's nothing needed in game, it'll work as normal.

### Prerequisites

**Install BepInEx 6 (IL2CPP)** if you haven't already:
   - Download the latest **IL2CPP** build from [BepInEx builds](https://builds.bepinex.dev/projects/bepinex_be)
   - Extract the contents to your ASKA game folder (where `ASKA.exe` is located)
   - Launch the game once to initialize BepInEx, then close it

### Installing the Mod

1. **Download this mod** from the [Releases](../../releases) page
2. **Extract** the DLL to `BepInEx/plugins/` folder
3. **Launch the game** - a config file will be generated automatically
4. **Edit the config** at `BepInEx/config/Ranensol.BepInEx.Aska.Carpentry.cfg`
5. **Restart the game** for changes to take effect

Your folder structure should look like:
```
ASKA/
├── ASKA.exe
├── BepInEx/
│   ├── plugins/
│   │   └── Ranensol.BepInEx.Aska.Carpentry.dll
│   └── config/
│       └── Ranensol.BepInEx.Aska.Carpentry.cfg
└── ...
```

## Configuration

After first launch, edit `BepInEx/config/Ranensol.BepInEx.Aska.Carpentry.cfg`:

```ini
[Carpentry A - Quantities]
## Number of shafts produced from 1 hardwood long stick (default: 2)
Shafts = 2

## Number of planks produced from 1 hardwood log (default: 2)
Planks = 2

[Carpentry B - Quantities]
## Number of beams produced from 1 hardwood long stick (default: 1)
Beams = 1

## Number of posts produced from 1 hardwood log (default: 1)
Posts = 1
```

### Configuration Notes

- **Default values match vanilla** - the mod doesn't change anything until you edit the config
- **Values must be at least 1** - invalid values will be rejected and defaults used
- **Use reasonable numbers** - If you set daft amounts, expect daft results
- **Multiplayer:** All players and the server must use the same config values
- The bench output capacity automatically adjusts to fit your configured quantities

## Examples

Want to make carpenter benches more efficient? Try these:

```ini
Shafts = 4    # Double vanilla output
Planks = 3    # 50% more planks
Beams = 2     # Double vanilla output  
Posts = 2     # Double vanilla output
```

## Compatibility

- ✅ No upper limit on quantities (intentional for mod compatibility)
- ✅ Works in single-player and multiplayer
- ⚠️ All players in multiplayer must have matching configs

## Troubleshooting

**Recipes aren't producing the configured amount:**
- Check BepInEx console for error messages
- Verify your values are at least 1
- Make sure you restarted the game after changing the config
- In multiplayer, ensure all clients and server have matching configs

**Config file not appearing:**
- Launch the game at least once with the mod installed
- Check `BepInEx/config/` folder

## Support

Found a bug or have a suggestion? Open an issue on GitHub!

## Building from Source

### Prerequisites
- Visual Studio 2022 or later
- .NET 6 SDK
- ASKA installed with BepInEx 6 (IL2CPP)

### Setup

1. Clone the repository
2. Reference the required assemblies from your ASKA installation:
   - `Assembly-CSharp.dll`
   - `Il2Cppmscorlib.dll`
   - `Il2CppSystem.Core.dll`
3. Reference BepInEx assemblies from your BepInEx installation
4. Build the solution

## Credits

Created by Ranensol

## License

MIT License - See LICENSE file for details