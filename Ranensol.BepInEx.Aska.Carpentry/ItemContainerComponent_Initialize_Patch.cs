using HarmonyLib;
using SandSailorStudio.Inventory;

namespace Ranensol.BepInEx.Aska.Carpentry
{
    [HarmonyPatch(typeof(ItemContainerComponent), "Initialize")]
    internal class ItemContainerComponent_Initialize_Patch
    {
        [HarmonyPostfix]
        static void Postfix(ItemContainerComponent __instance)
        {
            // Adjust capacity for Carpentry B bench
            if (Plugin.MaxCarpentryQuantityB > 0 &&
                __instance?.name != null &&
                __instance.name.Contains("CarpenterHorse_B") &&
                __instance.container != null)
            {
                var currentCapacity = __instance.container.capacity;
                if (currentCapacity < Plugin.MaxCarpentryQuantityB)
                {
                    __instance.container.capacity = Plugin.MaxCarpentryQuantityB;
                    Plugin.Log.LogInfo($"Adjusted CarpenterHorse_B capacity: {currentCapacity} -> {Plugin.MaxCarpentryQuantityB}");
                }
            }
        }
    }
}