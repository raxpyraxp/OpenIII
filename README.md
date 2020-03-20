![OpenIII](https://github.com/worm202/OpenIII/blob/master/OpenIII/Resources/cover.png)

# OpenIII

OpenIII is an ultimate all-in-one tool to modify a GTA game that works with 3D era games (GTA III, GTA: Vice City and GTA: San Andreas).

This project is in active development and is not ready yet for production.

## Project naming notice

This project is codenamed as OpenIII. Despite the fact that this tool is also related to the Grand Theft Auto modding like it's similar-named project OpenIV there is no relation to OpenIV, .black - the RAGE research project or any OpenIV developer. The real project name is still not yet selected and OpenIII is used more like "working codename" for a tool that is aiming to do similar things like OpenIV but for the 3D era of GTA.

## System requirements

### Operating Systems

OpenIII should work on all Microsoft Windows versions up from Windows XP.

Windows 7 and lower users required to have a .NET Framework 4.0 installed.

This tool aims to work with very old GTA games some of which requires even lower versions of Windows. To comply with that fact we'll try to support at least Windows XP as much as possible. We might reconsider this decision even before first release as all statistics pointing out that anicent Windows systems getting lower and lower user base and we might eventually drop support for old OSes. But for now we're focusing on Windows XP and later and we're testing our tool on this OS from time to time.

#### Linux/macOS

We also know that macOS have the official support for all 3D era of GTA that are distributed on Steam or App Store. Linux users are also able to run these games through Wine or Proton tools without any major problems. According to [WineHQ database](https://appdb.winehq.org/) all 3D era games mostly have no issues in Wine and have a Gold/Platinum status.

Another thing is that Mono project provides the ability to run .NET Framework applications on that operating systems as well and in theory it might run OpenIII. Because of that we think that we need to make a clear statement regarding Linux/macOS support:

We still don't know which limitations can we face with the planned features especially with anicent Windows systems and Mono. In this case we decided to make it Mono-compatiable as much as we can. We will test all our builds to work in Mono environment from time to time.

But you should be warned that because our tool is not even ready for production yet, our primary focus is to make all planned OpenIII features to be fully compatiable with Windows environment. That might lead to some features that will be known not to work in Mono to be disabled in that environment. We might even drop any support for OpenIII on Mono until proper solution found but this decision can be applied only if we'll face a really serious problem with OpenIII on Mono.

If we'll be forced to limit some features on this OSes, we'll create a wiki page with all disabled features for Mono.

### Supported Grand Theft Auto Versions

Right now these versions are supported:

- GTA III (PC)
- GTA: Vice City (PC)
- GTA: San Andreas (PC)

Our primary focus is any PC versions of 3D era of GTA games (based on RenderWare engine) including GTA III, GTA: Vice City and GTA: San Andreas. Also Rockstar doesn't update them too often and if they will - they don't introduce new versions of resource file formats and they're mostly focus on the executable code and game contents rather it's formats. At this moment OpenIII is independent from the actual exe version now, unlike ASI scripts for example, so we can say that it is compatiable with every single PC version.

macOS App Store and Steam versions technically are Windows versions running through Cider, so they're supported too. We plan to support mobile versions as well, including Android and iOS.

Support for console versions is also planned, at least for the original game versions (Original XBOX and PlayStation 2). But it could be limited to be read-only as running a modified game on console is tricky and often requires modified device.

**GTA: Liberty City Stories** and **GTA: Vice City Stories** are both under a big question for us because both of them use file formats incompatiable with the standard GTA 3D era games. Some of them are not yet reverse-engineered well. We might check on mobile version at least, as it is easier to mod the game on Android at least than on console.

## Build requirements

Build OpenIII.sln using Visual Studio 2019.

## Documentation

Documentation is under construction

## Contributing

Requirements are under construction

## Authors

- [raxp](https://github.com/worm202)
- [PrographerMan](https://github.com/PrographerMan)

## License

This program is licensed under GNU General Public License version 3 or later.

This program comes with ABSOLUTELY NO WARRANTY. [See details](https://www.gnu.org/licenses/gpl-3.0.html#section15).

This is free software, and you are welcome to redistribute it under certain conditions. [See details](https://www.gnu.org/licenses/gpl-3.0.html).
