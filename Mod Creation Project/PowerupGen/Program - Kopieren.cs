using System;
using System.Collections.Generic;
using System.Linq;
using UAssetAPI;
using UAssetAPI.ExportTypes;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.PropertyTypes.Structs;
using UAssetAPI.UnrealTypes;
using UAssetAPI.Unversioned;

class Program
{
    static void Main(string[] args)
    {
        string filePath = @"F:\Downloads\FModel\Output\Exports\EntropySurvivors\Content\Database\Equipment\AutoAbilities\Frog\BallLightning\DA_Auto_Frog_BallLightning.uasset";
        UAsset myAsset = new UAsset(filePath, EngineVersion.VER_UE4_5, new Usmap("Mappings.usmap"));

        var parameters = GetParameters(myAsset);

        var levels = ((StructPropertyData)((StructPropertyData)((NormalExport)myAsset.Exports[0]).Data[0]).Value.FirstOrDefault(x => x.Name.Value.Value == "Levels")).Value;

        for (int ilevels = 0; ilevels < levels.Count; ilevels++)
        {
            StructPropertyData levelStruct = (StructPropertyData)levels[ilevels];
            ArrayPropertyData parameterUpgrades = (ArrayPropertyData)levelStruct.Value.FirstOrDefault(x => x.Name.Value.Value == "ParameterUpgrades");
            ArrayPropertyData bonusUpgrades = (ArrayPropertyData)levelStruct.Value.FirstOrDefault(x => x.Name.Value.Value == "BonusUpgrades");

            if (parameterUpgrades != null && bonusUpgrades != null)
            {
                List<PropertyData> newParameterUpgrades = new List<PropertyData>();
                List<PropertyData> newBonusUpgrades = new List<PropertyData>(bonusUpgrades.Value);

                foreach (StructPropertyData upgrade in parameterUpgrades.Value)
                {
                    NamePropertyData paramID = (NamePropertyData)upgrade.Value.FirstOrDefault(x => x.Name.Value.Value == "ParamID");
                    IntPropertyData levelsAdded = (IntPropertyData)upgrade.Value.FirstOrDefault(x => x.Name.Value.Value == "LevelsAdded");

                    if (paramID != null && levelsAdded != null)
                    {
                        if (!paramID.Value.Value.Contains("Shared"))
                        {
                            // Entfernen aus ParameterUpgrades und Hinzufügen zu BonusUpgrades
                            newBonusUpgrades.Add(new StructPropertyData
                            {
                                Name = new FName(myAsset, "BonusUpgrade"),
                                Value = new List<PropertyData>
                                {
                                    new StructPropertyData
                                    {
                                        Name = new FName(myAsset, "ParamID"),
                                        Value = new List<PropertyData>
                                        {
                                            new NamePropertyData
                                            {
                                                Name = new FName(myAsset, "TagName"),
                                                Value = paramID.Value
                                            }
                                        }
                                    },
                                    new IntPropertyData
                                    {
                                        Name = new FName(myAsset, "LevelsAdded"),
                                        Value = levelsAdded.Value
                                    },
                                    new FloatPropertyData
                                    {
                                        Name = new FName(myAsset, "ProbabilityWeight"),
                                        Value = 1.0f
                                    }
                                }
                            });
                        }
                        else
                        {
                            newParameterUpgrades.Add(upgrade);
                        }
                    }
                }

                // Aktualisieren der Listen
                parameterUpgrades.Value = newParameterUpgrades.ToArray();
                bonusUpgrades.Value = newBonusUpgrades.ToArray();
            }
        }

        // Speichern der Änderungen
        myAsset.Write(filePath);
    }

    private static List<string> GetParameters(UAsset myAsset)
    {
        List<string> tagNames = new List<string>();
        foreach (var export in myAsset.Exports)
        {
            if (export is NormalExport normalExport && normalExport.ClassIndex.IsImport() && normalExport.ClassIndex.ToImport(myAsset).ObjectName.Value.ToString() == "EquipmentData")
            {
                foreach (var property in normalExport.Data)
                {
                    if (property.Name.ToString() == "EquipmentConfig" && property is StructPropertyData structProperty)
                    {
                        var parameters = structProperty.Value.Find(p => p.Name.ToString() == "Parameters") as ArrayPropertyData;
                        if (parameters != null)
                        {
                            foreach (var element in parameters.Value)
                            {
                                var paramIDStruct = (element as StructPropertyData)?.Value.Find(p => p.Name.ToString() == "ParamID") as StructPropertyData;
                                var tagNameProperty = paramIDStruct?.Value.Find(p => p.Name.ToString() == "TagName") as NamePropertyData;
                                if (tagNameProperty != null)
                                {
                                    tagNames.Add(tagNameProperty.Value.Value.ToString());
                                }
                            }
                        }
                    }
                }
            }
        }
        return tagNames;
    }
}
