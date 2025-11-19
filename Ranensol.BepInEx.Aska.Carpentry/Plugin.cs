using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;

namespace Ranensol.BepInEx.Aska.Carpentry
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BasePlugin
    {
        internal static new ManualLogSource Log;

        // Recipe definitions
        internal static Dictionary<string, RecipeConfig> Recipes { get; private set; }

        // Max capacities per bench
        internal static int MaxCarpentryQuantityA { get; set; }
        internal static int MaxCarpentryQuantityB { get; set; }

        public override void Load()
        {
            Log = base.Log;
            Log.LogInfo($"Loading plugin");

            InitializeRecipes();
            InitializeConfig();
            ValidateConfig();

            // Apply patches
            Harmony harmony = new(MyPluginInfo.PLUGIN_GUID);
            harmony.PatchAll();

            Log.LogInfo($"Plugin loaded successfully");
        }

        private void InitializeRecipes()
        {
            Recipes = new Dictionary<string, RecipeConfig>
            {
                { "Shafts", new RecipeConfig("Shafts", 2, "Carpentry A - Quantities", "Number of shafts produced from 1 hardwood long stick") },
                { "Planks", new RecipeConfig("Planks", 2, "Carpentry A - Quantities", "Number of planks produced from 1 hardwood log") },
                { "Beams", new RecipeConfig("Beams", 1, "Carpentry B - Quantities", "Number of beams produced from 1 hardwood long stick") },
                { "Posts", new RecipeConfig("Posts", 1, "Carpentry B - Quantities", "Number of posts produced from 1 hardwood log") }
            };
        }

        private void InitializeConfig()
        {
            foreach (var recipe in Recipes.Values)
            {
                recipe.ConfigEntry = Config.Bind(
                    recipe.Section,
                    recipe.Name,
                    recipe.DefaultValue,
                    $"{recipe.Description} (default: {recipe.DefaultValue})"
                );
            }
        }

        private static void ValidateConfig()
        {
            foreach (var recipe in Recipes.Values)
            {
                recipe.ValidatedQuantity = ValidateQuantity(
                    recipe.ConfigEntry.Value,
                    recipe.Name,
                    recipe.DefaultValue
                );
            }

            Log.LogInfo($"Config loaded - Shafts: {Recipes["Shafts"].ValidatedQuantity}, " +
                       $"Planks: {Recipes["Planks"].ValidatedQuantity}, " +
                       $"Beams: {Recipes["Beams"].ValidatedQuantity}, " +
                       $"Posts: {Recipes["Posts"].ValidatedQuantity}");
        }

        private static int ValidateQuantity(int value, string itemName, int defaultValue)
        {
            if (value < 1)
            {
                Log.LogError($"Invalid {itemName} quantity: {value}. Must be at least 1. Using default: {defaultValue}");
                return defaultValue;
            }
            return value;
        }
    }

    internal class RecipeConfig
    {
        public string Name { get; }
        public int DefaultValue { get; }
        public string Section { get; }
        public string Description { get; }
        public ConfigEntry<int> ConfigEntry { get; set; }
        public int ValidatedQuantity { get; set; }

        public RecipeConfig(string name, int defaultValue, string section, string description)
        {
            Name = name;
            DefaultValue = defaultValue;
            Section = section;
            Description = description;
        }
    }
}