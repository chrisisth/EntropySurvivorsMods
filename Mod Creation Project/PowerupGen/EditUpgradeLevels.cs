using UAssetAPI;
using UAssetAPI.ExportTypes;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.PropertyTypes.Structs;
using UAssetAPI.UnrealTypes;
using UAssetAPI.Unversioned;

internal class EditUpgradeLevels
{

    public EditUpgradeLevels()
    {
        Console.WriteLine("Please enter the file path to exported equipment assets:");
        string[] lines = File.ReadAllLines("AssetList.txt");
        foreach (string line in lines)
        {
            Console.WriteLine(line);
        }
        Console.WriteLine("Please enter the file path to exported equipment assets:");
        string path = Console.ReadLine();
        if (Directory.Exists(path))
        {
            try
            {
                string[] files = Directory.GetFiles(path, "*.uasset*", SearchOption.AllDirectories);


                foreach (string file in files)
                {
                    Console.WriteLine("Process file: " + file);
                    UAsset myAsset = new UAsset(file, EngineVersion.VER_UE4_5, new Usmap("Mappings.usmap"));
                    myAsset = ExtendLevels(myAsset);
                    myAsset = UpdateLevels(myAsset);
                    myAsset.Write(file);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has occurred: " + ex.Message);
            }
        }
        else
        {
            Console.WriteLine("The specified path does not exist.");
        }
    }

    private StructPropertyData CreateBonusParameterLevelUpgrade(UAsset myAsset, string tagName, int levelsAdded, float probabilityWeight, int index)
    {
        var namePropertyData = new NamePropertyData
        {
            ArrayIndex = 0,
            IsZero = false,
            Name = new FName(myAsset, "TagName"),
            PropertyTagFlags = EPropertyTagFlags.None,
            PropertyTagExtensions = EPropertyTagExtension.NoExtension,
            Value = new FName(myAsset, tagName)
        };

        var gameplayTagStructPropertyData = new StructPropertyData
        {
            ArrayIndex = 0,
            IsZero = false,
            Name = new FName(myAsset, "ParamID"),
            PropertyTagFlags = EPropertyTagFlags.None,
            PropertyTagExtensions = EPropertyTagExtension.NoExtension,
            StructType = new FName(myAsset, "GameplayTag"),
            SerializeNone = true,
            StructGUID = new Guid("{00000000-0000-0000-0000-000000000000}"),
            SerializationControl = EClassSerializationControlExtension.NoExtension,
            Operation = EOverriddenPropertyOperation.None,
            Value = new List<PropertyData> { namePropertyData }
        };

        // IntPropertyData for "LevelsAdded"
        var levelsAddedIntPropertyData = new IntPropertyData
        {
            ArrayIndex = 0,
            IsZero = false,
            Name = new FName(myAsset, "LevelsAdded"),
            PropertyTagFlags = EPropertyTagFlags.None,
            PropertyTagExtensions = EPropertyTagExtension.NoExtension,
            Value = levelsAdded
        };

        // FloatPropertyData for "ProbabilityWeight"
        var probabilityWeightFloatPropertyData = new FloatPropertyData
        {
            ArrayIndex = 0,
            IsZero = false,
            Name = new FName(myAsset, "ProbabilityWeight"),
            PropertyTagFlags = EPropertyTagFlags.None,
            PropertyTagExtensions = EPropertyTagExtension.NoExtension,
            Value = probabilityWeight
        };

        // StructPropertyData for "BonusParameterLevelUpgrade"
        var bonusParameterLevelUpgradeStructPropertyData = new StructPropertyData
        {
            ArrayIndex = 0,
            IsZero = false,
            Name = new FName(myAsset, index),
            PropertyTagFlags = EPropertyTagFlags.None,
            PropertyTagExtensions = EPropertyTagExtension.NoExtension,
            StructType = new FName(myAsset, "BonusParameterLevelUpgrade"),
            SerializeNone = true,
            StructGUID = new Guid("{00000000-0000-0000-0000-000000000000}"),
            SerializationControl = EClassSerializationControlExtension.NoExtension,
            Operation = EOverriddenPropertyOperation.None,
            Value = new List<PropertyData>
            {
                gameplayTagStructPropertyData,
                levelsAddedIntPropertyData,
                probabilityWeightFloatPropertyData
            }
        };

        return bonusParameterLevelUpgradeStructPropertyData;
    }

    private UAsset ExtendLevels(UAsset myAsset)
    {
        ArrayPropertyData levelList = (ArrayPropertyData)((StructPropertyData)((NormalExport)myAsset.Exports[0]).Data[0]).Value.FirstOrDefault(x => x.Name.Value.Value == "Levels");
        List<PropertyData> levels = new List<PropertyData>(levelList.Value);

        for (int ilevels = 0; ilevels < levelList.Value.Length; ilevels++)
        {
            var level = levelList.Value[ilevels];
            level.Name.Value.Value = "" + levelList.Value.Length + ilevels;
            levels.Add(level);
        }
        levelList.Value = levels.ToArray();
        return myAsset;
    }
    // Method to retrieve the list of parameters from the asset
    private List<string> GetParameters(UAsset myAsset)
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
                                    // Add the tag name to the list of parameter names
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

    private UAsset UpdateLevels(UAsset myAsset)
    {

        // Retrieve the parameters from the asset
        var parameters = GetParameters(myAsset);

        // Get the list of levels from the asset
        ArrayPropertyData levelList = (ArrayPropertyData)((StructPropertyData)((NormalExport)myAsset.Exports[0]).Data[0]).Value.FirstOrDefault(x => x.Name.Value.Value == "Levels");

        // Initialize new arrays for parameter and bonus upgrades
        ArrayPropertyData newParameterUpgrades = new ArrayPropertyData();
        ArrayPropertyData newBonusUpgrades = new ArrayPropertyData();

        // Iterate through each level in the level list
        for (int ilevels = 0; ilevels < levelList.Value.Length; ilevels++)
        {
            // Get the current level structure
            StructPropertyData levelStruct = (StructPropertyData)(levelList).Value[ilevels];
            // Get the parameter upgrades for the current level
            ArrayPropertyData parameterupgrades = (ArrayPropertyData)levelStruct.Value[0];
            // Get the bonus upgrades for the current level
            ArrayPropertyData bonusUpgrades = (ArrayPropertyData)levelStruct.Value[1];

            // Clone the parameter and bonus upgrades for modification
            newParameterUpgrades = (ArrayPropertyData)parameterupgrades.Clone();
            newBonusUpgrades = (ArrayPropertyData)bonusUpgrades.Clone();

            // Create a list of parameters to check
            var parameterstocheck = new List<string>(parameters.ToArray());

            // Iterate through the parameter upgrades
            for (int i = 0; i < parameterupgrades.Value.Length; i++)
            {
                StructPropertyData upgrade = (StructPropertyData)parameterupgrades.Value[i];
                // Get the tag name of the upgrade
                string tagName = ((PropertyData<FName>)((PropertyData)((StructPropertyData)upgrade.Value[0]).Value[0])).Value.Value.Value;

                if (tagName.Contains("Shared"))
                {
                    // Remove shared parameters from the list of parameters to check
                    if (parameters.Count > 1)
                    {
                        parameterstocheck.Remove(tagName);
                    }
                }
                else if (tagName.Contains("Passive"))
                {
                    //nothing yet
                }
                else
                {
                    // Remove auto abilities to add them as bonus only
                    var tmpnewParameterUpgrades = new List<PropertyData>(newParameterUpgrades.Value);
                    tmpnewParameterUpgrades.Remove(upgrade);
                    newParameterUpgrades.Value = tmpnewParameterUpgrades.ToArray();
                    tmpnewParameterUpgrades = null;
                }
            }

            // Update parameter upgrades with new values
            parameterupgrades = newParameterUpgrades;

            // Iterate through the bonus upgrades
            for (int i = 0; i < bonusUpgrades.Value.Length; i++)
            {
                StructPropertyData upgrade = (StructPropertyData)bonusUpgrades.Value[i];
                // Get the tag name of the bonus upgrade
                string tagName = ((PropertyData<FName>)((PropertyData)((StructPropertyData)upgrade.Value[0]).Value[0])).Value.Value.Value;

                // Remove parameters that are already a bonus upgrade in the current level
                parameterstocheck.Remove(tagName);
            }

            // Add new bonus upgrades
            var tmpnewBonusUpgrades = new List<PropertyData>(newBonusUpgrades.Value);
            Random random = new Random();
            foreach (var tagupgrade in parameterstocheck)
            {
                // Create a new bonus upgrade with random probability weight
                /*StructPropertyData bonusUpgrade = new StructPropertyData
                {
                    Name = new FName(myAsset, "BonusParameterLevelUpgrade"),
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
                                    Value = new FName(myAsset, tagupgrade)
                                }
                            }
                        },
                        new IntPropertyData
                        {
                            Name = new FName(myAsset, "LevelsAdded"),
                            Value = (1)
                        },
                        new FloatPropertyData
                        {
                            Name = new FName(myAsset, "ProbabilityWeight"),
                            Value = (float)(random.NextDouble() * (1.0 - 0.1) + 0.1)
                        }
                    }

                };*/
                int icparameters = parameters.Count <= 0 ? 1: parameters.Count;
                int iclevels = ilevels <= 0 ? 1 : ilevels;
                double factor = ((0.5+ (float)(random.NextDouble() * 0.5)) / icparameters) / iclevels;
                StructPropertyData bonusUpgrade = CreateBonusParameterLevelUpgrade(myAsset, tagupgrade, 1, (float)factor, tmpnewBonusUpgrades.Count);

                tmpnewBonusUpgrades.Add(bonusUpgrade);
            }
            newBonusUpgrades.Value = tmpnewBonusUpgrades.ToArray();

            // Update the level structure with new parameter and bonus upgrades
            levelStruct.Value[0] = newParameterUpgrades;
            levelStruct.Value[1] = newBonusUpgrades;
            (levelList).Value[ilevels] = levelStruct;
        }

        return myAsset;
    }
}