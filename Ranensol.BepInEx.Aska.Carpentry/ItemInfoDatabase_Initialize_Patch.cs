using HarmonyLib;
using SandSailorStudio.Inventory;
using SSSGame;

namespace Ranensol.BepInEx.Aska.Carpentry
{
    [HarmonyPatch(typeof(ItemInfoDatabase), "Initialize")]
    internal class ItemInfoDatabase_Initialize_Patch
    {
        private static bool _hasRun = false;

        [HarmonyPostfix]
        private static void Postfix(ItemInfoDatabase __instance)
        {
            if (_hasRun) return;
            _hasRun = true;

            Plugin.Log.LogInfo($"Modifying carpenter recipes");

            try
            {
                var itemsMap = __instance._itemsMap.Cast<Il2CppSystem.Collections.Generic.Dictionary<int, ItemInfo>>();

                foreach (var kvp in itemsMap)
                {
                    var itemInfo = kvp.Value;
                    var forgingBp = itemInfo.TryCast<ForgingBlueprintInfo>();
                    if (forgingBp == null) continue;

                    /// Carpentry A - Shafts
                    if (forgingBp.result?.name == "Item_Wood_Shaft" &&
                        forgingBp.parts != null &&
                        forgingBp.parts.Length > 0 &&
                        forgingBp.parts[0].itemInfo?.name == "Item_Wood_HardWoodLongStick")
                    {
                        ModifyRecipe(forgingBp, "Shafts", Plugin.Recipes["Shafts"].ValidatedQuantity, true);
                    }
                    // Carpentry A - Planks
                    else if (forgingBp.result?.name == "Item_Wood_Plank" &&
                        forgingBp.parts != null &&
                        forgingBp.parts.Length > 0 &&
                        forgingBp.parts[0].itemInfo?.name == "Item_Wood_HardWoodLog")
                    {
                        ModifyRecipe(forgingBp, "Planks", Plugin.Recipes["Planks"].ValidatedQuantity, true);
                    }
                    // Carpentry B - Beams
                    else if (forgingBp.result?.name == "Item_Wood_Beam" &&
                        forgingBp.parts != null &&
                        forgingBp.parts.Length > 0 &&
                        forgingBp.parts[0].itemInfo?.name == "Item_Wood_HardWoodLongStick")
                    {
                        ModifyRecipe(forgingBp, "Beams", Plugin.Recipes["Beams"].ValidatedQuantity, false);
                    }
                    // Carpentry B - Posts
                    else if (forgingBp.result?.name == "Item_Wood_Post" &&
                        forgingBp.parts != null &&
                        forgingBp.parts.Length > 0 &&
                        forgingBp.parts[0].itemInfo?.name == "Item_Wood_HardWoodLog")
                    {
                        ModifyRecipe(forgingBp, "Posts", Plugin.Recipes["Posts"].ValidatedQuantity, false);
                    }
                }

                Plugin.Log.LogInfo($"Recipe modification complete");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"Error modifying recipes: {ex.Message}");
            }
        }

        private static void ModifyRecipe(ForgingBlueprintInfo blueprint, string recipeName, int newQuantity, bool isBenchA)
        {
            var oldQuantity = blueprint.quantity;
            blueprint.quantity = newQuantity;

            if (isBenchA)
            {
                Plugin.MaxCarpentryQuantityA = Math.Max(Plugin.MaxCarpentryQuantityA, newQuantity);
            }
            else
            {
                Plugin.MaxCarpentryQuantityB = Math.Max(Plugin.MaxCarpentryQuantityB, newQuantity);
            }

            Plugin.Log.LogInfo($"Modified {recipeName}: {oldQuantity} -> {newQuantity}");
        }
    }
}