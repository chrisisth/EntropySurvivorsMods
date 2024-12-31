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
using UAssetAPI.FieldTypes;
using System.Globalization;

namespace PowerupGen
{
    internal class EditRuns
    {
        public EditRuns()
        {
            Console.WriteLine("Please provide the file path for the exported assets EntropySurvivors\\Content\\Database\\Runs :");
            string path = Console.ReadLine();
            if (Directory.Exists(path))
            {

                Console.WriteLine("Please enter the multiplier amount to increase minimum alive:");
                string input = Console.ReadLine();
                double minAliveIncrease = double.Parse(input, CultureInfo.InvariantCulture);
                Console.WriteLine(minAliveIncrease);

                Console.WriteLine("Please enter the amount to increase health multiplier:");
                input = Console.ReadLine();
                float healthIncrease = float.Parse(input, CultureInfo.InvariantCulture);
                Console.WriteLine(healthIncrease);
                Console.WriteLine("Please enter the amount to multiply boss health multiplier:");
                input = Console.ReadLine();
                float bosshealthIncrease = float.Parse(input, CultureInfo.InvariantCulture);
                Console.WriteLine(bosshealthIncrease);
                string[] files = Directory.GetFiles(path, "*.uasset*", SearchOption.AllDirectories);

                Usmap usmap = new Usmap("Mappings.usmap");

                foreach (string file in files)
                {
                    Console.WriteLine("Process file: " + file);
                    UAsset myAsset = new UAsset(file, EngineVersion.VER_UE4_5, usmap);
                    IncreaseMinimumAlive(myAsset, minAliveIncrease);
                    SetGlobalHealth(myAsset, healthIncrease);
                    SetBossHealth(myAsset, bosshealthIncrease);
                    myAsset.Write(file);
                }
            }
            else
            {
                Console.WriteLine("The specified path does not exist.");
            }
        }

        private UAsset IncreaseMinimumAlive(UAsset myAsset, double increase)
        {
            ArrayPropertyData runs = (ArrayPropertyData)((StructPropertyData)((NormalExport)myAsset.Exports[0]).Data[0]).Value.FirstOrDefault(x => x.Name.Value.Value == "Waves");
            if (runs == null)
            {
                throw new InvalidOperationException("Waves property not found.");
            }

            List<PropertyData> runsList = new List<PropertyData>(runs.Value);

            foreach (StructPropertyData run in runsList)
            {
                ArrayPropertyData enemies = (ArrayPropertyData)run.Value[3];
                List<PropertyData> enemiesList = new List<PropertyData>(enemies.Value);
                foreach (StructPropertyData enemy in enemiesList)
                {
                    //enemy.Value[0] <- Gegnertyp
                    IntPropertyData enemyAmount = (IntPropertyData)enemy.Value[1];
                    enemyAmount.Value = (int)(increase * enemyAmount.Value);
                    enemy.Value[1] = enemyAmount;
                }
            }
            return myAsset;
        }

        private UAsset SetGlobalHealth(UAsset myAsset, float health)
        {
            FloatPropertyData globalHealthfpd = (FloatPropertyData)((StructPropertyData)((NormalExport)myAsset.Exports[0]).Data[0]).Value.FirstOrDefault(x => x.Name.Value.Value == "GlobalHeathMultiplier");
            if (globalHealthfpd == null)
            {
                throw new InvalidOperationException("GlobalHeathMultiplier property not found." + myAsset.FilePath);
            }

            globalHealthfpd.Value += health;
            return myAsset;
        }
        private UAsset SetBossHealth(UAsset myAsset, float health)
        {
            FloatPropertyData bossHealthfpd = (FloatPropertyData)((StructPropertyData)((NormalExport)myAsset.Exports[0]).Data[0]).Value.FirstOrDefault(x => x.Name.Value.Value == "BossHealthMultiplier");
            if (bossHealthfpd == null)
            {
                throw new InvalidOperationException("BossHealthMultiplier property not found." + myAsset.FilePath);
            }
            bossHealthfpd.Value = (float)Math.Max(0.5, bossHealthfpd.Value * health);
            return myAsset;
        }

    }
}
