## Mod Creation Project

### Overview
This folder contains a C# project designed to help you create the mods for **Entropy Survivors**. The project includes all the necessary code and resources to develop the mods described earlier.

### Key Features
- **Mod Creation**: Easily create mods such as Enhanced Powerups and Increased Difficulty.
- **User-Friendly**: The project is designed to be straightforward and easy to use.

### Installation and Usage
To use this project, follow these steps:
1. Download and extract the project folder.
2. Open the project in your preferred C# development environment (e.g., Visual Studio).
3. Follow the instructions in the project README to create your mods.
4. Once your mod is created, use a program called **RePak** to pack the asset folder:


# UAssetAPI
[![CI Status](https://img.shields.io/github/actions/workflow/status/atenfyr/UAssetAPI/build.yml?label=CI)](https://github.com/atenfyr/UAssetAPI/actions)
[![Issues](https://img.shields.io/github/issues/atenfyr/UAssetAPI.svg?style=flat-square)](https://github.com/atenfyr/UAssetAPI/issues)
[![License](https://img.shields.io/github/license/atenfyr/UAssetAPI.svg?style=flat-square)](https://github.com/atenfyr/UAssetAPI/blob/master/LICENSE.md)

UAssetAPI is a low-level .NET library for reading and writing Unreal Engine game assets.

<img src="https://i.imgur.com/GZbr93m.png" align="center">

## Features
- Low-level read/write capability for a wide variety of cooked and uncooked .uasset files from ~4.13 to 5.3
- Support for more than 100 property types and 12 export types
- Support for JSON export and import to a proprietary format that maintains binary equality
- Support for reading and writing raw Kismet (blueprint) bytecode
- Reading capability for the unofficial .usmap format to parse ambiguous and unversioned properties
- Robust fail-safes for many properties and exports that fail serialization
- Automatic reflection for new property types in other loaded assemblies

## Usage
To get started using UAssetAPI, first build the API using the [Build Instructions guide](https://atenfyr.github.io/UAssetAPI/guide/build.html) and learn how to perform basic operations on your cooked .uasset files using the [Basic Usage guide](https://atenfyr.github.io/UAssetAPI/guide/basic.html).

UAssetGUI, a graphical wrapper around UAssetAPI which allows you to directly view and modify game assets by hand, is also available and can be downloaded for free on GitHub at [https://github.com/atenfyr/UAssetGUI/releases](https://github.com/atenfyr/UAssetGUI/releases).

## Contributing
All contributions, whether through pull requests or issues, that you may make are greatly appreciated.

I am particularly interested in .uasset files that have their `VerifyBinaryEquality()` method return false (or display "failed to maintain binary equality" within [UAssetGUI](https://github.com/atenfyr/UAssetGUI)); if you encounter such an asset, feel free to submit an issue here with a copy of the asset in question along with the name of the game and the Unreal version that it was cooked with.

## License
UAssetAPI and UAssetGUI are distributed under the MIT license, which you can view in detail in the [LICENSE file](LICENSE).
