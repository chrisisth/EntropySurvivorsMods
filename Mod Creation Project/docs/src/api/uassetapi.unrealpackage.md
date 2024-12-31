# UnrealPackage

Namespace: UAssetAPI

```csharp
public abstract class UnrealPackage : UAssetAPI.IO.INameMap
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [UnrealPackage](./uassetapi.unrealpackage.md)<br>
Implements [INameMap](./uassetapi.io.inamemap.md)

## Fields

### **Info**

Agent string to provide context in serialized JSON.

```csharp
public string Info;
```

### **FilePath**

The path of the file on disk that this asset represents. This does not need to be specified for regular parsing.

```csharp
public string FilePath;
```

### **Mappings**

The corresponding mapping data for the game that this asset is from. Optional unless unversioned properties are present.

```csharp
public Usmap Mappings;
```

### **CustomSerializationFlags**

List of custom serialization flags, used to override certain optional behavior in how UAssetAPI serializes assets.

```csharp
public CustomSerializationFlags CustomSerializationFlags;
```

### **UseSeparateBulkDataFiles**

Should the asset be split into separate .uasset, .uexp, and .ubulk files, as opposed to one single .uasset file?

```csharp
public bool UseSeparateBulkDataFiles;
```

### **IsUnversioned**

Should this asset not serialize its engine and custom versions?

```csharp
public bool IsUnversioned;
```

### **FileVersionLicenseeUE**

The licensee file version. Used by some games to add their own Engine-level versioning.

```csharp
public int FileVersionLicenseeUE;
```

### **ObjectVersion**

The object version of UE4 that will be used to parse this asset.

```csharp
public ObjectVersion ObjectVersion;
```

### **ObjectVersionUE5**

The object version of UE5 that will be used to parse this asset. Set to [ObjectVersionUE5.UNKNOWN](./uassetapi.unrealtypes.objectversionue5.md#unknown) for UE4 games.

```csharp
public ObjectVersionUE5 ObjectVersionUE5;
```

### **CustomVersionContainer**

All the custom versions stored in the archive.

```csharp
public List<CustomVersion> CustomVersionContainer;
```

### **GatherableTextData**

Map of the gatherable text data.

```csharp
public List<FGatherableTextData> GatherableTextData;
```

### **Exports**

Map of object exports. UAssetAPI used to call these "categories."

```csharp
public List<Export> Exports;
```

### **SearchableNames**

List of Searchable Names, by object containing them. Sorted to keep order consistent.

```csharp
public SortedDictionary<FPackageIndex, List<FName>> SearchableNames;
```

### **Thumbnails**

Map of object full names to the thumbnails

```csharp
public Dictionary<string, FObjectThumbnail> Thumbnails;
```

### **WorldTileInfo**

Tile information used by WorldComposition.
 Defines properties necessary for tile positioning in the world.

```csharp
public FWorldTileInfo WorldTileInfo;
```

### **MapStructTypeOverride**

In MapProperties that have StructProperties as their keys or values, there is no universal, context-free way to determine the type of the struct.



To that end, this dictionary maps MapProperty names to the type of the structs within them (tuple of key struct type and value struct type) if they are not None-terminated property lists.

```csharp
public Dictionary<string, Tuple<FString, FString>> MapStructTypeOverride;
```

### **ArrayStructTypeOverride**

IN ENGINE VERSIONS BEFORE [ObjectVersion.VER_UE4_INNER_ARRAY_TAG_INFO](./uassetapi.unrealtypes.objectversion.md#ver_ue4_inner_array_tag_info):



In ArrayProperties that have StructProperties as their keys or values, there is no universal, context-free way to determine the type of the struct. To that end, this dictionary maps ArrayProperty names to the type of the structs within them.

```csharp
public Dictionary<string, FString> ArrayStructTypeOverride;
```

## Properties

### **PackageFlags**

The flags for this package.

```csharp
public EPackageFlags PackageFlags { get; set; }
```

#### Property Value

[EPackageFlags](./uassetapi.unrealtypes.epackageflags.md)<br>

### **HasUnversionedProperties**

Whether or not this asset uses unversioned properties.

```csharp
public bool HasUnversionedProperties { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **IsFilterEditorOnly**

Whether or not this asset has PKG_FilterEditorOnly flag.

```csharp
public bool IsFilterEditorOnly { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

## Methods

### **FixNameMapLookupIfNeeded()**

```csharp
internal void FixNameMapLookupIfNeeded()
```

### **GetNameMapIndexList()**

Returns the name map as a read-only list of FStrings.

```csharp
public IReadOnlyList<FString> GetNameMapIndexList()
```

#### Returns

[IReadOnlyList&lt;FString&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
The name map as a read-only list of FStrings.

### **ClearNameIndexList()**

Clears the name map. This method should be used with extreme caution, as it may break unparsed references to the name map.

```csharp
public void ClearNameIndexList()
```

### **SetNameReference(Int32, FString)**

Replaces a value in the name map at a particular index.

```csharp
public void SetNameReference(int index, FString value)
```

#### Parameters

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The index to overwrite in the name map.

`value` [FString](./uassetapi.unrealtypes.fstring.md)<br>
The value that will be replaced in the name map.

### **GetNameReference(Int32)**

Gets a value in the name map at a particular index.

```csharp
public FString GetNameReference(int index)
```

#### Parameters

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The index to return the value at.

#### Returns

[FString](./uassetapi.unrealtypes.fstring.md)<br>
The value at the index provided.

### **GetNameReferenceWithoutZero(Int32)**

Gets a value in the name map at a particular index, but with the index zero being treated as if it is not valid.

```csharp
public FString GetNameReferenceWithoutZero(int index)
```

#### Parameters

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The index to return the value at.

#### Returns

[FString](./uassetapi.unrealtypes.fstring.md)<br>
The value at the index provided.

### **ContainsNameReference(FString)**

Checks whether or not the value exists in the name map.

```csharp
public bool ContainsNameReference(FString search)
```

#### Parameters

`search` [FString](./uassetapi.unrealtypes.fstring.md)<br>
The value to search the name map for.

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
true if the value appears in the name map, otherwise false.

### **SearchNameReference(FString)**

Searches the name map for a particular value.

```csharp
public int SearchNameReference(FString search)
```

#### Parameters

`search` [FString](./uassetapi.unrealtypes.fstring.md)<br>
The value to search the name map for.

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The index at which the value appears in the name map.

#### Exceptions

[NameMapOutOfRangeException](./uassetapi.namemapoutofrangeexception.md)<br>
Thrown when the value provided does not appear in the name map.

### **AddNameReference(FString, Boolean)**

Adds a new value to the name map.

```csharp
public int AddNameReference(FString name, bool forceAddDuplicates)
```

#### Parameters

`name` [FString](./uassetapi.unrealtypes.fstring.md)<br>
The value to add to the name map.

`forceAddDuplicates` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether or not to add a new entry if the value provided already exists in the name map.

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The index of the new value in the name map. If the value already existed in the name map beforehand, that index will be returned instead.

#### Exceptions

[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
Thrown when forceAddDuplicates is false and the value provided is null or empty.

### **CanCreateDummies()**

Whether or not we can create dummies in this name map. If false, attempting to define a dummy will append to the name map instead.

```csharp
public bool CanCreateDummies()
```

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
A boolean.

### **PathToStream(String)**

Creates a MemoryStream from an asset path.

```csharp
public MemoryStream PathToStream(string p)
```

#### Parameters

`p` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The path to the input file.

#### Returns

[MemoryStream](https://docs.microsoft.com/en-us/dotnet/api/system.io.memorystream)<br>
A new MemoryStream that stores the binary data of the input file.

### **PathToReader(String)**

Creates a BinaryReader from an asset path.

```csharp
public AssetBinaryReader PathToReader(string p)
```

#### Parameters

`p` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The path to the input file.

#### Returns

[AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>
A new BinaryReader that stores the binary data of the input file.

### **GetClassExport()**

Searches for and returns this asset's ClassExport, if one exists.

```csharp
public ClassExport GetClassExport()
```

#### Returns

[ClassExport](./uassetapi.exporttypes.classexport.md)<br>
The asset's ClassExport if one exists, otherwise null.

### **GetParentClass(FName&, FName&)**

Finds the class path and export name of the SuperStruct of this asset, if it exists.

```csharp
public void GetParentClass(FName& parentClassPath, FName& parentClassExportName)
```

#### Parameters

`parentClassPath` [FName&](./uassetapi.unrealtypes.fname&.md)<br>
The class path of the SuperStruct of this asset, if it exists.

`parentClassExportName` [FName&](./uassetapi.unrealtypes.fname&.md)<br>
The export name of the SuperStruct of this asset, if it exists.

### **GetParentClassExportName(FName&)**

```csharp
internal FName GetParentClassExportName(FName& modulePath)
```

#### Parameters

`modulePath` [FName&](./uassetapi.unrealtypes.fname&.md)<br>

#### Returns

[FName](./uassetapi.unrealtypes.fname.md)<br>

### **ResolveAncestries()**

Resolves the ancestry of all properties present in this asset.

```csharp
public void ResolveAncestries()
```

### **FindAssetOnDiskFromPath(String)**

Attempt to find another asset on disk given an asset path (i.e. one starting with /Game/).

```csharp
public string FindAssetOnDiskFromPath(string path)
```

#### Parameters

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The asset path.

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The path to the file on disk, or null if none could be found.

### **PullSchemasFromAnotherAsset(FName, FName)**

Pull necessary schemas from another asset on disk by examining its StructExports. Updates the mappings in-situ.

```csharp
public bool PullSchemasFromAnotherAsset(FName path, FName desiredObject)
```

#### Parameters

`path` [FName](./uassetapi.unrealtypes.fname.md)<br>
The relative path or name to the other asset.

`desiredObject` [FName](./uassetapi.unrealtypes.fname.md)<br>
The object that this asset is being accessed for. Optional.

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether or not the operation was completed successfully.

### **SetEngineVersion(EngineVersion)**

Sets the version of the Unreal Engine to use in serialization.

```csharp
public void SetEngineVersion(EngineVersion newVersion)
```

#### Parameters

`newVersion` [EngineVersion](./uassetapi.unrealtypes.engineversion.md)<br>
The new version of the Unreal Engine to use in serialization.

#### Exceptions

[InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/system.invalidoperationexception)<br>
Thrown when an invalid EngineVersion is specified.

### **GetEngineVersion(ObjectVersion, ObjectVersionUE5, List&lt;CustomVersion&gt;)**

```csharp
public static EngineVersion GetEngineVersion(ObjectVersion objectVersion, ObjectVersionUE5 objectVersionUE5, List<CustomVersion> customVersionContainer)
```

#### Parameters

`objectVersion` [ObjectVersion](./uassetapi.unrealtypes.objectversion.md)<br>

`objectVersionUE5` [ObjectVersionUE5](./uassetapi.unrealtypes.objectversionue5.md)<br>

`customVersionContainer` [List&lt;CustomVersion&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>

#### Returns

[EngineVersion](./uassetapi.unrealtypes.engineversion.md)<br>

### **GetEngineVersion()**

Estimates the retail version of the Unreal Engine based on the object and custom versions.

```csharp
public EngineVersion GetEngineVersion()
```

#### Returns

[EngineVersion](./uassetapi.unrealtypes.engineversion.md)<br>
The estimated retail version of the Unreal Engine.

### **GetCustomVersion(Guid)**

Fetches the version of a custom version in this asset.

```csharp
public int GetCustomVersion(Guid key)
```

#### Parameters

`key` [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>
The GUID of the custom version to retrieve.

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The version of the retrieved custom version.

### **GetCustomVersion(String)**

Fetches the version of a custom version in this asset.

```csharp
public int GetCustomVersion(string friendlyName)
```

#### Parameters

`friendlyName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The friendly name of the custom version to retrieve.

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The version of the retrieved custom version.

### **GetCustomVersion&lt;T&gt;()**

Fetches a custom version's enum value based off of its type.

```csharp
public T GetCustomVersion<T>()
```

#### Type Parameters

`T`<br>
The enum type of the custom version to retrieve.

#### Returns

T<br>
The enum value of the requested custom version.

#### Exceptions

[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
Thrown when T is not an enumerated type.

### **GuessCustomVersionFromTypeAndEngineVersion(EngineVersion, Type)**

```csharp
public static int GuessCustomVersionFromTypeAndEngineVersion(EngineVersion chosenVersion, Type typ)
```

#### Parameters

`chosenVersion` [EngineVersion](./uassetapi.unrealtypes.engineversion.md)<br>

`typ` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **GetDefaultCustomVersionContainer(EngineVersion)**

Fetches a list of all default custom versions for a specific Unreal version.

```csharp
public static List<CustomVersion> GetDefaultCustomVersionContainer(EngineVersion chosenVersion)
```

#### Parameters

`chosenVersion` [EngineVersion](./uassetapi.unrealtypes.engineversion.md)<br>
The version of the engine to check against.

#### Returns

[List&lt;CustomVersion&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
A list of all the default custom version values for the given engine version.

### **ConvertExportToChildExportAndRead(AssetBinaryReader, Int32)**

```csharp
protected void ConvertExportToChildExportAndRead(AssetBinaryReader reader, int i)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

`i` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Read(AssetBinaryReader, Int32[], Int32[])**

Reads an asset into memory.

```csharp
public void Read(AssetBinaryReader reader, Int32[] manualSkips, Int32[] forceReads)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>
The input reader.

`manualSkips` [Int32[]](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
An array of export indexes to skip parsing. For most applications, this should be left blank.

`forceReads` [Int32[]](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
An array of export indexes that must be read, overriding entries in the manualSkips parameter. For most applications, this should be left blank.

#### Exceptions

[UnknownEngineVersionException](./uassetapi.unknownengineversionexception.md)<br>
Thrown when this is an unversioned asset and [UnrealPackage.ObjectVersion](./uassetapi.unrealpackage.md#objectversion) is unspecified.

[FormatException](https://docs.microsoft.com/en-us/dotnet/api/system.formatexception)<br>
Throw when the asset cannot be parsed correctly.

### **WriteData()**

Serializes an asset from memory.

```csharp
public MemoryStream WriteData()
```

#### Returns

[MemoryStream](https://docs.microsoft.com/en-us/dotnet/api/system.io.memorystream)<br>
A stream that the asset has been serialized to.

### **Write(MemoryStream&, MemoryStream&)**

Serializes and writes an asset to two split streams (.uasset and .uexp) from memory.

```csharp
public void Write(MemoryStream& uassetStream, MemoryStream& uexpStream)
```

#### Parameters

`uassetStream` [MemoryStream&](https://docs.microsoft.com/en-us/dotnet/api/system.io.memorystream&)<br>
A stream containing the contents of the .uasset file.

`uexpStream` [MemoryStream&](https://docs.microsoft.com/en-us/dotnet/api/system.io.memorystream&)<br>
A stream containing the contents of the .uexp file, if needed, otherwise null.

#### Exceptions

[UnknownEngineVersionException](./uassetapi.unknownengineversionexception.md)<br>
Thrown when [UnrealPackage.ObjectVersion](./uassetapi.unrealpackage.md#objectversion) is unspecified.

### **Write(String)**

Serializes and writes an asset to disk from memory.

```csharp
public void Write(string outputPath)
```

#### Parameters

`outputPath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The path on disk to write the asset to.

#### Exceptions

[UnknownEngineVersionException](./uassetapi.unknownengineversionexception.md)<br>
Thrown when [UnrealPackage.ObjectVersion](./uassetapi.unrealpackage.md#objectversion) is unspecified.
