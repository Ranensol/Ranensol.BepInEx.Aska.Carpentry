using HarmonyLib;
using SandSailorStudio.Inventory;

namespace Ranensol.BepInEx.Aska.Carpentry
{
    [HarmonyPatch(typeof(InventoryComponent), "Start")]
    internal class InventoryComponent_Start_Patch
    {
        [HarmonyPostfix]
        static void Postfix(InventoryComponent __instance)
        {
            // Adjust capacity for Carpentry A bench
            if (Plugin.MaxCarpentryQuantityA > 0 &&
                __instance?.name != null &&
                __instance.name.Contains("CarpenterHorse_A") &&
                __instance.items != null &&
                __instance.items._containers != null &&
                __instance.items._containers.Count > 0)
            {
                var currentCapacity = __instance.items._containers[0].capacity;
                if (currentCapacity < Plugin.MaxCarpentryQuantityA)
                {
                    __instance.items._containers[0].capacity = Plugin.MaxCarpentryQuantityA;
                    Plugin.Log.LogInfo($"Adjusted CarpenterHorse_A capacity: {currentCapacity} -> {Plugin.MaxCarpentryQuantityA}");
                }
            }
        }
    }
}