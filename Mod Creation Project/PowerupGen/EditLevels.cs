using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAssetAPI.UnrealTypes;
using UAssetAPI.Unversioned;
using UAssetAPI;
using UAssetAPI.ExportTypes;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.PropertyTypes.Structs;
using System.Globalization;

namespace PowerupGen
{
    internal class EditLevels
    {
        public EditLevels()
        {
            Console.WriteLine("Please provide the file path for the exported assets EntropySurvivors\\Content\\Database\\Levels :");
            string path = Console.ReadLine();
            if (Directory.Exists(path))
            {
                RemixSettings settings = new RemixSettings();
                try
                {
                    Console.WriteLine("Enter the value for RemixOfDifficulty (0 for default) (-1 - 2):");
                    string input = Console.ReadLine();
                    settings.RemixOfDifficulty = int.Parse(input, CultureInfo.InvariantCulture);

                    Console.WriteLine("Enter the value for additional ExtraHealthPercentage (0 for default) (e.g 2):");
                    input = Console.ReadLine();
                    settings.ExtraHealthPercentage = float.Parse(input, CultureInfo.InvariantCulture);

                    Console.WriteLine("Enter the value for additional ExtraMoveSpeedPercentage (0 for default) (e.g 0.6):");
                    input = Console.ReadLine();
                    settings.ExtraMoveSpeedPercentage = float.Parse(input, CultureInfo.InvariantCulture);

                    Console.WriteLine("Enter the value for additional ExtraDamagePercentage (0 for default) (e.g 4):");
                    input = Console.ReadLine();
                    settings.ExtraDamagePercentage = float.Parse(input, CultureInfo.InvariantCulture);

                    Console.WriteLine("Enter the value for additional ExtraEnemyCountPercentage (0 for default) (e.g 0.4):");
                    input = Console.ReadLine();
                    settings.ExtraEnemyCountPercentage = float.Parse(input, CultureInfo.InvariantCulture);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error has occurred: " + ex.Message);
                }

                Console.WriteLine("\nSettings have been updated:");
                Console.WriteLine($"RemixOfDifficulty: {settings.RemixOfDifficulty}");
                Console.WriteLine($"ExtraHealthPercentage: {settings.ExtraHealthPercentage}");
                Console.WriteLine($"ExtraMoveSpeedPercentage: {settings.ExtraMoveSpeedPercentage}");
                Console.WriteLine($"ExtraDamagePercentage: {settings.ExtraDamagePercentage}");
                Console.WriteLine($"ExtraEnemyCountPercentage: {settings.ExtraEnemyCountPercentage}");

            try
                {
                    string[] files = Directory.GetFiles(path, "*.uasset*", SearchOption.AllDirectories);


                    foreach (string file in files)
                    {
                        Console.WriteLine("Process file: " + file);
                        UAsset myAsset = new UAsset(file, EngineVersion.VER_UE4_5, new Usmap("Mappings.usmap"));
                        myAsset = SetRemix(myAsset, settings);
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

        private UAsset SetRemix(UAsset myAsset, RemixSettings finalremixsettings)
        {
            var difficultydefinitions = ((PropertyData<PropertyData[]>)((NormalExport)myAsset.Exports[0]).Data[3]).Value;

            foreach (StructPropertyData difficultydefinition in difficultydefinitions)
            {
                StructPropertyData remixsettings = (StructPropertyData)difficultydefinition.Value.FirstOrDefault(x => x.Name.Value.Value == "RemixSettings");
                List<PropertyData> remixsettingsList = new List<PropertyData>(remixsettings.Value);

                if (finalremixsettings.RemixOfDifficulty != 0) {
                    ((IntPropertyData) remixsettingsList[0]).Value = finalremixsettings.RemixOfDifficulty;
                }
                if (finalremixsettings.ExtraHealthPercentage != 0)
                {
                    ((FloatPropertyData) remixsettingsList[1]).Value += finalremixsettings.ExtraHealthPercentage;
                }
                if (finalremixsettings.ExtraMoveSpeedPercentage != 0)
                {
                    ((FloatPropertyData)remixsettingsList[2]).Value += finalremixsettings.ExtraMoveSpeedPercentage;
                }
                if (finalremixsettings.ExtraMoveSpeedPercentage != 0)
                {
                    ((FloatPropertyData)remixsettingsList[2]).Value += finalremixsettings.ExtraMoveSpeedPercentage;
                }
                if (finalremixsettings.ExtraDamagePercentage != 0)
                {
                    ((FloatPropertyData)remixsettingsList[3]).Value += finalremixsettings.ExtraDamagePercentage;
                }
                if (finalremixsettings.ExtraEnemyCountPercentage != 0)
                {
                    ((FloatPropertyData)remixsettingsList[4]).Value += finalremixsettings.ExtraEnemyCountPercentage;
                }
            }

            return myAsset;
        }
    }
    class RemixSettings
    {
        public int RemixOfDifficulty { get; set; }
        public float ExtraHealthPercentage { get; set; }
        public float ExtraMoveSpeedPercentage { get;set; }
        public float ExtraDamagePercentage { get; set; }
        public float ExtraEnemyCountPercentage { get; set; }
    }
}
