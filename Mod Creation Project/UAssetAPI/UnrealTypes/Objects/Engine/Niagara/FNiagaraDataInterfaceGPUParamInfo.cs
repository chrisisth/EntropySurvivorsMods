﻿using System;
using UAssetAPI.CustomVersions;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.UnrealTypes;

public class FNiagaraDataInterfaceGeneratedFunction
{
    /** Name of the function as defined by the data interface. */
    public FName DefinitionName;

    /** Name of the instance. Derived from the definition name but made unique for this DI instance and specifier values. */
    public FString InstanceName;

    /** Specifier values for this instance. */
    public (FName, FName)[] Specifiers;

    public FNiagaraDataInterfaceGeneratedFunction(AssetBinaryReader reader)
    {
        DefinitionName = reader.ReadFName();
        InstanceName = reader.ReadFString();

        int num = reader.ReadInt32();
        Specifiers = new (FName, FName)[num];
        for (int i = 0; i < num; i++)
        {
            Specifiers[i] = (reader.ReadFName(), reader.ReadFName());
        }
    }

    public void Write(AssetBinaryWriter writer)
    {
        writer.Write(DefinitionName);
        writer.Write(InstanceName);

        writer.Write(Specifiers.Length);
        foreach (var spec in Specifiers)
        {
            writer.Write(spec.Item1);
            writer.Write(spec.Item2);
        }
    }
}

public class FNiagaraDataInterfaceGPUParamInfo
{
    /** Symbol of this DI in the hlsl. Used for binding parameters. */
    public FString DataInterfaceHLSLSymbol;

    /** Name of the class for this data interface. Used for constructing the correct parameters struct. */
    public FString DIClassName;

    /** Information about all the functions generated by the translator for this data interface. */
    public FNiagaraDataInterfaceGeneratedFunction[] GeneratedFunctions = [];

    public FNiagaraDataInterfaceGPUParamInfo()
    {

    }

    public FNiagaraDataInterfaceGPUParamInfo(AssetBinaryReader reader)
    {
        DataInterfaceHLSLSymbol = reader.ReadFString();
        DIClassName = reader.ReadFString();

        if (reader.Asset.GetCustomVersion<FNiagaraCustomVersion>() >= FNiagaraCustomVersion.AddGeneratedFunctionsToGPUParamInfo)
        {
            int num = reader.ReadInt32();
            GeneratedFunctions = new FNiagaraDataInterfaceGeneratedFunction[num];
            for (int i = 0; i < num; i++)
            {
                GeneratedFunctions[i] = new FNiagaraDataInterfaceGeneratedFunction(reader);
            }
        }
    }

    public int Write(AssetBinaryWriter writer)
    {
        var offset = writer.BaseStream.Position;
        writer.Write(DataInterfaceHLSLSymbol);
        writer.Write(DIClassName);
        if (writer.Asset.GetCustomVersion<FNiagaraCustomVersion>() >= FNiagaraCustomVersion.AddGeneratedFunctionsToGPUParamInfo)
        {
            if (GeneratedFunctions == null) GeneratedFunctions = Array.Empty<FNiagaraDataInterfaceGeneratedFunction>();
            writer.Write(GeneratedFunctions.Length);
            foreach (var func in GeneratedFunctions)
            {
                func.Write(writer);
            }
        }

        return (int)(writer.BaseStream.Position - offset);
    }
}
